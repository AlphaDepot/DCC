using MudBlazor;

namespace DCC.Constants;

/// <summary>
///     Provides constants for configuring dialog options used across the application.
/// </summary>
public static class DialogConstants
{
    public static readonly DialogOptions DefaultDialogOptions = new()
    {
        CloseOnEscapeKey = false,
        FullWidth = true,
        MaxWidth = MaxWidth.Large,
        BackdropClick = false,
        BackgroundClass = "dialog-backdrop"
    };

    public static readonly DialogOptions ProcessingDialogOptions = new()
    {
        CloseOnEscapeKey = false,
        CloseButton = false,
        FullWidth = true,
        MaxWidth = MaxWidth.Small,
        BackdropClick = false,
        NoHeader = true,
        Position = DialogPosition.Center,
        BackgroundClass = "processing-dialog-background"
    };
}