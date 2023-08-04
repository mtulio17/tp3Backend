using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArticulosAPI.Modelos
{
    public class Articulo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [BsonElement("descripcion")] 
        public string Descripcion { get; set; }

        [BsonElement("marca")]
        public string Marca { get; set; }

        [BsonElement("cantidad")]
        public int Cantidad { get; set; }       

    }


}

