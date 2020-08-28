using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;
        public StudentController(StudentContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult<Student>> SaveInfo(Student student)
        {
            if(student == null)
            {
                return BadRequest(new {message = " Data Requied"});
            }

            var nameExist = await _context.StudentTable
                .Where(stu=> stu.SName == student.SName)
                .FirstOrDefaultAsync();
            if(nameExist != null)
            {
                return BadRequest();
            }

            try
            {
                await _context.StudentTable.AddAsync(student);
                var res= await _context.SaveChangesAsync();
                return Ok(new{message = "Save Successfully" , data = student,result = res.ToString()});
            }
            catch (Exception ex)
            {
                return Ok(new{message = $"{ex.Message.ToString()}"});
                
            }
        
        }

        [HttpGet]
        public async Task<ActionResult<Student>> GetAll()
        {
            var student = await _context.StudentTable.ToListAsync();

            return Ok(new{total = student.Count() , data = student});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetData(long id)
        {
            var student = await _context.StudentTable
                //.Include(sub => sub.StudentSubjects)
                //.Where(s => s.SID == id)
                .FirstOrDefaultAsync(s => s.SID == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(new{ data = student});
        }

        [HttpPut]
        public async Task<ActionResult<Student>> Update(Student student){

            var studList = await _context.StudentTable.Where(st => st.SID == student.SID).FirstOrDefaultAsync();
            if(studList == null)
            {
                return BadRequest(new {message = " No Data exist"});
            }

            studList.SID = student.SID;
            studList.SName = student.SName;
            studList.Address = student.Address;
            studList.Age = student.Age;

            _context.StudentTable.Update(studList);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Update done"});

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteDetail(long id)
        {
            //var stud = await _context.Students.FindAsync(id);
            var stud =  await _context.StudentTable
                .Include(sub => sub.StudentSubjects)
                .Where(s => s.SID == id).FirstOrDefaultAsync();
            if(stud == null)
            {
                return BadRequest(new {msg = "No id like this"});
            }

            try
            {
                _context.StudentTable.Remove(stud);
                await _context.SaveChangesAsync();
                return Ok(new {msg = "Delete done"});
            }
            catch (Exception)
            {
                return Ok(new {msg = "Got Some error"});
            }

        }
    }
}