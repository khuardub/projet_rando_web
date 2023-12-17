using projet_rando_web.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using projet_rando_web.Enums;
using Microsoft.AspNetCore.Components;
using projet_rando_web.Interfaces;

namespace projet_rando_web.Pages
{
    public partial class Randonnees
    {
        Randonnee _randonnee = new Randonnee();

        private string _filtreVille = string.Empty;
        private string _filtreDifficulte = string.Empty;
        private string _filtreMeteo = string.Empty;
        private string _filtreType = string.Empty;
        List<Randonnee> _randonnees = new List<Randonnee>();
        protected override async Task OnInitializedAsync()
        {
            _randonnees = await randonneeService.GetRandonneesAVenirNonArchive();
        }

        private async Task FiltrerRandonnees()
        {
            _randonnees = await randonneeService.GetRandonnees(); 

            if (!string.IsNullOrEmpty(_filtreVille))
            {
                _randonnees = _randonnees
                    .Where(r => r.EndroitDepart.Nom.ToLower().Contains(_filtreVille.ToLower()))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(_filtreDifficulte))
            {
                _randonnees = _randonnees
                    .Where(r => r.Niveau.ToString().ToLower() == _filtreDifficulte.ToLower())
                    .ToList();
            }

            if (!string.IsNullOrEmpty(_filtreMeteo))
            {
                _randonnees = _randonnees
                    .Where(r => r.Meteo.ToString().ToLower() == _filtreMeteo.ToLower())
                    .ToList();
            }

            if (!string.IsNullOrEmpty(_filtreType))
            {
                _randonnees = _randonnees
                    .Where(r => r.TypeRando.ToString().ToLower() == _filtreType.ToLower())
                    .ToList();
            }

        }

    }
}
