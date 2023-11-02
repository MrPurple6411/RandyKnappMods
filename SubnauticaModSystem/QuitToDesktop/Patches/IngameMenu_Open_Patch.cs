namespace QuitToDesktop.Patches;

using HarmonyLib;
using QuitToDesktop.Configuration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

[HarmonyPatch(typeof(IngameMenu), nameof(IngameMenu.Open))]
public static class IngameMenu_Open_Patch
{
    static Button quitButton;
    static GameObject quitConfirmation;
    static IngameMenu ingameMenu;

    [HarmonyPostfix]
    public static void Postfix(IngameMenu __instance)
    {

        if (
#if SUBNAUTICA
            GameModeUtils.IsPermadeath()
#else
            GameModeManager.GetOption<bool>(GameOption.PermanentDeath)
#endif
            )
        {
            return;
        }

        if (__instance != null && quitButton == null)
        {
            ingameMenu = __instance;

            // make a new confirmation Menu
            var quitConfirmationPrefab = __instance.gameObject.FindChild("QuitConfirmation");
            quitConfirmation = GameObject.Instantiate(quitConfirmationPrefab, __instance.gameObject.FindChild("QuitConfirmation").transform.parent);
            quitConfirmation.name = "QuitToDesktopConfirmation";

            // get the No Button and add the needed listeners to it
            var noButtonPrefab = quitConfirmation.gameObject.transform.Find("ButtonNo").GetComponent<Button>();
            noButtonPrefab.onClick.RemoveAllListeners();
            noButtonPrefab.onClick.AddListener(() => { __instance.Close(); });

            // get the Yes Button and add the needed listeners to it
            var yesButtonPrefab = quitConfirmation.gameObject.transform.Find("ButtonYes").GetComponent<Button>();
            yesButtonPrefab.onClick.RemoveAllListeners();
            yesButtonPrefab.onClick.AddListener(() => { __instance.QuitGame(true); });

            // make the Quit To Desktop Button
            var buttonPrefab = __instance.quitToMainMenuButton;
            quitButton = GameObject.Instantiate(buttonPrefab, __instance.quitToMainMenuButton.transform.parent);
            quitButton.name = "ButtonQuitToDesktop";
            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(() => { __instance.gameObject.FindChild("QuitConfirmationWithSaveWarning").SetActive(false); }); // set the confirmation with save false so it doesn't conflict
            quitButton.onClick.AddListener(() => { __instance.gameObject.FindChild("QuitConfirmation").SetActive(false); }); // set the Quit To Main Menu confirmation to false so it doesn't conflict
            quitButton.onClick.AddListener(OnQuitButtonClicked);

            IEnumerable<Text> texts = quitButton.GetComponents<Text>().Concat(quitButton.GetComponentsInChildren<Text>());
            foreach (Text text in texts)
            {
                text.text = "Quit to Desktop";
            }

            texts = __instance.quitToMainMenuButton.GetComponents<Text>().Concat(__instance.quitToMainMenuButton.GetComponentsInChildren<Text>());
            foreach (Text text in texts)
            {
                text.text = "Quit to Main Menu";
            }
        }
    }

    private static void OnQuitButtonClicked()
    {
        if (!Settings.ShowConfirmationDialog)
        {
            ingameMenu.QuitGame(true);
            return;
        }
        else
        {
            quitConfirmation.SetActive(true);
        }
    }
}
