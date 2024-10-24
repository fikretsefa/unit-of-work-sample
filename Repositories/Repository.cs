using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using unit_of_work_sample.Context;
using unit_of_work_sample.Entities;

namespace unit_of_work_sample.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly EducationDbContext _context;

    public Repository(EducationDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Entity => _context.Set<T>();

    public async Task<bool> AddAsync(T entity)
    {
       //await _context.Set<T>().AddAsync(entity);
       EntityEntry<T> entityEntry = await Entity.AddAsync(entity);
       return entityEntry.State == EntityState.Added;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await Entity.AddRangeAsync(entities);
    }

    public bool Delete(T entity)
    {
        EntityEntry<T> entityEntry = Entity.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
       var entity = await Entity.FirstOrDefaultAsync(p => p.Id == id);
        return Delete(entity);

    }

    public void DeleteRangeAsync(IEnumerable<T> entities)
    {
        Entity.RemoveRange(entities);
    }

   

    public IQueryable<T> GetAll()
    {
        return Entity.AsQueryable();
    }

    public async Task<T> GetByIdAsync(int id)
    {
      return await Entity.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<T> GetFirstAsync(int id)
    {
        return await Entity.FirstOrDefaultAsync();
    }

    public async Task<T> GetFirstWhereAsync(Expression<Func<T, bool>> expression)
    {
        return await Entity.FirstOrDefaultAsync(expression);
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
    {
        return Entity.Where(expression);
    }

    public bool Update(T entity)
    {
        EntityEntry<T> entityEntry = Entity.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }

  
}
