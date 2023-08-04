using System;

public class MappingConfiguration
{
    MappingConfiguration {
 public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ArticuloDto, Articulo>();
            config.CreateMap<Articulo, ArticuloDto>();
        });
        return mappingConfig;
    }
}
}
