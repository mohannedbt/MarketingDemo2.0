using MarketingDemo.Entities;
using MarketingDemo.Repositories;

namespace MarketingDemo.Services;

public class CampagneMarketingService : IMarketing
{
    private readonly ICampaigneRepository _campaignRepo;
    private readonly ITemplateRepository _templateRepo;
    private readonly IResponseRepository _responseRepo;
    private readonly IEnumerable<DistributionCanalService> _channels;
    
    // 1. Inject the observers via the constructor (Modern DI approach)
    private readonly IEnumerable<ICampaignObserver> _observers;

    public CampagneMarketingService(
        ICampaigneRepository campaignRepo, 
        ITemplateRepository templateRepo,
        IEnumerable<DistributionCanalService> channels, 
        IResponseRepository responseRepo,
        IEnumerable<ICampaignObserver> observers) // Added here
    {
        _campaignRepo = campaignRepo;
        _templateRepo = templateRepo;
        _channels = channels;
        _responseRepo = responseRepo;
        _observers = observers;
    }

    public async Task<ResponseCampaign> DistribuerCampagne(int campaignId, int templateId, string channelType)
    {
        // 1. This calls your abstract method which DOES the _responseRepo.AddResponseAsync(response)
        var response = await _channels
            .FirstOrDefault(c => c.GetType().Name.StartsWith(channelType, StringComparison.OrdinalIgnoreCase))
            .distribuerCampaign(campaignId, templateId);

        // 2. Get the actual objects from repositories for the observers
        var campaign = await _campaignRepo.findByIdAsync(campaignId);
        var template = await _templateRepo.getByIdAsync(templateId);

        // 3. Filter and Notify Observers
        var relevantObservers = _observers.Where(o => 
            o.SupportedChannel.Equals(channelType, StringComparison.OrdinalIgnoreCase));

        foreach (var observer in relevantObservers)
        {
            await observer.NotifyAsync(campaign.Nom, template.Content);
        
            // 4. THE FIX: Add the name to the response object.
            // Since 'response' is the same instance stored in your In-Memory List,
            // this change is "live" and will be visible in the History view and Result view.
            string platformName = observer.GetType().Name.Replace("Observer", "");
            response.NotifiedPlatforms.Add(platformName);
        }
    
        return response;
    }

    // --- Other methods remain clean and focused ---
    public async Task<IEnumerable<Template>> ListTemplates() => await _templateRepo.getAllAsync();
    public async Task<IEnumerable<CampaignMarketing>> ListCampaigns() => await _campaignRepo.listCampagne();
    public async Task CreerCampagne(CampaignMarketing campaign) => await _campaignRepo.AddCampaignAsync(campaign);
    public async Task<IEnumerable<ResponseCampaign>> GetDistributionHistory() => await _responseRepo.GetAllResponsesAsync();
}