using ElectEd.DTO;
using ElectEd.Services.Candidate;
using ElectEd.Services.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using System.Collections.Specialized;

namespace ElectEd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentInfoService _studentInfoService;


        public StudentsController(ApplicationDbContext context, IStudentInfoService studentInfoService)
        {
            _context = context;
            _studentInfoService = studentInfoService;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var student = _studentInfoService.GetStudents();

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
           var student = _studentInfoService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentDto studentDto)
        {

            var existingStudent = await _context.VoteSlips.SingleOrDefaultAsync(x => x.Id == id);
            if (existingStudent == null)
            {
                return NotFound($"Election with id {id} does not exist.");
            }

            // Only update properties (no need to update the 'id' field)
            studentDto.StudentId = studentDto.StudentId;
            studentDto.Name = studentDto.Name;
            studentDto.Department = studentDto.Department;

         

            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return the updated election
            return Ok(existingStudent);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentDto studentDto)
        {


            int id = _context.Students.Any() ? _context.Students.Max(x => x.Id) + 1 : 1;
           


            var student = new Student
            {
                Id = id,
                StudentId = studentDto.StudentId,
                Name = studentDto.Name,
                Department = studentDto.Department,
            
            };

            // The Id will automatically be generated and incremented by EF Core
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Return the newly created election with its URL
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);

        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
