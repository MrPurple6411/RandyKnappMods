namespace QuitToDesktop.Configuration;

using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Options.Attributes;

public class Settings : ConfigFile
{
    [Toggle("Show Confirmation Dialog?")]
    public bool showConfirmationDialog = true;

    public static Settings Instance { get; } = OptionsPanelHandler.RegisterModOptions<Settings>();

    public static bool ShowConfirmationDialog => Instance.showConfirmationDialog;

}
