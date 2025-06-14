using MudBlazor;

namespace DCC.Managers;

/// <summary>
///     Interface for managing theme-related properties and operations.
///     Provides methods to initialize theme mode, toggle between dark and light modes,
///     and access theme-related state and events.
/// </summary>
public interface IThemeManager : IAsyncDisposable
{
    Action? OnChange { get; set; }
    bool IsDarkMode { get; set; }
    bool IsInitialized { get; }
    MudTheme Theme { get; }
    string DarkLightModeButtonIcon { get; }
    Task InitializeThemeMode();
    Task DarkModeToggle();
}