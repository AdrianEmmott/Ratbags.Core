namespace Ratbags.Core.Models;

// paginating lists of data - e.g. articles
public class PagedResult<T>
{
    public int TotalCount { get; set; }
    public List<T> Items { get; set; } = new List<T>();
    public int PageSize { get; set; } // take
    public int CurrentPage { get; set; } // skip
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
