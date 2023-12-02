using DL.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApiStudentGPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly StudentDbContext _context;

        public SubjectController(StudentDbContext context)
        {
            _context = context;
        }

        // POST: api/subjects
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] SubjectDbDto subjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.subjectDbDto.Add(subjectDto);
            await _context.SaveChangesAsync();

            return Ok(subjectDto);
        }


        // PUT: api/subjects/{subject_id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectDbDto updatedSubjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedSubjectDto.id)
            {
                return BadRequest("Invalid ID in the request body.");
            }

            var existingSubject = await _context.subjectDbDto.FindAsync(id);

            if (existingSubject == null)
            {
                return NotFound();
            }

            existingSubject.Name = updatedSubjectDto.Name;

            _context.subjectDbDto.Update(existingSubject);
            await _context.SaveChangesAsync();

            return Ok(existingSubject);
        }

        // DELETE: api/subjects/{subject_id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.subjectDbDto.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            _context.subjectDbDto.Remove(subject);
            await _context.SaveChangesAsync();

            return Ok($"Subject with ID {id} has been deleted.");
        }


        // GET: api/subjects/{subject_id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _context.subjectDbDto.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // GET: api/subjects
        [HttpGet]
        public IActionResult GetAllSubjects()
        {
            var subjects = _context.subjectDbDto.ToList();

            return Ok(subjects);
        }

    }
}
