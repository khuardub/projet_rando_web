using projet_rando_web.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;
using dymaptic.GeoBlazor.Core.Components.Geometries;
using dymaptic.GeoBlazor.Core.Components.Layers;
using dymaptic.GeoBlazor.Core.Components.Views;
using Microsoft.AspNetCore.Components;
using projet_rando_web.Interfaces;
using dymaptic.GeoBlazor.Core.Components.Popups;
using dymaptic.GeoBlazor.Core.Components.Symbols;
using dymaptic.GeoBlazor.Core.Objects;
using System;
using MongoDB.Driver.Core.Misc;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace projet_rando_web.Pages
{
    public partial class Index
    {
        [Parameter]
        public string Id { get; set; }
        public GraphicsLayer graphicsLayer;
        private MapView? mapView;
        List<Randonnee> _randonnees;
        public int grosseurChunk = 200;

        private async Task OnViewInitialized()
        {
            graphicsLayer = new GraphicsLayer();
            Graphic graph;
            foreach (Randonnee rando in _randonnees)
            {
                Geometry point = GeneratePoint(rando);
                graph = new Graphic(point, GenerateSimpleMarker(), GeneratePopupTemplate(rando));
                graphicsLayer.Add(graph);
            }
            mapView.AddLayer(graphicsLayer);
        }
        protected override async Task OnInitializedAsync()
        {
            _randonnees = randonneeService.GetRandonnees();
        }
        private PopupTemplate GeneratePopupTemplate(Randonnee rando)
        {
            return new PopupTemplate(rando.Nom, $"<p>{rando.Description}</p><a href='https://randomaxx.azurewebsites.net/detail/{rando.Id}'>Voir les Détails</a>");
        }

        private Symbol GenerateSimpleMarker()
        {
            return new SimpleMarkerSymbol(new Outline(new MapColor(33, 25, 51)),
                new MapColor(67, 131, 168), 7);
        }
        private Point GeneratePoint(Randonnee rando)
        {
            return new Point(rando.EndroitDepart.Longitude, rando.EndroitDepart.Latitude);
        }
        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
    }
}
