using MongoDB.Driver;
using projet_rando_web.Classes;
using projet_rando_web.Interfaces;

namespace projet_rando_web.Data
{
    public class RandonneeService : IRandonnee
    {
        private MongoClient _client = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<Randonnee> _randonneeTable = null;

        public RandonneeService()
        {
            _client = new MongoClient("mongodb+srv://adminRandomax:*****@cluster0.x75fagl.mongodb.net/");
            _database = _client.GetDatabase("randomax");
            _randonneeTable = _database.GetCollection<Randonnee>("randonnees");
        }

        public string Delete(string randonneeId)
        {
            _randonneeTable.DeleteOne(rando => rando.Id == randonneeId);
            return "Supprimé";
        }

        public Randonnee GetRandonnee(string randonneeId)
        {
            return _randonneeTable.Find(rando => rando.Id == randonneeId).FirstOrDefault();
        }

        public List<Randonnee> GetRandonnees()
        {
            return _randonneeTable.Find(FilterDefinition<Randonnee>.Empty).ToList();
        }

        public void SaveOrUpdate(Randonnee randonnee)
        {
            var randoUpdate = _randonneeTable.Find(rando => rando.Id == randonnee.Id).FirstOrDefault();
            if (randoUpdate != null)
            {
                _randonneeTable.InsertOne(randoUpdate);
            }
            else
            {
                _randonneeTable.ReplaceOne(rando => rando.Id == randonnee.Id, randonnee);
            }
        }
    }
}
