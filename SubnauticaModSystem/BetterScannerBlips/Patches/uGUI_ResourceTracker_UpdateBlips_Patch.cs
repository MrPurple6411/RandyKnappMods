namespace BetterScannerBlips.Patches;

using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

[HarmonyPatch(typeof(uGUI_ResourceTracker), nameof(uGUI_ResourceTracker.UpdateBlips))]
public class uGUI_ResourceTracker_UpdateBlips_Patch
{
    private static bool hide = false;

    private static void Prefix(uGUI_ResourceTracker __instance)
    {
        if (__instance == null || __instance.blip == null || __instance.blip.GetComponent<CustomBlip>() != null)
            return;

        Plugin.Logger.LogDebug("Adding CustomBlip to blip prefab");
        __instance.blip.AddComponent<CustomBlip>();
    }

    private static void Postfix(uGUI_ResourceTracker __instance)
    {
        if (Input.GetKeyDown(Settings.ToggleKey))
        {
            hide = !hide;
            ErrorMessage.AddDebug(string.Format("Scanner Blips Toggled: {0}", hide ? $"OFF (Press {Settings.ToggleKey} to show)" : "ON"));
        }

        HashSet<ResourceTrackerDatabase.ResourceInfo> nodes = __instance.nodes;
        var blips = __instance.blips;

        Camera camera = MainCamera.camera;
        Vector3 position = camera.transform.position;
        Vector3 forward = camera.transform.forward;
        int i = 0;
        foreach (ResourceTrackerDatabase.ResourceInfo resourceInfo in nodes)
        {
            Vector3 lhs = resourceInfo.position - position;
            if (Vector3.Dot(lhs, forward) > 0f)
            {
                var blipObject = blips[i].gameObject;
                var customBlip = blipObject.GetComponent<CustomBlip>();

                customBlip.Refresh(resourceInfo);

                i++;
            }
        }

        for (var j = 0; j < blips.Count; ++j)
        {
            var blipObject = blips[j].gameObject;
            if (hide)
            {
                blipObject.SetActive(false);
            }
        }
    }
}