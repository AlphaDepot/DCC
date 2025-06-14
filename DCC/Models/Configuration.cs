namespace DCC.Models;

/// <summary>
///     Represents the configuration for the application.
///     <remarks>
///         This class is the root of the configuration file.
///         It will be used to store the list of cleaners.
///     </remarks>
/// </summary>
public class Configuration
{
    /// <summary>
    ///     Gets or sets the list of cleaners defined in the configuration.
    /// </summary>
    public List<Cleaner> Cleaners { get; set; } = [];
}