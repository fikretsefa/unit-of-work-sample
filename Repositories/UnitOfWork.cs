using unit_of_work_sample.Context;

namespace unit_of_work_sample.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EducationDbContext _context;

    public UnitOfWork(EducationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
