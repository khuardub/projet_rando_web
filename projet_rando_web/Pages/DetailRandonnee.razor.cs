using System.Reflection.Metadata;
using projet_rando_web.Classes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using projet_rando_web.Data;
using dymaptic.GeoBlazor.Core.Components.Layers;
using dymaptic.GeoBlazor.Core.Components.Views;
using System.Collections.Generic;
using System.Threading;
using projet_rando_web.Interfaces;
using dymaptic.GeoBlazor.Core.Components.Geometries;
using System.Dynamic;
using dymaptic.GeoBlazor.Core.Components;
using dymaptic.GeoBlazor.Core.Components.Popups;
using dymaptic.GeoBlazor.Core.Components.Symbols;
using dymaptic.GeoBlazor.Core.Objects;

namespace projet_rando_web.Pages
{
    public partial class DetailRandonnee
    {
        Randonnee randonnee;
        private string randonneeId;

        [Parameter]
        public string Id { get; set; }
        public int grosseurChunk = 100;
        public GraphicsLayer graphicsLayer;
        private MapView? mapView;

        protected override async Task OnInitializedAsync()
        {
            randonnee = randonneeService.GetRandonnee(Id);
        }
        private async Task OnMapRendered()
        {
            graphicsLayer = new GraphicsLayer();
            Graphic graph;
            Geometry point = GeneratePoint(randonnee);
            graph = new Graphic(point, GenerateSimpleMarker());
            graphicsLayer.Add(graph);
            mapView.AddLayer(graphicsLayer);
        }
        private Symbol GenerateSimpleMarker()
        {
            return new SimpleMarkerSymbol(new Outline(new MapColor(33, 25, 51)),
                new MapColor(67, 131, 168), 10);
        }
        private Point GeneratePoint(Randonnee rando)
        {
            return new Point(rando.EndroitDepart.Longitude, rando.EndroitDepart.Latitude);
        }
    }

}