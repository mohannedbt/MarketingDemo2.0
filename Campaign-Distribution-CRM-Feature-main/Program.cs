using Microsoft.AspNetCore.Mvc;
using MarketingDemo.Repositories;
using MarketingDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services for MVC (Views + Controllers)
builder.Services.AddControllersWithViews();

// --- 1. Register Repositories as SINGLETON ---
// This ensures your in-memory lists persist while the app is running
builder.Services.AddSingleton<ICampaigneRepository, InMemoryCampaignRepository>();
builder.Services.AddSingleton<ICanalRepository, InMemoryCanalRepository>();
builder.Services.AddSingleton<ITemplateRepository, InMemoryTemplateRepository>();
// builder.Services.AddSingleton<IResponseRepository, InMemoryResponseRepository>(); // Uncomment if you created this

// --- 2. Register Distribution Strategies ---
// Registering them this way allows the Controller to receive 
// IEnumerable<DistributionCanalService> and pick the right one.
builder.Services.AddScoped<DistributionCanalService, EmailCampagneDistribution>();
builder.Services.AddScoped<DistributionCanalService, SocialMediaCampagneDistribution>();

var app = builder.Build();

// --- 3. Pipeline Configuration ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // This allows the browser to download bootstrap.cssapp.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Campaign}/{action=Create}/{id?}"); // Point to your campaign creator by default

app.Run();