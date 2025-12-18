using MarketingDemo.Entities;
using MarketingDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketingDemo.Controllers;

[Route("campaign")]
public class CampaignController : Controller
{
    private readonly IMarketing _marketingService;

    public CampaignController(IMarketing marketingService)
    {
        _marketingService = marketingService;
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        // Centralized calls to the service
        ViewBag.Campaigns = await _marketingService.ListCampaigns();
        ViewBag.Templates = await _marketingService.ListTemplates();
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(int campaignId, int templateId, string channelType)
    {
        try
        {
            // The service now handles template finding and execution logic
            var response = await _marketingService.DistribuerCampagne(campaignId, templateId, channelType);
            
            return View("Result", response);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Campaigns = await _marketingService.ListCampaigns();
            ViewBag.Templates = await _marketingService.ListTemplates();
            return View("Create");
        }
    }
    [HttpGet("history")]
    public async Task<IActionResult> History()
    {
        // Simple and clean: the service does the work
        var history = await _marketingService.GetDistributionHistory();
    
        // This will now contain the Love Scores and Sentiment we added!
        return View(history);
    }
}