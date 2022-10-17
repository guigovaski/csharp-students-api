using StudentsApi.Models;

namespace StudentsApi.Services.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetStudents();
    Task<Student> GetStudentById(int id);
    Task CreateStudent(Student student);
    Task UpdateStudent(Student student);
    Task DeleteStudent(Student student);
}
