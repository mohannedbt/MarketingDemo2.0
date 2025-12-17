using Microsoft.AspNetCore.Mvc;
using MarketingDemo.Repositories;
using MarketingDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services for MVC (Views + Controllers)
builder.Services.AddControllersWithViews();

// --- 1. Register Repositories as SINGLETON ---
builder.Services.AddSingleton<ICampaigneRepository, InMemoryCampaignRepository>();
builder.Services.AddSingleton<ICanalRepository, InMemoryCanalRepository>();
builder.Services.AddSingleton<ITemplateRepository, InMemoryTemplateRepository>();

// --- 2. Register The Orchestrator Service (NEW) ---
// This connects the Controller to the logic layer
builder.Services.AddScoped<IMarketing, CampagneMarketingService>();

// --- 3. Register Distribution Strategies ---
builder.Services.AddScoped<DistributionCanalService, EmailCampagneDistribution>();
builder.Services.AddScoped<DistributionCanalService, SocialMediaCampagneDistribution>();

var app = builder.Build();

// --- 4. Pipeline Configuration ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseRouting();
app.UseAuthorization();

// Updated routing to match your Controller name
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Campaign}/{action=Create}/{id?}");

app.Run();