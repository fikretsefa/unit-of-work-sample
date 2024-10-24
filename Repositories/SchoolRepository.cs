using Microsoft.EntityFrameworkCore;
using unit_of_work_sample.Context;
using unit_of_work_sample.Entities;

namespace unit_of_work_sample.Repositories;

public class SchoolRepository : Repository<School>, ISchoolRepository
{
    public EducationDbContext _context;
    public SchoolRepository(EducationDbContext context) : base(context)
    {
        _context = context;
    }

    public  async Task<School> Get(int id)
    {
        return await _context.Set<School>().Where(p => p.Id == id).FirstOrDefaultAsync();
    }
}
