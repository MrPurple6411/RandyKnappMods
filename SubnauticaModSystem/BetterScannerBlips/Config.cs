namespace BetterScannerBlips;

using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Options.Attributes;
using UnityEngine;

public class Config : ConfigFile
{
    [Slider("Max Range", 0, 500f)]
    public float maxRange = 200.0f;

    [Slider("Max Range Scale", 0, 1f)]
    public float maxRangeScale = 0.2f;

    [Slider("Close Range", 0, 100f)]
    public float closeRange = 12.0f;

    [Slider("Close Range Scale", 0, 1f)]
    public float closeRangeScale = 1.0f;

    [Slider("Min Range", 0, 50f)]
    public float minRange = 1.0f;

    [Slider("Min Range Scale", 0, 1f)]
    public float minRangeScale = 10.0f;

    [Slider("Text Range", 0, 500f)]
    public float textRange = 100.0f;

    [Slider("Alpha Out Range", 0, 500f)]
    public float alphaOutRange = 150.0f;

    [Slider("Max Alpha", 0, 1f)]
    public float maxAlpha = 1f;

    [Slider("Min Alpha", 0, 1f)]
    public float minAlpha = 0.4f;

    [Toggle("Custom Colors")]
    public bool customColors = false;

    [ColorPicker("Circle Color")]
    public Color circleColor = new Color(0, 1, 0, 1);

    [ColorPicker("Text Color")]
    public Color textColor = new Color(0, 1, 0, 1);

    [Toggle("Show Distance")]
    public bool showDistance = true;

    [Toggle("No Text")]
    public bool noText = false;

    [Keybind("Toggle Key")]
    public KeyCode toggleKey = KeyCode.L;

    public static Config Instance { get; } = OptionsPanelHandler.RegisterModOptions<Config>();

    public static float MaxRange => Instance.maxRange;
    public static float MaxRangeScale => Instance.maxRangeScale;
    public static float CloseRange => Instance.closeRange;
    public static float CloseRangeScale => Instance.closeRangeScale;
    public static float MinRange => Instance.minRange;
    public static float MinRangeScale => Instance.minRangeScale;
    public static float TextRange => Instance.textRange;
    public static float AlphaOutRange => Instance.alphaOutRange;
    public static float MaxAlpha => Instance.maxAlpha;
    public static float MinAlpha => Instance.minAlpha;
    public static bool CustomColors => Instance.customColors;
    public static Color CircleColor => Instance.circleColor;
    public static Color TextColor => Instance.textColor;
    public static bool ShowDistance => Instance.showDistance;
    public static bool NoText => Instance.noText;
    public static KeyCode ToggleKey => Instance.toggleKey;

}
