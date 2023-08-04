using ArticulosAPI.Data;
using ArticulosAPI.DTO;
using ArticulosAPI.Modelos;
using ArticulosAPI.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArticulosAPI.Repositorios
{
    public class ArticuloRepositorio:IArticulosRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper mapper;

        public ArticuloRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<ArticulosDTO> CreateUpdate(ArticulosDTO articulosDTO, int? id)
        {
            Articulo articulo = mapper.Map<ArticulosDTO, Articulo>(articulosDTO);
            if (articulo.Id > 0)
            {
                _context.Articulos.Update(articulo);
            }
            else {

                await _context.Articulos.AddAsync(articulo);
            }
            await _context.SaveChangesAsync();
            return mapper.Map<Articulo, ArticulosDTO>(articulo);
        }

        public Task<ArticulosDTO> CreateUpdate(int id, ArticulosDTO articulosDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Articulo articulo = await _context.Articulos.FindAsync(id);
                if (articulo == null)
                {
                    return false;

                }
                _context.Articulos.Remove(articulo);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }

        public Task<List<ArticulosDTO>> GetArticulos()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticulosDTO>> GetArticulosbyid()
        {
            List<Articulo> articulos = await _context.Articulos.ToListAsync();
            return mapper.Map<List<ArticulosDTO>>(articulos);
        }

        public async Task<ArticulosDTO> GetArticulosById(int id)
        {
            Articulo articulo = await _context.Articulos.FindAsync(id);
            return mapper.Map<ArticulosDTO>(articulo);
        }
    }

}
