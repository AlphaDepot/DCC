using MudBlazor;


namespace DCC.Services;

/// <summary>
///     Provides methods for managing directories, including finding and removing directories.
/// </summary>
internal class DirectoryService : IDirectoryService
{
    private readonly ISnackbar _snackbar;

    public DirectoryService(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }
    
    /// <summary>
    ///     Finds all directories matching the specified names under the given starting location.
    ///     Searches recursively and avoids duplicates.
    /// </summary>
    /// <param name="directoryNames">A list of directory names to search for.</param>
    /// <param name="startingLocation">The root directory to start the search from.</param>
    /// <returns>A list of paths to the matching directories.</returns>
    public async Task<List<string>> GetDirectoryPathsAsync(List<string> directoryNames, string startingLocation)
    {
        var foundDirectories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        FindMatchingDirectories(startingLocation, directoryNames, foundDirectories);
        return await Task.FromResult(foundDirectories.ToList());
    }

    /// <summary>
    ///     Removes all directories matching the specified names under the given starting location.
    ///     Uses <see cref="GetDirectoryPathsAsync" /> to find directories and deletes them.
    /// </summary>
    /// <param name="directoryNames">A list of directory names to remove.</param>
    /// <param name="startingLocation">The root directory to start the search from.</param>
    /// <returns>True if any directories were found and removal was attempted; otherwise, false.</returns>
    public async Task<bool> RemoveDirectoryAsync(List<string> directoryNames, string startingLocation)
    {
        var directoriesToRemove = await GetDirectoryPathsAsync(directoryNames, startingLocation);
        if (directoriesToRemove.Count == 0)
            return false;


        foreach (var dir in directoriesToRemove) TryDeleteDirectory(dir);

        return true;
    }

    /// <summary>
    ///     Recursively searches for directories matching the specified names.
    ///     Adds matching directories to the result set and skips descending into them.
    /// </summary>
    /// <param name="currentDir">The current directory being searched.</param>
    /// <param name="directoryNames">A list of directory names to search for.</param>
    /// <param name="result">A set to store the paths of matching directories.</param>
    private void FindMatchingDirectories(string currentDir, List<string> directoryNames, HashSet<string> result)
    {
        foreach (var dir in SafeGetDirectories(currentDir))
        {
            var dirName = Path.GetFileName(dir);
            if (directoryNames.Contains(dirName))
                result.Add(dir);
            // Do not recurse into this directory
            else
                FindMatchingDirectories(dir, directoryNames, result);
        }
    }

    /// <summary>
    ///     Safely retrieves the subdirectories of the specified path.
    ///     Handles exceptions such as access denied or path not found.
    /// </summary>
    /// <param name="path">The directory path to retrieve subdirectories from.</param>
    /// <returns>An enumerable of subdirectory paths, or an empty enumerable if an error occurs.</returns>
    private IEnumerable<string> SafeGetDirectories(string path)
    {
        try
        {
            return Directory.GetDirectories(path);
        }
        catch
        {
            // Ignore directories we can't access
            return [];
        }
    }

    /// <summary>
    ///     Attempts to delete the specified directory and its contents.
    ///     Ignores errors such as access denied or directory not found.
    /// </summary>
    /// <param name="path">The path of the directory to delete.</param>
    private void TryDeleteDirectory(string path)
    {
        try
        {
            Directory.Delete(path, true);
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException || ex is DirectoryNotFoundException ||
                                   ex is IOException)
        {
            if (ex is DirectoryNotFoundException)
                // Directory not found, nothing to do
                return;

            if (ex is UnauthorizedAccessException || ex is IOException)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error deleting directory '{path}': {ex.Message}");
                _snackbar.Add($"Error deleting directory '{path}': {ex.Message}", Severity.Error);
            }
                 
            else
                throw new InvalidOperationException($"Failed to delete directory '{path}'.", ex);
        }
    }
}