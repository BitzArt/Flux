using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <summary>
/// Flux parameters for a paged operation.
/// </summary>
public interface IPagedOperationParameters
{
    /// <summary>
    /// Pagination parameters.
    /// </summary>
    public PageRequest PageRequest { get; }
}
