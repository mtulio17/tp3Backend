using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArticulosAPI.Data;
using ArticulosAPI.Modelos;
using ArticulosAPI.Repository;
using ArticulosAPI.Repositorios;
using System.Collections;
using ArticulosAPI.DTO;


using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace ArticulosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IMongoCollection<Articulo> _articulosCollection;
        private readonly IMongoClient _mongoClient;

        private readonly ResponseDTO _response;

    public ArticulosController(IMongoClient mongoClient, ResponseDTO responseDTO, IOptions<ArticulosDataBaseSettings> settings)
    {
            _response = responseDTO;
            _mongoClient = mongoClient;
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _articulosCollection = database.GetCollection<Articulo>(settings.Value.ArticulosCollectionName);
        }


    // GET: api/Articulos
    [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetArticulos()
        {
            try
            {
                var stock = await _articulosCollection.Find(_ => true).ToListAsync();
                _response.Result = stock;
                _response.DisplayMessage = "Lista de Stock";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        // GET: api/Articulos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> FindArticulo(int id)
        {
            var articulo = await _articulosCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

            if (articulo == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Articulo no encontrado";
                return NotFound(_response);
            }

            _response.Result = articulo;
            _response.DisplayMessage = "Informacion del articulo solicitado";
            return Ok(_response);
        }

        // PUT: api/Articulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticulo(int id, Articulo articulo)
        {
            try
            {
                var updatedArticulo = await _articulosCollection.FindOneAndReplaceAsync(a => a.Id == id, articulo);
                _response.Result = updatedArticulo;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el articulo";
                _response.Errors = new List<string>() { e.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Articulos
        [HttpPost]
        public async Task<IActionResult> CreateArticulo(Articulo articulo)
        {
            try
            {
                await _articulosCollection.InsertOneAsync(articulo);
                _response.Result = articulo;
                return CreatedAtAction("CreateArticulo", new { id = articulo.Id }, _response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al guardar el articulo";
                _response.Errors = new List<string>() { e.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulo(int id)
        {
            try
            {
                var deleteResult = await _articulosCollection.DeleteOneAsync(a => a.Id == id);
                if (deleteResult.DeletedCount > 0)
                {
                    _response.Result = true;
                    _response.DisplayMessage = "Articulo Eliminado";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al eliminar el articulo";
                    return BadRequest(_response);
                }
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { e.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
