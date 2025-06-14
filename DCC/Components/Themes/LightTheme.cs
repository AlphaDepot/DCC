using MudBlazor;

namespace DCC.Components.Themes;

/// <summary>
///     Represents the main application light theme.
/// </summary>
public abstract class LightTheme
{
    // Colors
    private const string Primary = "rgb(96,165,250)";
    private const string Secondary = "rgb(236,72,153)";
    private const string Tertiary = "rgb(20,184,166)";


    // Background
    private const string Background = "rgb(255,255,255)";
    private const string Foreground = "rgb(250,250,250)";

    private const string Lines = "rgb(228,228,231)";

    // Text
    private const string TextPrimary = "rgb(63,63,70)";
    private const string TextSecondary = "rgb(113,113,122)";
    private const string TextDisabled = "rgba(63,63,70, 0.75)";


    public static readonly PaletteLight LightPalette = new()
    {
        // Primary
        Primary = Primary,
        Secondary = Secondary,
        Tertiary = Tertiary,

        // Background
        Background = Background,
        DrawerBackground = Background,
        Surface = Foreground,
        BackgroundGray = Foreground,
        AppbarBackground = Background,

        TableStriped = Foreground,
        TableHover = Foreground,
        Skeleton = Foreground,

        // Lines
        LinesDefault = Lines,
        LinesInputs = Lines,
        TableLines = Lines,
        Divider = Lines,

        ActionDefault = "rgb(113,113,122)",

        // Status
        Info = "rgb(14,165,233)",
        Success = "rgb(0,193,106)",
        Warning = "rgb(245,158,11)",
        Error = "rgb(239,68,68)",


        // Text
        TextPrimary = TextPrimary,
        TextDisabled = TextDisabled,
        AppbarText = TextSecondary,
        TextSecondary = TextSecondary,
        DrawerIcon = TextSecondary,
        DrawerText = TextSecondary
    };
}