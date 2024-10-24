using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using unit_of_work_sample.Entities;

namespace unit_of_work_sample.Repositories;
public interface IRepository<T> where T : class
{
    DbSet<T> Entity {  get; }
    Task<bool> AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entities);

    bool Update(T entity);

    bool Delete(T entity);

    void DeleteRangeAsync(IEnumerable<T> entities);

    Task<bool> DeleteByIdAsync(int id);

    IQueryable<T> GetWhere(Expression<Func<T,bool>> expression);

    Task<T> GetByIdAsync(int id);


    Task<T> GetFirstWhereAsync(Expression<Func<T, bool>> expression);

}
