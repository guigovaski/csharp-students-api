using System.ComponentModel.DataAnnotations;

namespace StudentsApi.Models;

public class Student
{
    [Key]
    public int StudentId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; }
}
