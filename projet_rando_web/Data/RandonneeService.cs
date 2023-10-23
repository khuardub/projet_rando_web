using Microsoft.Extensions.Options;
using MongoDB.Driver;
using projet_rando_web.Classes;
using projet_rando_web.Interfaces;

namespace projet_rando_web.Data
{
    public class RandonneeService : IRandonnee
    {

        private readonly IMongoCollection<Randonnee> _randonneesCollection;

        public RandonneeService(
            IOptions<RandoMaxDatabaseSettings> randoMaxDatabaseSettings)
        {
            var _mongoClient = new MongoClient(
                randoMaxDatabaseSettings.Value.ConnectionString);

            var _mongoDatabase = _mongoClient.GetDatabase(
                randoMaxDatabaseSettings.Value.DatabaseName);

            _randonneesCollection = _mongoDatabase.GetCollection<Randonnee>(
                randoMaxDatabaseSettings.Value.RandonneesCollectionName);
        }




        public string Delete(string randonneeId)
        {
            _randonneesCollection.DeleteOne(rando => rando.Id == randonneeId);
            return "Supprimé";
        }

        public Randonnee GetRandonnee(string randonneeId)
        {
            return _randonneesCollection.Find(rando => rando.Id == randonneeId).FirstOrDefault();
        }

        public List<Randonnee> GetRandonnees()
        {
            return _randonneesCollection.Find(FilterDefinition<Randonnee>.Empty).ToList();
        }

        public void SaveOrUpdate(Randonnee randonnee)
        {
            var randoUpdate = _randonneesCollection.Find(rando => rando.Id == randonnee.Id).FirstOrDefault();
            if (randoUpdate != null)
            {
                _randonneesCollection.InsertOne(randoUpdate);
            }
            else
            {
                _randonneesCollection.ReplaceOne(rando => rando.Id == randonnee.Id, randonnee);
            }
        }
    }
}
