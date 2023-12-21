using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDBContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            _logger.LogInformation("Getting Villas");
            IEnumerable<Villa> villas = await _context.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villas));

        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
         public async Task<ActionResult<VillaDTO>> GetVilla(int id)
         {
            if(id == 0)
            {
                _logger.LogError("Villa Id is 0");
                return BadRequest();
            }
            var villa =await  _context.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if(villa == null)
            {
                _logger.LogError($"Villa with id {id} not found");
                return NotFound();
            }
           return Ok(_mapper.Map<VillaDTO>(villa));
            
          }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> AddVilla([FromBody] VillaCreateDTO villaDTO)
        {
            if (villaDTO == null)
            {
                _logger.LogError("Villa Object is null");
                return BadRequest();
            }
            if (await _context.Villas.FirstOrDefaultAsync(villa => villa.Nombre.ToLower() == villaDTO.Nombre.ToLower()) != null)
            {
                _logger.LogError($"Villa {villaDTO.Nombre} already exists");
                ModelState.AddModelError("VillaNameEqualError", $"Villa {villaDTO.Nombre} already exists");
            }
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid Villa Object");
                return BadRequest(ModelState);
            }
           
           
            Villa modelo = _mapper.Map<Villa>(villaDTO);
           
            await _context.Villas.AddAsync(modelo);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, _mapper.Map<VillaDTO>(modelo));

           
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if(id == 0)
            {
                _logger.LogError("Villa Id is 0");
                return BadRequest();
            }   

            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if(villa == null)
            {
                _logger.LogError($"Villa with id {id} not found");
                return NotFound();
            }
           
            _context.Villas.Remove(villa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
        {
            if(villaDTO == null || id == 0 || id != villaDTO.Id )
            {
                return BadRequest();
            }

            Villa modelo = _mapper.Map<Villa>(villaDTO);
            _context.Villas.Update(modelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> vd)
        {
            if (vd == null || id == 0 )
            {
                return BadRequest();
            }

            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Id == id);
            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);
            if(villa == null)
            {
                return BadRequest();
            }

            vd.ApplyTo(villaDTO, ModelState);

            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villaDTO);
            _context.Villas.Update(modelo);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}
