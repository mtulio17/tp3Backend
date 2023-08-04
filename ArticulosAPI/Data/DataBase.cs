namespace ArticulosAPI.Data
{
    using MongoDB.Driver;

    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("ArticulosDB");
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }


}
