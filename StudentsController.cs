using Microsoft.AspNetCore.Mvc;

namespace GenericRepository;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IRepository<Student, int> studentRepository) : ControllerBase
{
    private readonly IRepository<Student, int> _studentRepository = studentRepository;
}
