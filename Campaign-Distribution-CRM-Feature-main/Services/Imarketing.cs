using MarketingDemo.Entities;
using MarketingDemo.Repositories;
using MarketingDemo.Services;

public interface IMarketing
{
    Task CreerCampagne(CampaignMarketing campaign);
    // Updated to accept templateId
    Task<ResponseCampaign> DistribuerCampagne(int campaignId, int templateId, string channelType);
    Task<IEnumerable<Template>> ListTemplates();
    Task<IEnumerable<CampaignMarketing>> ListCampaigns();
    Task<IEnumerable<ResponseCampaign>> GetDistributionHistory();
}
