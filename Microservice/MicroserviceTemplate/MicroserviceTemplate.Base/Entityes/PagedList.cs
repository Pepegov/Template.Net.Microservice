namespace MicroserviceTemplate.Base.Entityes;

public class PagedList<TSource, TResult> : IPagedList<TResult>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public int IndexFrom { get; }
    public IList<TResult> Items { get; }
    public bool HasPreviousPage => PageIndex - IndexFrom > 0;
    public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

    public PagedList(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
        int pageIndex, int pageSize, int indexFrom)
    {
        if (indexFrom > pageIndex)
        {
            throw new ArgumentException(
                $"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
        }

        if (source is IQueryable<TSource> querable)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IndexFrom = indexFrom;
            TotalCount = querable.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            var items = querable.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();

            Items = new List<TResult>(converter(items));
        }
        else
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IndexFrom = indexFrom;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            var items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();

            Items = new List<TResult>(converter(items));
        }
    }

    public PagedList(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
    {
        PageIndex = source.PageIndex;
        PageSize = source.PageSize;
        IndexFrom = source.IndexFrom;
        TotalCount = source.TotalCount;
        TotalPages = source.TotalPages;

        Items = new List<TResult>(converter(source.Items));
    }
}

public class PagedList<T> : IPagedList<T>
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
    public int IndexFrom { get; init; }
    public IList<T> Items { get; init; }
    public bool HasPreviousPage => PageIndex - IndexFrom > 0;
    public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

    internal PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom)
    {
        if (indexFrom > pageIndex)
        {
            throw new ArgumentException(
                $"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
        }

        if (source is IQueryable<T> queryable)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IndexFrom = indexFrom;
            TotalCount = queryable.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            Items = queryable.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
        }
        else
        {
            var enumerable = source.ToList();
            PageIndex = pageIndex;
            PageSize = pageSize;
            IndexFrom = indexFrom;
            TotalCount = enumerable.Count;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            Items = enumerable
                .Skip((PageIndex - IndexFrom) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }

    internal PagedList() => Items = Array.Empty<T>();
}