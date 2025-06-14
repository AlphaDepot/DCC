using MudBlazor;

namespace DCC.Components.Themes;

/// <summary>
///     Represents the main application dark theme.
/// </summary>
public abstract class DarkTheme
{
    // Colors
    private const string Primary = "rgb(96,165,250)";
    private const string Secondary = "rgb(236,72,153)"; // "rgb(244,114,182)";
    private const string Tertiary = "rgb(45,212,191)";


    // Background
    private const string Background = "rgb(24,24,27)";
    private const string Foreground = "rgb(39,39,42)";

    private const string Lines = "rgb(63,63,70)"; // "rgb(39,39,42)";

    // Text
    private const string TextPrimary = "rgb(228,228,231)";
    private const string TextSecondary = "rgb(161,161,170)";
    private const string TextDisabled = "rgba(228,228,231, 0.75)";

    public static readonly PaletteDark DarkPalette = new()
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

        ActionDefault = "rgb(161,161,170)",

        // Lines
        LinesDefault = Lines,
        LinesInputs = Lines,
        TableLines = Lines,
        Divider = Lines,

        // Status
        Info = "rgb(96,165,250)",
        Success = "rgb(0,220,130)",
        Warning = "rgb(251,191,36)",
        Error = "rgb(239,68,68)",

        // Text
        TextPrimary = TextPrimary,
        TextDisabled = TextDisabled,
        DarkContrastText = TextDisabled,
        AppbarText = TextSecondary,
        TextSecondary = TextSecondary,
        DrawerIcon = TextSecondary,
        DrawerText = TextSecondary,

        Dark = Foreground
    };
}