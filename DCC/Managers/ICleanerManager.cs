using DCC.Models;

namespace DCC.Managers;

/// <summary>
///     Defines the contract for managing cleaners within the application.
/// </summary>
public interface ICleanerManager
{
    Action? OnChange { get; set; }
    List<Cleaner> Cleaners { get; }

    Task LoadCleanersAsync(bool forceRefresh = false);
    Task AddCleanerAsync(Cleaner cleaner);
    Task UpdateCleanerAsync(Cleaner cleaner);
    Task RemoveCleanerAsync(int id);
}