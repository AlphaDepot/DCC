using Blazored.LocalStorage;
using DCC.Enums;
using Microsoft.JSInterop;
using MudBlazor;
using AppTheme = DCC.Components.Themes.AppTheme;

namespace DCC.Managers;

/// <summary>
///     Service for managing theme modes and preferences.
/// </summary>
public class ThemeManager(ILocalStorageService localStorage, IJSRuntime jsRuntime) : IThemeManager
{
	/// <summary>
	///     The key used to store the theme mode in local storage.
	/// </summary>
	private const string ThemeKey = "BrsThemeMode";

	/// <summary>
	///     The task to load the JS module.
	///     The module is loaded lazily to avoid loading it when not needed.
	/// </summary>
	private readonly Lazy<Task<IJSObjectReference>> _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
        "import", "./js/themeServices.js").AsTask());


	/// <summary>
	///     The current theme mode.
	///     The default value is System.
	/// </summary>
	private ThemeMode _currentThemeMode = ThemeMode.System;

	/// <summary>
	///     Event triggered when the theme mode changes.
	/// </summary>
	public Action? OnChange { get; set; }

	/// <summary>
	///     Indicates if the theme service is initialized.
	/// </summary>
	public bool IsInitialized { get; set; }

	/// <summary>
	///     Indicates if the current theme is dark mode.
	/// </summary>
	public bool IsDarkMode { get; set; }

	/// <summary>
	///     Gets the current theme.
	/// </summary>
	public MudTheme Theme { get; } = new AppTheme().Theme;

	/// <summary>
	///     Initializes the theme mode by retrieving the saved mode or system preference.
	/// </summary>
	public async Task InitializeThemeMode()
    {
        _currentThemeMode = await GetMode();
        IsDarkMode = _currentThemeMode == ThemeMode.Dark;
        IsInitialized = true;
        NotifyStateChanged();
    }

	/// <summary>
	///     Toggles the theme mode between Light, Dark, and System.
	/// </summary>
	public async Task DarkModeToggle()
    {
        UpdateThemeMode();
        await ApplyThemeMode();
        NotifyStateChanged();
    }

	/// <summary>
	///     Gets the icon for the dark/light mode button based on the current theme mode.
	/// </summary>
	public string DarkLightModeButtonIcon => _currentThemeMode switch
    {
        ThemeMode.Light => Icons.Material.Outlined.DarkMode,
        ThemeMode.Dark => Icons.Material.Rounded.AutoMode,
        ThemeMode.System => Icons.Material.Filled.WbSunny,
        _ => Icons.Material.Rounded.AutoMode
    };

	/// <summary>
	///     Disposes the JS module when the service is disposed.
	/// </summary>
	public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }

	/// <summary>
	///     Updates the current theme mode to the next mode in the sequence.
	/// </summary>
	private void UpdateThemeMode()
    {
        _currentThemeMode = _currentThemeMode switch
        {
            ThemeMode.Light => ThemeMode.Dark,
            ThemeMode.Dark => ThemeMode.System,
            ThemeMode.System => ThemeMode.Light,
            _ => ThemeMode.System
        };
    }

	/// <summary>
	///     Applies the current theme mode by setting the appropriate dark mode flag.
	/// </summary>
	private async Task ApplyThemeMode()
    {
        if (_currentThemeMode == ThemeMode.System)
        {
            var systemPreference = await GetSystemPreference();
            IsDarkMode = systemPreference == ThemeMode.Dark;
        }
        else
        {
            IsDarkMode = _currentThemeMode == ThemeMode.Dark;
        }

        await SetMode(_currentThemeMode);
    }


	/// <summary>
	///     Sets the theme mode in local storage.
	/// </summary>
	/// <param name="mode">The theme mode to set.</param>
	private async Task SetMode(ThemeMode mode)
    {
        switch (mode)
        {
            case ThemeMode.Light:
                await localStorage.SetItemAsync(ThemeKey, "light");
                break;
            case ThemeMode.Dark:
                await localStorage.SetItemAsync(ThemeKey, "dark");
                break;
            case ThemeMode.System:
            default:
                await localStorage.SetItemAsync(ThemeKey, "system");
                break;
        }
    }

	/// <summary>
	///     Gets the saved theme mode from local storage or the system preference if not set.
	/// </summary>
	private async Task<ThemeMode> GetMode()
    {
        var mode = await localStorage.GetItemAsync<string>(ThemeKey);
        if (string.IsNullOrWhiteSpace(mode)) return await GetSystemPreference();

        return mode switch
        {
            "light" => ThemeMode.Light,
            "dark" => ThemeMode.Dark,
            "system" => await GetSystemPreference(),
            _ => ThemeMode.System
        };
    }

	/// <summary>
	///     Gets the system preference for dark mode.
	/// </summary>
	private async Task<ThemeMode> GetSystemPreference()
    {
        var module = await _moduleTask.Value;
        var isDarkMode = await module.InvokeAsync<bool>("getSystemPreference");
        return isDarkMode ? ThemeMode.Dark : ThemeMode.Light;
    }

	/// <summary>
	///     Notifies subscribers that the theme mode has changed.
	/// </summary>
	private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}