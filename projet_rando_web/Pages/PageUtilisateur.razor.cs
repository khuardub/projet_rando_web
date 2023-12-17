using projet_rando_web.Classes;
using Microsoft.AspNetCore.Components;
using dymaptic.GeoBlazor.Core.Components.Layers;
using dymaptic.GeoBlazor.Core.Components.Views;
using dymaptic.GeoBlazor.Core.Components.Geometries;
using dymaptic.GeoBlazor.Core.Components.Symbols;
using dymaptic.GeoBlazor.Core.Objects;
using projet_rando_web.Data;
using System.Security.Claims;
using Microsoft.JSInterop;
using projet_rando_web.Enums;
using projet_rando_web.Interfaces;

namespace projet_rando_web.Pages
{
    public partial class PageUtilisateur
    {
        private UtilisateurSession utilisateurSession = new UtilisateurSession();
        int i = 0;
        List<Randonnee> _randonneesFuturesAuteur;
        List<Randonnee> _randonneesPasseesAuteur;
        List<Randonnee> _randonneesArchiveesAuteur;
        List<Randonnee> _randonneesFuturesParticipant;
        List<Randonnee> _randonneesPasseesParticipant;

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //utilisateurSession
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                utilisateurSession.Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            _randonneesArchiveesAuteur = await randonneeService.GetRandonneesArchiveesByAuteur(utilisateurSession.Id);
            _randonneesPasseesAuteur = await randonneeService.GetRandonneesPasseesByAuteur(utilisateurSession.Id);
            _randonneesFuturesAuteur = await randonneeService.GetRandonneesFuturesByAuteur(utilisateurSession.Id);
            _randonneesFuturesParticipant =
                await randonneeService.GetRandonneesFuturesByParticipant(utilisateurSession.Id);
            _randonneesPasseesParticipant =
                await randonneeService.GetRandonneesPasseesByParticipant(utilisateurSession.Id);
            string echelon = utilisateurSession.Echelon.GetDescription();
        }

        private void ModifierProfil()
        {
            navManager.NavigateTo($"/modification-profil/{utilisateurSession.Id}");
        }

    }
}