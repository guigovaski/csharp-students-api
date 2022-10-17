using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Models;

namespace StudentsApi.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Student>? Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                StudentId = 1,
                Email = "guizinho@gmail.com",
                Name = "Guizinho",
            },
            new Student
            {
                StudentId = 2,
                Email = "stezinha@gmail.com",
                Name = "Stezinha"
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
