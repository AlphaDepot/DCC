using MudBlazor;

namespace DCC.Components.Themes;

public class AppTheme
{
    public AppTheme()
    {
        SetTypography();
        SetThemeColorPallets();
    }

    public MudTheme Theme { get; set; } = new();

    private void SetTypography()
    {
        var typography = new Typography
        {
            // Set default header sizes
            H1 = new H1Typography { FontSize = "2.5rem", FontWeight = "600" },
            H2 = new H2Typography { FontSize = "2rem", FontWeight = "600" },
            H3 = new H3Typography { FontSize = "1.5rem", FontWeight = "600" },
            H4 = new H4Typography { FontSize = "1.35rem", FontWeight = "600" },
            H5 = new H5Typography { FontSize = "1.20rem", FontWeight = "600" },
            H6 = new H6Typography { FontSize = "1rem", FontWeight = "600" },
            Default = new DefaultTypography
            {
                FontSize = "1rem",
                FontWeight = "400",
                LineHeight = "2"
            },
            Body1 = new Body1Typography
            {
                FontSize = "1rem",
                FontWeight = "400",
                LineHeight = "2"
            }
        };

        Theme.Typography = typography;
    }

    private void SetThemeColorPallets()
    {
        Theme.PaletteLight = LightTheme.LightPalette;
        Theme.PaletteDark = DarkTheme.DarkPalette;
    }
}