using System.Reflection.Metadata;
using projet_rando_web.Classes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using projet_rando_web.Data;
using dymaptic.GeoBlazor.Core.Components.Layers;
using dymaptic.GeoBlazor.Core.Components.Views;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using projet_rando_web.Interfaces;
using dymaptic.GeoBlazor.Core.Components.Geometries;
using System.Dynamic;
using dymaptic.GeoBlazor.Core.Components;

namespace projet_rando_web.Pages
{
    public partial class DetailRandonnee
    {
        Randonnee randonnee;
        private string randonneeId;

        [Parameter]
        public string Id { get; set; }
        public GraphicsLayer graphicsLayer;
        private MapView? mapView;

        protected override async Task OnInitializedAsync()
        {
            randonnee = randonneeService.GetRandonnee(Id);
        }
        private async Task OnViewRendered()
        {
            Geometry geo = new dymaptic.GeoBlazor.Core.Components.Geometries.Point(randonnee.EndroitDepart.Longitude, randonnee.EndroitDepart.Latitude);
            
            graphicsLayer = new GraphicsLayer();
            Graphic graph1;
            graph1 = new Graphic(geo);
            graphicsLayer.Add(graph1);
            mapView.AddLayer(graphicsLayer);
        }
    }

}