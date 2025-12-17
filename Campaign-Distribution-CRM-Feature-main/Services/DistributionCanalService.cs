using MarketingDemo.Entities;
using MarketingDemo.Repositories;

namespace MarketingDemo.Services;

public abstract class DistributionCanalService
{
    protected readonly ICampaigneRepository _campaignRepo;
    protected readonly ICanalRepository _canalRepo;
    protected readonly ITemplateRepository _templateRepo;

    public DistributionCanalService(
        ICampaigneRepository campaignRepo,
        ICanalRepository canalRepo,
        ITemplateRepository templateRepo)
    {
        _campaignRepo = campaignRepo;
        _canalRepo = canalRepo;
        _templateRepo = templateRepo;
    }

    /// <summary>
    /// The Template Method: Defines the skeleton of the distribution process.
    /// This matches the 'distribuerCampagne()' flow in the class diagram.
    /// </summary>
    public async Task<ResponseCampaign> distribuerCampaign(int campaignId)
    {
        // Fetch the campaign using the ID
        var campaign = await _campaignRepo.findByIdAsync(campaignId);
        
        // Fetch the specific canal configuration (EmailCanal or SocialMediaCanal)
        var canal = await _canalRepo.findCanalByTypeAsync(campaign.CanalType);
        
        // 1. Hook: build specific content (construireTemplate in diagram)
        var template = await construireTemplate(campaign, canal); 
        
        // 2. Prepare the response trace (preparerDonnees in diagram)
        var response = await preparerDonnees(campaign, template);
        
        // 3. Hook: Actual delivery (envoyer in diagram)
        await envoyer(response.IdResponse); 
        
        // 4. Log the result (enregistrerTrace in diagram)
        await enregistrerTrace(response);
        
        return response;
    }

    // These abstract methods must be implemented by EmailCampagneDistribution 
    // and SocialMediaCampagneDistribution as shown in the diagram.
    
    protected abstract Task<Template> construireTemplate(CampaignMarketing campaign, CanalDistribution canal);
    
    protected abstract Task<ResponseCampaign> preparerDonnees(CampaignMarketing campaign, Template template);
    
    protected abstract Task envoyer(int responseId);

    // Virtual method for logging, providing a default implementation
    protected virtual Task enregistrerTrace(ResponseCampaign response) 
    {
        // Realistic CRM logic: Save the response to the ResponseRepository
        Console.WriteLine($"Trace recorded for Campaign {response.CampaignId} on {response.TypeReponse}");
        return Task.CompletedTask;
    }
}