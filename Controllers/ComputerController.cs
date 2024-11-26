using ComputerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerApi.Controllers
{
    [Route("computer")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly ComputerContext computerContext;

        public ComputerController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<Comp>> Post(CreateCompDto createCompDto)
        {
            var comp = new Comp()
            {
                Id = Guid.NewGuid(),
                Brand = createCompDto.Brand,
                Type = createCompDto.Type,
                Display = createCompDto.Display,
                Memory = createCompDto.Memory,
                OsId = createCompDto.OsId,
                CreatedTime = DateTime.Now
            };

            if (comp != null) 
            { 
                await computerContext.Comps.AddAsync(comp);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, comp);
            }

            return BadRequest(new { messages = "Hiba az objektum megadásával." });
        }
        [HttpGet]
        public async Task<ActionResult<Comp>> Get()
        {

            return Ok(await computerContext.Comps.Select(x => new { x.Brand, x.Type, x.Memory, x.Os.Name }).ToListAsync());
        }
        
        [HttpGet("numberOfComputers")]

        public async Task<ActionResult> GetNumberOfComputers()
        {
            var comps = await computerContext.Comps.ToListAsync();
            return Ok(new { message = "Sikeres lekérdezés.", result = comps.Count()});
        }

        [HttpGet("allWindowsOsComputer")]

        public async Task<ActionResult<Comp>> GetAllWindowsComputer()
        {
            return Ok(await computerContext.Comps.Where(x => x.Os.Name.Contains
            ("linux")).Select(x => new {comp =  x, osName = x.Os.Name }).ToListAsync());
        }
        [HttpGet("osOrderDescendant")]
        public async Task<ActionResult<OSystem>> GetOsOrderDescendant()
        {
            return Ok(await computerContext.Os.OrderByDescending(x => x.Name).ToListAsync());
        }


    }
}
