using Microsoft.Extensions.Options;
using MongoDB.Driver;
using projet_rando_web.Classes;
using projet_rando_web.Interfaces;
using projet_rando_web.Pages;

namespace projet_rando_web.Data
{
    public class UtilisateurService : IUtilisateur
    {
        private readonly IMongoCollection<Utilisateur> _utilisateursCollection;

        public UtilisateurService(
            IOptions<RandoMaxDatabaseSettings> randoMaxDatabaseSettings)
        {
            var _mongoClient = new MongoClient(
                randoMaxDatabaseSettings.Value.ConnectionString);

            var _mongoDatabase = _mongoClient.GetDatabase(
                randoMaxDatabaseSettings.Value.DatabaseName);

            _utilisateursCollection = _mongoDatabase.GetCollection<Utilisateur>(
                randoMaxDatabaseSettings.Value.UtilisateursCollectionName);
        }

        public async Task SaveOrUpdateUser(Utilisateur utilisateur)
        {
            var userUpdate = _utilisateursCollection.Find(user => user.Courriel == utilisateur.Courriel).FirstOrDefault();
            if (userUpdate != null)
            {
                _utilisateursCollection.ReplaceOne(user => user.Id == utilisateur.Id, utilisateur);
            }
            else
            {
                _utilisateursCollection.InsertOne(utilisateur);
            }
        }

        public async Task<Utilisateur> GetUtilisateur(string utilisateurId)
        {
            return await Task.Run(() =>
            {
                return _utilisateursCollection.Find(user => user.Id == utilisateurId).FirstOrDefault();
            });
        }

        public Utilisateur GetUtilisateurByCourriel(string courriel)
        {
            return _utilisateursCollection.Find(user => user.Courriel == courriel).FirstOrDefault();
        }

        public string DeleteUser(string utilisateurId)
        {
            _utilisateursCollection.DeleteOne(user => user.Id == utilisateurId);
            return "Supprimé";
        }

        public List<Utilisateur> GetUtilisateurs()
        {
            return _utilisateursCollection.Find(FilterDefinition<Utilisateur>.Empty).ToList();
        }
    }
}
