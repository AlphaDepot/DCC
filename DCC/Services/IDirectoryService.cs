namespace DCC.Services;

/// <summary>
///     Provides methods for retrieving and removing directories based on specified criteria.
/// </summary>
public interface IDirectoryService
{
    Task<List<string>> GetDirectoryPathsAsync(List<string> directoryNames, string startingLocation);
    Task<bool> RemoveDirectoryAsync(List<string> directoryNames, string startingLocation);
}