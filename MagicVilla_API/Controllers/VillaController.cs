using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDBContext _context;

        public VillaController(ILogger<VillaController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Getting Villas");
            return Ok(_context.Villas.ToList());

        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
         public ActionResult<VillaDTO> GetVilla(int id)
         {
            if(id == 0)
            {
                _logger.LogError("Villa Id is 0");
                return BadRequest();
            }
            var villa = _context.Villas.FirstOrDefault(v => v.Id == id);
            if(villa == null)
            {
                _logger.LogError($"Villa with id {id} not found");
                return NotFound();
            }
           return Ok(villa);
            
          }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> AddVilla([FromBody] VillaDTO villaDTO)
        {
            if(_context.Villas.FirstOrDefault(villa => villa.Nombre.ToLower() == villaDTO.Nombre.ToLower()) != null)
            {
                _logger.LogError($"Villa {villaDTO.Nombre} already exists");
                ModelState.AddModelError("VillaNameEqualError", $"Villa {villaDTO.Nombre} already exists");
            }
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid Villa Object");
                return BadRequest(ModelState);
            }
            if(villaDTO == null)
            {
                _logger.LogError("Villa Object is null");
                return BadRequest();
            }
            if(villaDTO.Id > 0)
            {
                _logger.LogError("Villa Id is greater than 0");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa modelo = new()
            {
                Nombre = villaDTO.Nombre,
                Detalles = villaDTO.Detalles,
                Tarifa = villaDTO.Tarifa,
                Ocupantes = villaDTO.Ocupantes,
                MetrosCuadrados = villaDTO.MetrosCuadrados,
                ImagenURL = villaDTO.ImagenURL,
                Amenidades = villaDTO.Amenidades,
                
            };
            _context.Villas.Add(modelo);
            _context.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);

           
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if(id == 0)
            {
                _logger.LogError("Villa Id is 0");
                return BadRequest();
            }   

            var villa = _context.Villas.FirstOrDefault(v => v.Id == id);
            if(villa == null)
            {
                _logger.LogError($"Villa with id {id} not found");
                return NotFound();
            }
           
            _context.Villas.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || id == 0 || id != villaDTO.Id )
            {
                return BadRequest();
            }

            Villa modelo = new()
            {
                Id = villaDTO.Id,
                Nombre = villaDTO.Nombre,
                Detalles = villaDTO.Detalles,
                Tarifa = villaDTO.Tarifa,
                Ocupantes = villaDTO.Ocupantes,
                MetrosCuadrados = villaDTO.MetrosCuadrados,
                ImagenURL = villaDTO.ImagenURL,
                Amenidades = villaDTO.Amenidades,
            };
            _context.Villas.Update(modelo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> vd)
        {
            if (vd == null || id == 0 )
            {
                return BadRequest();
            }

            var villa = _context.Villas.FirstOrDefault(v => v.Id == id);
            VillaDTO villaDTO = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalles = villa.Detalles,
                Tarifa = villa.Tarifa,
                Ocupantes = villa.Ocupantes,
                MetrosCuadrados = villa.MetrosCuadrados,
                ImagenURL = villa.ImagenURL,
                Amenidades = villa.Amenidades,
            };  
            if(villa == null)
            {
                return BadRequest();
            }

            vd.ApplyTo(villaDTO, ModelState);

            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = new()
            {
                Id = villaDTO.Id,
                Nombre = villaDTO.Nombre,
                Detalles = villaDTO.Detalles,
                Tarifa = villaDTO.Tarifa,
                Ocupantes = villaDTO.Ocupantes,
                MetrosCuadrados = villaDTO.MetrosCuadrados,
                ImagenURL = villaDTO.ImagenURL,
                Amenidades = villaDTO.Amenidades,
            };
            _context.Villas.Update(modelo);
            _context.SaveChanges();
            return NoContent();
        }



    }
}
