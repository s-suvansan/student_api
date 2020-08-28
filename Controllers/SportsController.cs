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
    public class SportsController : ControllerBase
    {
        private readonly StudentContext _context;
        public SportsController(StudentContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult<Sports>> SaveInfo(Sports sports)
        {
            if(sports == null){
                return BadRequest(new {message = " Data Requied"});
            }

            var nameExist = await _context.SportsTable
                .Where(sp=> sp.SportsName == sports.SportsName)
                .FirstOrDefaultAsync();

            if(nameExist != null)
            {
                return BadRequest();
            }

            try
            {
                await _context.SportsTable.AddAsync(sports);
                var res= await _context.SaveChangesAsync();
                return Ok(new{message = "Save Successfully" , data = sports,result = res.ToString()});
            }
            catch (Exception ex)
            {
                return Ok(new{message = $"{ex.Message.ToString()}"});
                
            }
        
        }

        [HttpGet]
        public async Task<ActionResult<Sports>> GetAll()
        {
            var sports = await _context.SportsTable.ToListAsync();

            return Ok(new{total = sports.Count() , data = sports});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sports>> GetData(long id)
        {
            var sports = await _context.SportsTable.FindAsync(id);

            if (sports == null)
            {
                return NotFound();
            }

            return Ok(new{ data = sports});
        }


        [HttpPut]
        public async Task<ActionResult<Sports>> Update(Sports sports){

            var subList = await _context.SportsTable
                .Where(sub => sub.SportsName == sports.SportsName).FirstOrDefaultAsync();
            if(subList == null)
            {
                return BadRequest(new {message = " No Data exist"});
            }

            subList.SportsID = sports.SportsID;
            subList.SportsName = sports.SportsName;
            subList.StudID = sports.StudID;

            _context.SportsTable.Update(subList);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Update done"});

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Sports>> DeleteDetail(long id)
        {
            var sp = await _context.SportsTable.FindAsync(id);
            if(sp == null)
            {
                return BadRequest(new {msg = "No id like this"});
            }

            try
            {
                _context.SportsTable.Remove(sp);
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