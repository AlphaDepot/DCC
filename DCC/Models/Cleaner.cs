namespace DCC.Models;

/// <summary>
///     Represents a Cleaner entity that defines the details and configuration
///     for managing specific cleaning tasks or operations within the system.
/// </summary>
public class Cleaner
{
    /// <summary>
    ///     Gets or sets the unique identifier for the cleaner.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the cleaner.
    ///     This field is required.
    ///     <remarks>
    ///         A friednly name to identify the cleaner,
    ///         which is used in the UI and logs.
    ///         Example: JavaScript Cleaner, .Net Cleaner, etc.
    ///     </remarks>
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Gets or sets the description of the cleaner.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Gets or sets the list of directories associated with the cleaner.
    ///     This field is required.
    ///     <remarks>
    ///         A list of directories that the cleaner will operate on.
    ///         This are the directories that will be deleted.
    ///     </remarks>
    /// </summary>
    public required List<string> Directories { get; set; } = [];

    /// <summary>
    ///     Gets or sets the location associated with the cleaner.
    ///     This field is required.
    ///     <remarks>
    ///         This is the starting location where the cleaner will search for files to clean.
    ///         Any files or directories that match the cleaner's criteria
    ///         but are not a subdirectory of this location will not be cleaned.
    ///     </remarks>
    /// </summary>
    public required string Location { get; set; }
}