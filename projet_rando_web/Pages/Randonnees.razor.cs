using projet_rando_web.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using projet_rando_web.Enums;
using Microsoft.AspNetCore.Components;

namespace projet_rando_web.Pages
{
    public partial class Randonnees
    {
        Randonnee _randonnee = new Randonnee();
        List<Randonnee> _randonnees;
        private string _filtreVille = string.Empty;
        private string _filtreDifficulte = string.Empty;
        private string _filtreMeteo = string.Empty;
        private string _filtreType = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            _randonnees = randonneeService.GetRandonnees();
            List<Randonnee> filtreRandonnees = _randonnees;
        }

        private void FiltrerRandonnees()
        {
            _randonnees = randonneeService.GetRandonnees(); 

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


        private void Save()
        {
            randonneeService.SaveOrUpdate(_randonnee);
            Reset();
            _randonnees = randonneeService.GetRandonnees();
        }
        private void Reset()
        {
            _randonnee = new Randonnee();
            _randonnees = randonneeService.GetRandonnees();
        }
        private void Edit(string randonneeId)
        {
            _randonnee = randonneeService.GetRandonnee(randonneeId);
        }
        private void Delete(string randonneeId)
        {
            randonneeService.Delete(randonneeId);
            _randonnees = randonneeService.GetRandonnees();
        }

    }
}
