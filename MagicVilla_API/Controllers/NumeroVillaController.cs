using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {

        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly INumeroVillaRepository _numeroVillaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepository villaRepo,  IMapper mapper, INumeroVillaRepository numeroVillaRepo)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _response = new APIResponse();
            _numeroVillaRepo = numeroVillaRepo;
        }

        [HttpGet]
        public async  Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Getting Number Villas");
                IEnumerable<NumeroVilla> numeroVillas = await _numeroVillaRepo.getAll();
                _response.Result = _mapper.Map<IEnumerable<NumeroVillaDTO>>(numeroVillas);
                _response.statusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                return Ok(_response);
            }catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVillas action: {ex.Message}");
                
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

                
            }
            return _response;

        }

        [HttpGet("{numero:int}", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
         public async Task<ActionResult<APIResponse>> GetNumeroVilla(int numero)
         {
            try
            {
                if (numero == 0)
                {
                    _logger.LogError("NumeroVilla Id is 0");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _numeroVillaRepo.Obtener(v => v.Numero == numero);
                if (villa == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<NumeroVillaDTO>(villa);
                _response.statusCode = HttpStatusCode.OK;
                _response.isSuccess = true;

                return Ok(_response);
            }
            catch (Exception e)
            {

                _response.isSuccess = false;
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>() { e.ToString()};
            }
            return _response; 
          }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddNumeroVilla([FromBody] NumeroVillaCreateDTO numeroVillaDTO)
        {
            try
            {
                if (numeroVillaDTO == null)
                {
                    _logger.LogError("Villa Object is null");

                    return BadRequest();
                }
                if (await _numeroVillaRepo.Obtener(numeroVilla => numeroVilla.Numero == numeroVillaDTO.Numero) != null)
                {
                    _logger.LogError($"Villa {numeroVillaDTO.Numero} already exists");
                    ModelState.AddModelError("VillaNameEqualError", $"Villa {numeroVillaDTO.Numero} already exists");
                }
                if (await _villaRepo.Obtener(v => v.Id == numeroVillaDTO.IdVilla) == null)
                {
                    _logger.LogError($"Villa with id {numeroVillaDTO.IdVilla} not found");
                    ModelState.AddModelError("VillaIdError", $"Villa with id {numeroVillaDTO.IdVilla} not found");
                }
                {

                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Villa Object");
                    return BadRequest(ModelState);
                }


                NumeroVilla modelo = _mapper.Map<NumeroVilla>(numeroVillaDTO);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaModificacion = DateTime.Now;
                await _numeroVillaRepo.Crear(modelo);
                _response.Result = _mapper.Map<NumeroVillaDTO>(modelo);
                _response.statusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.Numero }, _response);
            }
            catch (Exception e)
            {

               _response.isSuccess = false;
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>() { e.ToString()};
            }

            return BadRequest(_response);
           
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {

                if (id == 0)
                {
                    _logger.LogError("Villa Id is 0");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSuccess = false;
                    return BadRequest(_response);
                }

                var villa = await _numeroVillaRepo.Obtener(v => v.Numero == id);
                if (villa == null)
                {
                    _logger.LogError($"Villa with id {id} not found");
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    return NotFound(_response);
                }

                _numeroVillaRepo.Remover(villa);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.isSuccess = false;
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>() { e.ToString()};

                
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDTO numeroVillaDTO)
        {
            if(numeroVillaDTO == null || id == 0 || id != numeroVillaDTO.Numero )
            {
                _response.isSuccess = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest();
            }
            if (await _villaRepo.Obtener(v => v.Id == numeroVillaDTO!.IdVilla) == null)
            {
                _logger.LogError($"Villa with id {numeroVillaDTO!.IdVilla} not found");
                ModelState.AddModelError("VillaIdError", $"Villa with id {numeroVillaDTO.IdVilla} not found");
            }


            NumeroVilla modelo = _mapper.Map<NumeroVilla>(numeroVillaDTO);
            await _numeroVillaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            _response.isSuccess = true;



            return Ok(_response);
        }

       


    }
}
