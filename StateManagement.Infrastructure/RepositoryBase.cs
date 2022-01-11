using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StateManagement.SharedKernel;

namespace StateManagement.Infrastructure;

public abstract class RepositoryBase<TAggregate, TId> : IRepository<TAggregate, TId> where TAggregate : AggregateRoot<TId>
{
    private readonly DbContext _dbContext;

    protected RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TAggregate> Set => _dbContext.Set<TAggregate>();
    protected IQueryable<TAggregate> All => Set;

    public string AggregateName => typeof(TAggregate).Name;

    public void Add(TAggregate aggregateRoot)
    {
        Set.Add(aggregateRoot);
    }

    public void Delete(TAggregate aggregateRoot)
    {
        Set.Remove(aggregateRoot);
    }

    public async Task<TAggregate> GetByIdAsync(TId id, params Expression<Func<TAggregate, object>>[] includeProperties)
    {

        var query = All;

        foreach (var include in includeProperties)
        {
            query = query.Include(include);
        }

        var aggregateRoot = await query.Where(x => x.Id.Equals(id)).SingleOrDefaultAsync();

        return aggregateRoot;
    }

    public async Task<TAggregate?> FirstOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate, params Expression<Func<TAggregate, object>>[] includeProperties)
    {

        var query = All.Where(predicate);

        foreach (var include in includeProperties)
        {
            query = query.Include(include);
        }

        var aggregateRoot = await query.FirstOrDefaultAsync();

        return aggregateRoot;
    }

    public async Task<TAggregate> FirstAsync(Expression<Func<TAggregate, bool>> predicate, params Expression<Func<TAggregate, object>>[] includeProperties)
    {
        var aggregate = await FirstOrDefaultAsync(predicate, includeProperties);

        if (aggregate == null) throw new NotFoundException($"{AggregateName} not found");

        return aggregate;
    }

    public async Task<TAggregate?> SingleOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate, params Expression<Func<TAggregate, object>>[] includeProperties)
    {
        var query = All.Where(predicate);

        foreach (var include in includeProperties)
        {
            query = query.Include(include);
        }

        var aggregateRoot = await query.SingleOrDefaultAsync();


        return aggregateRoot;
    }

    public async Task<TAggregate> SingleAsync(Expression<Func<TAggregate, bool>> predicate, params Expression<Func<TAggregate, object>>[] includeProperties)
    {
        var aggregate = await SingleOrDefaultAsync(predicate, includeProperties);

        if (aggregate == null) throw new NotFoundException($"{AggregateName} not found");

        return aggregate;
    }

    public virtual async Task<PagedResponse<TAggregate>> ListAsync(PageFilter pageFilter,
        Expression<Func<TAggregate, bool>> predicate = null,
        params Expression<Func<TAggregate, object>>[] includeProperties)
    {
        var query = All;

        foreach (var include in includeProperties)
        {
            query = query.Include(include);
        }

        if (predicate != null)
            query = query.Where(predicate);

        var pageTask = query
            .Skip(pageFilter.Skip)
            .Take(pageFilter.PageSize)
            .ToListAsync();
        var countTask = CountAsync(predicate);

        await System.Threading.Tasks.Task.WhenAll(pageTask, countTask);

        return new PagedResponse<TAggregate>(pageTask.Result, pageFilter, countTask.Result);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TAggregate, bool>> predicate = null)
    {

        int count;
        if (predicate == null)
            count = await All.CountAsync();
        else
            count = await All.CountAsync(predicate);

        return count;
    }

    public virtual async System.Threading.Tasks.Task SaveChangesAsync()
    {

        await _dbContext.SaveChangesAsync();
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message, Exception? innerEx = null)
        : base(message, innerEx) { }
}