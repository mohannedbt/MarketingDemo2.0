using MarketingDemo.Entities;
using MarketingDemo.Repositories;
using MarketingDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketingDemo.Controllers;

[Route("campaign")]
public class CampaignController : Controller
{
    private readonly ICampaigneRepository _campaignRepo;
    private readonly ITemplateRepository _templateRepo;
    private readonly IEnumerable<DistributionCanalService> _channels;

    public CampaignController(
        ICampaigneRepository campaignRepo, 
        ITemplateRepository templateRepo,
        IEnumerable<DistributionCanalService> channels)
    {
        _campaignRepo = campaignRepo;
        _templateRepo = templateRepo;
        _channels = channels;
    }

    // GET: /campaign/create
    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        // Load data from repositories to populate the dropdowns/lists in the view
        ViewBag.Campaigns = await _campaignRepo.listCampagne();
        ViewBag.Templates = await _templateRepo.getAllAsync();
        
        return View();
    }

    // POST: /campaign/create
    [HttpPost("create")]
    public async Task<IActionResult> Create(int campaignId, int templateId, string channelType)
    {
        try
        {
            // 1. Find the specific strategy implementation (Email or SocialMedia)
            // Based on the class names: EmailCampagneDistribution or SocialMediaCampagneDistribution
            var strategy = _channels.FirstOrDefault(c => 
                c.GetType().Name.StartsWith(channelType, StringComparison.OrdinalIgnoreCase));
            
            if (strategy == null) 
                throw new Exception($"The channel '{channelType}' is not currently supported.");

            // 2. Execute the Template Method distribution
            // This handles building the template, sending, and logging the trace
            var response = await strategy.distribuerCampaign(campaignId);
            
            // 3. Return the Result view with the ResponseCampaign (the trace)
            return View("Result", response);
        }
        catch (Exception ex)
        {
            // If something fails, reload the data so the form isn't empty on return
            ViewBag.Error = ex.Message;
            ViewBag.Campaigns = await _campaignRepo.listCampagne();
            ViewBag.Templates = await _templateRepo.getAllAsync();
            
            return View("Create");
        }
    }
}