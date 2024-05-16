namespace BitzArt.Flux;

/// <summary>
/// Represents a Flux Items Provider.
/// </summary>
public interface IFluxItemsProvider
{
    /// <summary>
    /// Current page.
    /// </summary>
    public int CurrentPage { get; }

    /// <summary>
    /// Total item count.
    /// </summary>
    public int TotalItems { get; }

    /// <summary>
    /// Total pages count.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Resets the current page to 1.
    /// </summary>
    public Task ResetPaginationAsync();

    /// <summary>
    /// Sets the current page.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    public Task SetPageAsync(int pageNumber);
}
