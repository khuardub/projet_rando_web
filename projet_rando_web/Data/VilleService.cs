using Microsoft.Extensions.Options;
using MongoDB.Driver;
using projet_rando_web.Classes;
using projet_rando_web.Interfaces;

namespace projet_rando_web.Data
{
    public class VilleService : IVille
    {
        private readonly IMongoCollection<Ville> _villesCollection;

        public VilleService(
            IOptions<RandoMaxDatabaseSettings> randoMaxDatabaseSettings)
        {
            var _mongoClient = new MongoClient(
                randoMaxDatabaseSettings.Value.ConnectionString);

            var _mongoDatabase = _mongoClient.GetDatabase(
                randoMaxDatabaseSettings.Value.DatabaseName);

            _villesCollection = _mongoDatabase.GetCollection<Ville>(
                randoMaxDatabaseSettings.Value.VilleCollectionName);
        }

        public List<Ville> GetVilles()
        {
            return _villesCollection.Find(FilterDefinition<Ville>.Empty).ToList();
        }
    }
}
