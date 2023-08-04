using ArticulosAPI.DTO;

namespace ArticulosAPI.Repository
{
    public interface IArticulosRepositorio
    {
        Task<List<ArticulosDTO>> GetArticulos();
        Task<ArticulosDTO> GetArticulosById(int id);

        Task<ArticulosDTO> CreateUpdate(ArticulosDTO articulosDTO, int? id = 0 );
        Task<bool>Delete (int id);
    }
}
