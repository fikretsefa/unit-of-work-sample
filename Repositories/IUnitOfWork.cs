namespace unit_of_work_sample.Repositories;

public interface IUnitOfWork 
{
    Task<int> SaveChangesAsync();
}
