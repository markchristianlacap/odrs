using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database;

public class PagedReq
{
    [DefaultValue(1)]
    public int? Page { get; set; } = 1;

    [DefaultValue(15)]
    public int? RowsPerPage { get; set; } = 15;

    public string? SortBy { get; set; }
    public bool? Descending { get; set; }
}

public class PagedRes<T>
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int RowsPerPage { get; set; }
    public int RowsCount { get; set; }
    public IList<T> Rows { get; set; } = [];
}

public static class DbPagination
{
    public static async Task<PagedRes<T>> ToPagedAsync<T>(
        this IQueryable<T> query,
        PagedReq request,
        CancellationToken ct
    )
        where T : class
    {
        if (request.SortBy != null)
        {
            query =
                request.Descending ?? false
                    ? query.OrderByColumnDescending(request.SortBy)
                    : query.OrderByColumn(request.SortBy);
        }

        var page = request.Page ?? 1;
        var rowsPerPage = request.RowsPerPage ?? 15;
        var result = new PagedRes<T>
        {
            CurrentPage = page,
            RowsPerPage = rowsPerPage,
            RowsCount = await query.CountAsync(ct),
        };
        if (request.RowsPerPage == 0)
            result.RowsPerPage = result.RowsCount;
        result.PageCount = result.RowsCount / result.RowsPerPage;
        var skip = (page - 1) * rowsPerPage;
        result.Rows = await query.Skip(skip).Take(result.RowsPerPage).ToListAsync(ct);
        return result;
    }
}
