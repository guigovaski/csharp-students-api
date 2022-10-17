using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsApi.Services.Interfaces;
using StudentsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace StudentsApi.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

	public StudentsController(IStudentService studentService)
	{
		_studentService = studentService;
	}

	[HttpGet]
	public async Task<ActionResult<IAsyncEnumerable<Student>>> Get()
	{
		var students = await _studentService.GetStudents();
		return Ok(students);
	}

	[HttpGet("{id:int}", Name = "GetStudent")]
	public async Task<ActionResult<Student>> GetById(int id)
	{
		var student = await _studentService.GetStudentById(id);
		return Ok(student);
	}

	[HttpPost]
	public async Task<IActionResult> Post(Student student)
	{
		await _studentService.CreateStudent(student);
		return CreatedAtRoute(nameof(GetById), new { id = student.StudentId });
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> Put(int id, Student student)
	{
		if (id != student.StudentId)
		{
			return BadRequest();
		}

		await _studentService.UpdateStudent(student);
		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id, Student student)
	{
		if (id != student.StudentId)
		{
			return BadRequest();
		}

		await _studentService.DeleteStudent(student);
		return Ok();
	}

}
