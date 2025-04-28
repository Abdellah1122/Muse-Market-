
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MuseMarket;
using MuseMarket.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7148/") });

builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<CategorieOeuvreService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<SousCategorieOeuvreService>();
builder.Services.AddScoped<IImageComparisonService, ImageComparisonService>();
builder.Services.AddScoped<OeuvreService>();
builder.Services.AddScoped<ArtFormService>();
builder.Services.AddScoped<VilleService>();
builder.Services.AddScoped<VenteService>();
builder.Services.AddScoped<CommentaireService>();
builder.Services.AddScoped<CommisionService>();


builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
