using ComputerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsController : ControllerBase
    {
        private readonly ComputerContext computerContext;

        public OsController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<OSystem>> Post(CreateOsDto createOsDto) 
        {
            var os = new OSystem
            {
                Id = Guid.NewGuid(),
                Name = createOsDto.Name,
                CreatedTime = DateTime.Now,
            };
            if (os != null)
            {
                await computerContext.Os.AddAsync(os);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, os); 
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OSystem>> Get()
        {
            return Ok(await computerContext.Os.ToListAsync());
        }

      
        [HttpGet]
        public async Task<ActionResult<OSystem>> GetById(Guid id)
        {
            var os = await computerContext.Os.FirstOrDefaultAsync(o => o.Id == id);
            if (os != null)
            {
                return Ok(os);
            }
            return NotFound(new { message = "Nincs találat."});
        }
    }
}
