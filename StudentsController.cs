using Microsoft.AspNetCore.Mvc;

namespace GenericRepository;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IRepository<Student, int> studentRepository) : ControllerBase
{
    private readonly IRepository<Student, int> _studentRepository = studentRepository;

    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetStudentsAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    [HttpGet("{studentId}")]
    public async Task<ActionResult<Student?>> GetStudentAsync(int studentId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);
        if (student is null) return NotFound(new ErrorResponse
        {
            Code = ApplicationErrorCode.ENTITY_NOT_FOUND,
            Message = $"Can not find student with id: {studentId}"
        });
        return student;
    }

    [HttpPost]
    public async Task<ActionResult> AddStudentAsync(StudentDTO student)
    {
        var savedCount = await _studentRepository.AddOrUpdateAsync(new Student
        {
            Name = student.Name
        });
        if (savedCount == 1)
            return Created();

        return BadRequest(new ErrorResponse
        {
            Code = ApplicationErrorCode.CAN_NOT_ADD_ENTITY,
            Message = "Can not create student"
        });
    }

    [HttpPut]
    public async Task<ActionResult> UpdateStudentAsync(Student updatedStudent)
    {
        var student = await _studentRepository.GetByIdAsync(updatedStudent.Id);
        if (student is null)
            return BadRequest(new ErrorResponse
            {
                Code = ApplicationErrorCode.ENTITY_NOT_FOUND,
                Message = "Incorrect Student entity"
            });

        var savedCount = await _studentRepository.AddOrUpdateAsync(updatedStudent);
        if (savedCount != 1)
        {
            return BadRequest(new ErrorResponse
            {
                Code = ApplicationErrorCode.CAN_NOT_UPDATE_ENTITY,
                Message = "Can not update entity"
            });
        }

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteStudentAsync(Student student)
    {
        var deletedCount = await _studentRepository.DeleteAsync(student);
        if (deletedCount == 1)
            return Ok();

        return BadRequest(new ErrorResponse
        {
            Code = ApplicationErrorCode.CAN_NOT_DELETE_ENTITY,
            Message = $"Can not delete student with id: {student.Id}"
        });
    }
}
