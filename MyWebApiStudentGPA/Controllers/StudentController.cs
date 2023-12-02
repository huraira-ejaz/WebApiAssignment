using DL.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;



namespace MyWebApiStudentGPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        
        private readonly StudentDbContext _context;

        public StudentController(StudentDbContext context)
        {
            _context = context;
        }

        // POST: api/students
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDbDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.studentDbDto.Add(studentDto);
            await _context.SaveChangesAsync();
            return Ok(studentDto);
        }

        // PUT: api/students/{student_id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDbDto updatedStudentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedStudentDto.Id)
            {
                return BadRequest("Invalid ID in the request body.");
            }

            var existingStudent = await _context.studentDbDto.FindAsync(id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            // Update existing student properties
            existingStudent.Name = updatedStudentDto.Name;
            existingStudent.RollNumber = updatedStudentDto.RollNumber;
            existingStudent.PhoneNumber = updatedStudentDto.PhoneNumber;

            _context.studentDbDto.Update(existingStudent);
            await _context.SaveChangesAsync();

            return Ok(existingStudent);
        }

        // DELETE: api/students/{student_id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.studentDbDto.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.studentDbDto.Remove(student);
            await _context.SaveChangesAsync();

            return Ok($"Student with ID {id} has been deleted.");
        }

        // GET: api/students/{student_id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _context.studentDbDto.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // GET: api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _context.studentDbDto.ToList();

            return Ok(students);
        }
    }
}
