﻿using projet_rando_web.Classes;

namespace projet_rando_web.Pages
{
    public partial class Randonnees
    {
        Randonnee _randonnee = new Randonnee();
        List<Randonnee> _randonnees = new List<Randonnee>();
        // protected override async Task OnInitializedAsync()
        protected async Task OnInitializedAsync()
        {
            GetRandonnees();
        }
        private void GetRandonnees()
        {
            _randonnees = randonneeService.GetRandonnees();
        }
        private void Save()
        {
            randonneeService.SaveOrUpdate(_randonnee);
            Reset();
            GetRandonnees();
        }
        private void Reset()
        {
            _randonnee = new Randonnee();
        }
        private void Edit(string randonneeId)
        {
            _randonnee = randonneeService.GetRandonnee(randonneeId);
        }
        private void Delete(string randonneeId)
        {
            randonneeService.Delete(randonneeId);
            GetRandonnees();
        }
    }
}