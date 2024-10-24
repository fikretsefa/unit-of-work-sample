
using Microsoft.EntityFrameworkCore;
using unit_of_work_sample.Context;
using unit_of_work_sample.Entities;
using unit_of_work_sample.Repositories;

namespace unit_of_work_sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<EducationDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-P74S1G2\\SQLEXPRESS;Database=Education;User Id=sa;Password=rootroot;TrustServerCertificate=true;"));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ISchoolRepository,SchoolRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("GetSchoolById/{schoolId:int}", async (int schoolId, EducationDbContext context) =>
            {
                var school = await context.School.FindAsync(schoolId);

                if (school is null)
                {
                    return Results.NotFound(); 
                }

                return Results.Ok(school);
            });

            app.MapGet("IncreaseBudget/{schoolId:int}", async (int schoolId, EducationDbContext context) =>
            {
                var school = await context.School.FindAsync(schoolId); 

                if (school is null)
                {
                    return Results.NotFound();
                }

                school.AnnualBudget += school.AnnualBudget * 0.10m;

                await context.SaveChangesAsync();

                return Results.Ok(school);
            });

            //app.MapPost("AddSchool", async (string schoolName, EducationDbContext context) =>
            //{
            //    School school = new();
            //    school.Name = schoolName;

            //    await context.Set<School>().AddAsync(school);
            //    await context.SaveChangesAsync();

            //    return Results.Created();
            //});

            app.MapPost("AddSchool", async (string schoolName, ISchoolRepository schoolRepository, IUnitOfWork unitOfWork) =>
            {
                School school = new();
                school.Name = schoolName;

                await schoolRepository.AddAsync(school);
                await unitOfWork.SaveChangesAsync();

                return Results.Created();
            });

            app.MapGet("GetAllSchool", async (EducationDbContext context) => {
                var schools = await context.Set<School>().ToListAsync();
                return Results.Ok(schools);

            });

            app.MapControllers();

            app.Run();
        }
    }
}
