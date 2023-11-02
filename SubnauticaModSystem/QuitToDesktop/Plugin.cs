namespace QuitToDesktop;

using HarmonyLib;
using System.Reflection;
using BepInEx;
using QuitToDesktop.Configuration;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(Nautilus.PluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BaseUnityPlugin
{
    public void Awake()
    {
        Settings.Instance.Load();
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID);
        Logger.LogInfo("Patched");
    }
}
