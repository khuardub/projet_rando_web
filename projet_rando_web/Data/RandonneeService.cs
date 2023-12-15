using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using projet_rando_web.Classes;
using projet_rando_web.Interfaces;
using projet_rando_web.Data;

namespace projet_rando_web.Data
{
    public class RandonneeService : IRandonnee
    {

        private readonly IMongoCollection<Randonnee> _randonneesCollection;
        private readonly IUtilisateur _utilisateurService;

        public RandonneeService(
            IOptions<RandoMaxDatabaseSettings> randoMaxDatabaseSettings,
            IUtilisateur utilisateurService)
        {
            var _mongoClient = new MongoClient(
                randoMaxDatabaseSettings.Value.ConnectionString);

            var _mongoDatabase = _mongoClient.GetDatabase(
                randoMaxDatabaseSettings.Value.DatabaseName);

            _randonneesCollection = _mongoDatabase.GetCollection<Randonnee>(
                randoMaxDatabaseSettings.Value.RandonneesCollectionName); ;

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


        public async Task<List<Randonnee>> GetRandonneesPasseesByAuteur(string utilisateurId)
        {
            DateTime currentDate = DateTime.Now;
            return _randonneesCollection
                .Find(rando => rando.Auteur.Id == utilisateurId && rando.DateDepart < currentDate)
                .ToList();
        }

        public async Task<List<Randonnee>> GetRandonneesFuturesByAuteur(string utilisateurId)
        {
            DateTime currentDate = DateTime.Now;
            return _randonneesCollection
                .Find(rando => rando.Auteur.Id == utilisateurId && rando.DateDepart > currentDate)
                .ToList();
        }


        public async Task<List<Randonnee>> GetRandonneesPasseesByParticipant(string utilisateurId)
        {
            DateTime currentDate = DateTime.Now;
            return _randonneesCollection
                .Find(rando => rando.Participants.Any(p => p.Id == utilisateurId) && rando.DateDepart < currentDate)
                .ToList();
        }

        public async Task<List<Randonnee>> GetRandonneesFuturesByParticipant(string utilisateurId)
        {
            DateTime currentDate = DateTime.Now;
            return _randonneesCollection
                .Find(rando => rando.Participants.Any(p => p.Id == utilisateurId) && rando.DateDepart > currentDate)
                .ToList();
        }

        public async Task<List<Randonnee>> GetRandonnees()
        {
            return _randonneesCollection.Find(FilterDefinition<Randonnee>.Empty).ToList();
        }

        public async Task<List<Randonnee>> GetRandonneesAVenirNonArchive()
        {
            var randoFiltres = _randonneesCollection
                    .Find(rando => rando.IsArchive == false && rando.DateDepart >= DateTime.Today)
                    .ToList();
            return randoFiltres;
        }
        
        public async Task<string> Update(Randonnee randonnee, string auteurId)
        {
            var randoUpdate = _randonneesCollection.Find(rando => rando.Id == randonnee.Id).FirstOrDefault();
            if (randoUpdate != null)
            {
                if (randoUpdate.Auteur.Id == auteurId)
                {
                    await _randonneesCollection.ReplaceOneAsync(rando => rando.Id == randonnee.Id, randonnee);
                    return "Randonnée modifiée.";
                }
                else
                {
                    return "Vous n'êtes pas l'auteur de la randonnée.";
                }
            }
            else
            {
                return "La randonnée n'existe pas.";
            }
        }
        public async Task<string> Insert(Randonnee randonnee, string utilisateurId)
        {
            if(randonnee != null)
            {
                var auteur = await _utilisateurService.GetUtilisateur(utilisateurId);
                if (auteur != null)
                {
                    randonnee.Auteur = auteur;
                    randonnee.Participants = new List<Utilisateur>();
                    _randonneesCollection.InsertOne(randonnee);
                    return "Randonnée ajoutée.";
                }
                else
                {
                    return "L'utilisateur n'existe pas.";
                }
                
            }
            else
            {
                return "La randonnée n'existe pas.";
            }
        }

        public bool RandonneeExiste(Randonnee randonnee)
        {
            var randonneExtiste = _randonneesCollection.Find(r => r.Nom == randonnee.Nom).FirstOrDefault();
            return randonneExtiste != null;
        }

        public List<Utilisateur> GetParticipants(Randonnee randonnee)
        {
            return randonnee.Participants.ToList();
        }

        public async Task<string> InsertParticipant(Randonnee randonnee, string utilisateurId)
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
                        return "Vous êtes déjà inscrit à cette randonnée.";
                    } else if (randonnee.Auteur.Id == user.Id)
                    {
                        return "Vous êtes l'auteur de cette randonnée.";
                    }
                    else
                    {
                        randonnee.Participants.Add(user);
                        var filter = Builders<Randonnee>.Filter.Eq(r => r.Id, randonnee.Id);
                        var update = Builders<Randonnee>.Update.Set(r => r.Participants, randonnee.Participants);
                        await _randonneesCollection.UpdateOneAsync(filter, update);
                        return "Inscription réussie";
                    }
                }
                else
                {
                    // utilisateur non trouvé
                    return "L'utilisateur non trouvé.";
                }
            }
            else
            {
                // rando non trouvé
                return "La randonnée n'existe pas.";
            }

        }

        public async Task<string> RemoveParticipant(Randonnee randonnee, string utilisateurId)
        {
            var randoExiste = RandonneeExiste(randonnee);
            if (randoExiste)
            {
                var user = await _utilisateurService.GetUtilisateur(utilisateurId);
                if (user != null)
                {
                    var participantToRemove = randonnee.Participants.FirstOrDefault(u => u.Id == utilisateurId);
                    if (participantToRemove != null)
                    {
                        randonnee.Participants.Remove(participantToRemove);
                        var filter = Builders<Randonnee>.Filter.Eq(r => r.Id, randonnee.Id);
                        var update = Builders<Randonnee>.Update.Set(r => r.Participants, randonnee.Participants);
                        await _randonneesCollection.UpdateOneAsync(filter, update);
                        return "Désinscription réussie.";
                    }
                    else
                    {
                        return "Vous n'êtes pas inscrit à cette randonnée.";
                    }

                }
                else
                {
                    return "L'utilisateur non trouvé.";
                }
            }
            else
            {
                return "La randonnée n'existe pas.";
            }
        }
    }
}
