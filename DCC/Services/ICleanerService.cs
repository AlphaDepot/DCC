using DCC.Models;

namespace DCC.Services;

/// <summary>
///     Defines operations for managing cleaner-related data and functionality.
/// </summary>
public interface ICleanerService
{
    Task<List<Cleaner>> GetCleanersAsync();
    Task<Cleaner?> GetCleanerByIdAsync(int id);
    Task<Cleaner> CreateCleanerAsync(Cleaner cleaner);
    Task<Cleaner> UpdateCleanerAsync(Cleaner cleaner);
    Task DeleteCleanerAsync(int id);
}