namespace QuitToDesktop.Configuration;

using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Options.Attributes;

[Menu("Quit To Desktop", LoadOn = MenuAttribute.LoadEvents.MenuOpened | MenuAttribute.LoadEvents.MenuRegistered)]
public class Config : ConfigFile
{
    [Toggle("Show Confirmation Dialog (restart required)")]
    public bool showConfirmationDialog = true;

    internal static Config Instance { get; } = OptionsPanelHandler.RegisterModOptions<Config>();

    public static bool ShowConfirmationDialog => Instance.showConfirmationDialog;

}
