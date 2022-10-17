using Microsoft.EntityFrameworkCore;
using StudentsApi.Data;
using StudentsApi.Models;
using StudentsApi.Services.Interfaces;

namespace StudentsApi.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetStudents()
    {
        return await _context.Students.AsNoTracking().ToListAsync();
    }

    public async Task<Student> GetStudentById(int id)
    {
        return await _context.Students.AsNoTracking().FirstAsync(s => s.StudentId == id);
    }

    public async Task CreateStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudent(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}
