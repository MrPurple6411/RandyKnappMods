namespace BetterScannerBlips;

using System.Reflection;
using HarmonyLib;
using BepInEx;
using BepInEx.Logging;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(Nautilus.PluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger { get; private set; }

    public void Awake()
    {
        Logger = base.Logger;
        Settings.Instance.Load();
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID);
        Logger.LogInfo("Patched");
    }
}