using MicroserviceTemplate.Base.Entityes;
using MicroserviceTemplate.Base.Reposytory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace MicroserviceTemplate.Base.UnitOfWork;

public sealed class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>
    where TContext : DbContext
{
    private bool _disposed;
    private Dictionary<Type, object>? _repositories;

    public UnitOfWork(TContext context)
    {
        DbContext = context ?? throw new ArgumentNullException(nameof(context));
        LastSaveChangesResult = new SaveChangesResult();
    }

    public TContext DbContext { get; }

    #region Methods

    public Task<IDbContextTransaction> BeginTransactionAsync(bool useIfExists = false)
    {
        var transaction = DbContext.Database.CurrentTransaction;
        if (transaction == null)
        {
            return DbContext.Database.BeginTransactionAsync();
        }

        return useIfExists ? Task.FromResult(transaction) : DbContext.Database.BeginTransactionAsync();
    }

    public IDbContextTransaction BeginTransaction(bool useIfExists = false)
    {
        var transaction = DbContext.Database.CurrentTransaction;
        if (transaction == null)
        {
            return DbContext.Database.BeginTransaction();
        }

        return useIfExists ? transaction : DbContext.Database.BeginTransaction();
    }

    public void SetAutoDetectChanges(bool value) => DbContext.ChangeTracker.AutoDetectChangesEnabled = value;

    public SaveChangesResult LastSaveChangesResult { get; }

    #endregion

    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
    {
        _repositories ??= new Dictionary<Type, object>();

        // what's the best way to support custom repository?
        if (hasCustomRepository)
        {
            var customRepo = DbContext.GetService<IRepository<TEntity>>();
            if (customRepo != null)
            {
                return customRepo;
            }
        }

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(DbContext);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public int SaveChanges()
    {
        try
        {
            return DbContext.SaveChanges();
        }
        catch (Exception exception)
        {
            LastSaveChangesResult.Exception = exception;
            return 0;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await DbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            LastSaveChangesResult.Exception = exception;
            return 0;
        }
    }

    public async Task<int> SaveChangesAsync(params IUnitOfWork[] unitOfWorks)
    {
        var count = 0;
        foreach (var unitOfWork in unitOfWorks)
        {
            count += await unitOfWork.SaveChangesAsync();
        }

        count += await SaveChangesAsync();
        return count;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _repositories?.Clear();
                DbContext.Dispose();
            }
        }

        _disposed = true;
    }

    public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback) =>
        DbContext.ChangeTracker.TrackGraph(rootEntity, callback);
}