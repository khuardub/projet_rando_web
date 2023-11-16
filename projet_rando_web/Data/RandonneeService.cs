using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using projet_rando_web.Classes;
using projet_rando_web.Interfaces;

namespace projet_rando_web.Data
{
    public class RandonneeService : IRandonnee
    {

        private readonly IMongoCollection<Randonnee> _randonneesCollection;
        private readonly IMongoCollection<Utilisateur> _utilisateursCollection;
        private readonly UtilisateurService _utilisateurService;

        public RandonneeService(
            IOptions<RandoMaxDatabaseSettings> randoMaxDatabaseSettings,
            UtilisateurService utilisateurService)
        {
            var _mongoClient = new MongoClient(
                randoMaxDatabaseSettings.Value.ConnectionString);

            var _mongoDatabase = _mongoClient.GetDatabase(
                randoMaxDatabaseSettings.Value.DatabaseName);

            _randonneesCollection = _mongoDatabase.GetCollection<Randonnee>(
                randoMaxDatabaseSettings.Value.RandonneesCollectionName);

            _utilisateursCollection = _mongoDatabase.GetCollection<Utilisateur>(
                randoMaxDatabaseSettings.Value.UtilisateursCollectionName);

            _utilisateurService = utilisateurService;
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
        
        public async Task SaveOrUpdate(Randonnee randonnee)
        {
            var randoUpdate = _randonneesCollection.Find(rando => rando.Id == randonnee.Id).FirstOrDefault();
            if (randoUpdate != null)
            {
                await _randonneesCollection.ReplaceOneAsync(rando => rando.Id == randonnee.Id, randonnee);
            }
            else
            {
                await _randonneesCollection.InsertOneAsync(randonnee);
            }
        }
        public void Insert(Randonnee randonnee)
        {
            if(randonnee != null)
            {
                _randonneesCollection.InsertOne(randonnee);
            }
        }

        public bool RandonneeExiste(Randonnee randonnee)
        {
            var randonneExtiste = _randonneesCollection.Find(r => r.Nom == randonnee.Nom).FirstOrDefault();
            return randonneExtiste != null;
        }

        public async Task InsertParticipant(Randonnee randonnee, string utilisateurId)
        {
            // verifier si rando existe
            var randoExiste = RandonneeExiste(randonnee);
            if (randoExiste)
            {
                // verifier si utilisateur existe
                var user = await _utilisateurService.GetUtilisateur(utilisateurId);
                if (user != null)
                {
                    // verifier si utilisateur pas deja inscrit a la rando
                    if (randonnee.Participants.Any(u => u.Id == user.Id))
                    {
                        return;
                    }
                    randonnee.Participants.Add(user);
                    var filter = Builders<Randonnee>.Filter.Eq(r => r.Id, randonnee.Id);
                    var update = Builders<Randonnee>.Update.Set(r => r.Participants, randonnee.Participants);
                    await _randonneesCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    // utilisateur non trouvé
                    return;
                }
            }
            else
            {
                // rando non trouvé
                return;
            }

        }
    }
}
