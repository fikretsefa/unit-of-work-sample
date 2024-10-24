using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using unit_of_work_sample.Entities;

namespace unit_of_work_sample.Context;

public class EducationDbContext : DbContext
{
    public DbSet<School> School { get; set; }
    public DbSet<Student> Student { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-P74S1G2\\SQLEXPRESS;Database=Education;User Id=sa;Password=rootroot;TrustServerCertificate=true;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<School>()
                .HasMany(s => s.Students)
                .WithOne(st => st.School)
                .HasForeignKey(st => st.SchoolId);
    }
}
