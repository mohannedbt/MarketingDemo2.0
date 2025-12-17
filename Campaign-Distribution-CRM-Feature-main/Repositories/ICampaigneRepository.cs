using MarketingDemo.Entities;

namespace MarketingDemo.Repositories;

public interface ICampaigneRepository
{
    Task<CampaignMarketing> findByIdAsync(int id);
    Task<IEnumerable<CampaignMarketing>> listCampagne();
    Task AddCampaignAsync(CampaignMarketing campaign);
    Task ModifyCampgne(CampaignMarketing campaign);
}