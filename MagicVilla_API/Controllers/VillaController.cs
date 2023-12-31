﻿using AutoMapper;
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
    public class VillaController : ControllerBase
    {

        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public VillaController(ILogger<VillaController> logger, IVillaRepository villaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async  Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Getting Villas");
                IEnumerable<Villa> villas = await _villaRepo.getAll();
                _response.Result = _mapper.Map<IEnumerable<VillaDTO>>(villas);
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

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
         public async Task<ActionResult<APIResponse>> GetVilla(int id)
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
                var villa = await _villaRepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaDTO>(villa);
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
        public async Task<ActionResult<APIResponse>> AddVilla([FromBody] VillaCreateDTO villaDTO)
        {
            try
            {
                if (villaDTO == null)
                {
                    _logger.LogError("Villa Object is null");

                    return BadRequest();
                }
                if (await _villaRepo.Obtener(villa => villa.Nombre.ToLower() == villaDTO.Nombre.ToLower()) != null)
                {
                    _logger.LogError($"Villa {villaDTO.Nombre} already exists");
                    ModelState.AddModelError("VillaNameEqualError", $"Villa {villaDTO.Nombre} already exists");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Villa Object");
                    return BadRequest(ModelState);
                }


                Villa modelo = _mapper.Map<Villa>(villaDTO);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaModificacion = DateTime.Now;
                await _villaRepo.Crear(modelo);
                _response.Result = _mapper.Map<VillaDTO>(modelo);
                _response.statusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetVilla", new { id = modelo.Id }, _response);
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
        public async Task<IActionResult> DeleteVilla(int id)
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

                var villa = await _villaRepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _logger.LogError($"Villa with id {id} not found");
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    return NotFound(_response);
                }

                _villaRepo.Remover(villa);
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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
        {
            if(villaDTO == null || id == 0 || id != villaDTO.Id )
            {
                _response.isSuccess = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest();
            }

            Villa modelo = _mapper.Map<Villa>(villaDTO);
            await _villaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            _response.isSuccess = true;



            return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> vd)
        {
            if (vd == null || id == 0 )
            {
                _response.isSuccess = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var villa = await _villaRepo.Obtener(v => v.Id == id, tracked:false);
            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);
            if(villa == null)
            {
                _response.isSuccess = false;
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            vd.ApplyTo(villaDTO, ModelState);

            
            if (!ModelState.IsValid)
            {
                _response.isSuccess = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.Result = ModelState;
                return BadRequest(_response);
            }

            Villa modelo = _mapper.Map<Villa>(villaDTO);
            await _villaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            _response.isSuccess = true;

            return Ok(_response);
        }



    }
}
