using ArticulosAPI.DTO;
using ArticulosAPI.Modelos;
using AutoMapper;

namespace ArticulosAPI
{
    public class MappingConfiguration
{

    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ArticulosDTO, Articulo>();
            config.CreateMap<Articulo, ArticulosDTO>();
        });
        return mappingConfig;
    }
}

}
