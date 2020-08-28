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
    public class SubjectController : ControllerBase
    {
        private readonly StudentContext _context;
        public SubjectController(StudentContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult<Subject>> SaveInfo(Subject subject)
        {
            if(subject == null){
                return BadRequest(new {message = " Data Requied"});
            }

            var nameExist = await _context.SubjectTable
                .Where(sub=> sub.SubjectTitle == subject.SubjectTitle)
                .FirstOrDefaultAsync();

            if(nameExist != null)
            {
                return BadRequest();
            }

            try
            {
                await _context.SubjectTable.AddAsync(subject);
                var res= await _context.SaveChangesAsync();
                return Ok(new{message = "Save Successfully" , data = subject,result = res.ToString()});
            }
            catch (Exception ex)
            {
                return Ok(new{message = $"{ex.Message.ToString()}"});
                
            }
        
        }

        [HttpGet]
        public async Task<ActionResult<Subject>> GetAll()
        {
            var subject = await _context.SubjectTable.ToListAsync();

            return Ok(new{total = subject.Count() , data = subject});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetData(long id)
        {
            var subject = await _context.SubjectTable.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(new{ data = subject});
        }

        [HttpGet("subjectsOfUser/{sid}")]
        public async Task<ActionResult<Subject>> GetDataBySid(long sid)
        {
            var subject = await _context.SubjectTable.Where(s => s.StudId == sid).ToListAsync();

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(new{ data = subject});
        }

        [HttpPut]
        public async Task<ActionResult<Subject>> Update(Subject subject){

            var subList = await _context.SubjectTable
                .Where(sub => sub.SubjectTitle == subject.SubjectTitle).FirstOrDefaultAsync();
            if(subList == null)
            {
                return BadRequest(new {message = " No Data exist"});
            }

            subList.SubjectID = subject.SubjectID;
            subList.SubjectTitle = subject.SubjectTitle;
            subList.StudId = subject.StudId;

            _context.SubjectTable.Update(subList);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Update done"});

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Subject>> DeleteDetail(long id)
        {
            var sub = await _context.SubjectTable.FindAsync(id);
            if(sub == null)
            {
                return BadRequest(new {msg = "No id like this"});
            }

            try
            {
                _context.SubjectTable.Remove(sub);
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
