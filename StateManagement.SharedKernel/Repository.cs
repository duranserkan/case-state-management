using System.Linq.Expressions;

namespace StateManagement.SharedKernel;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}

public interface IRepository<TAggregate, TId> : IUnitOfWork where TAggregate : IAggregateRoot<TId>
{
    string AggregateName { get; }

    void Add(TAggregate aggregateRoot);
    void Delete(TAggregate aggregateRoot);

    Task<TAggregate> GetByIdAsync(TId id, params Expression<Func<TAggregate, object>>[] includeProperties);
    Task<TAggregate> FirstOrDefaultAsync(
        Expression<Func<TAggregate, bool>> predicate,
        params Expression<Func<TAggregate, object>>[] includeProperties);
    Task<TAggregate> FirstAsync(
        Expression<Func<TAggregate, bool>> predicate,
        params Expression<Func<TAggregate, object>>[] includeProperties);
    Task<TAggregate> SingleOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate,
        params Expression<Func<TAggregate, object>>[] includeProperties);
    Task<TAggregate> SingleAsync(Expression<Func<TAggregate, bool>> predicate,
        params Expression<Func<TAggregate, object>>[] includeProperties);
    Task<PagedResponse<TAggregate>> ListAsync(PageFilter pageFilter,
        Expression<Func<TAggregate, bool>> predicate = null,
        params Expression<Func<TAggregate, object>>[] includeProperties);

    Task<int> CountAsync(Expression<Func<TAggregate, bool>> predicate = null);

}

public class PagedResponse<TData>
{
    public IReadOnlyList<TData> DataList { get; }
    public PageFilter PageFilter { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public int Taken => DataList.Count;

    public PagedResponse(IReadOnlyList<TData> dataList, PageFilter pageFilter, int totalCount)
    {
        DataList = dataList;
        PageFilter = pageFilter;
        TotalCount = totalCount;

        TotalPages = (int)Math.Ceiling((double)TotalCount / PageFilter.PageSize);
    }
}

public class PageFilter
{
    public PageFilter()
    {
        PageNumber = 1;
        PageSize = 10;
    }
    public PageFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 100 ? 100 : pageSize;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
    public int Skip => (PageNumber - 1) * PageSize;
}