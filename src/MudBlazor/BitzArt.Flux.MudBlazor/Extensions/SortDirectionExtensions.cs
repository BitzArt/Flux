namespace MudBlazor;

/// <summary>
/// Extension methods for <see cref="SortDirection"/>.
/// </summary>
public static class SortDirectionExtensions
{
    /// <summary>
    /// Inverts the specified <paramref name="direction"/>.
    /// </summary>
    public static SortDirection Invert(this SortDirection direction)
    {
        return direction switch
        {
            SortDirection.Ascending => SortDirection.Descending,
            SortDirection.Descending => SortDirection.Ascending,
            _ => throw new InvalidOperationException($"Sort direction '{direction}' can not be inverted.")
        };
    }
}
