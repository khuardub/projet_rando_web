using dymaptic.GeoBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.StaticFiles;
using projet_rando_web.Data;
using projet_rando_web.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddGeoBlazor();
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.Configure<RandoMaxDatabaseSettings>(
builder.Configuration.GetSection("RandoMaxDatabase"));
builder.Services.AddScoped<IRandonnee, RandonneeService>();
builder.Services.AddScoped<IVille, VilleService>();
builder.Services.AddScoped<IUtilisateur, UtilisateurService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".wsv"] = "application/octet-stream";

app.UseStaticFiles();
// NOTE: for some reason, you still need the plain "UseStaticFiles" call above
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
