using MarketingDemo.Entities;
using MarketingDemo.Repositories;

namespace MarketingDemo.Services;

public class SocialMediaCampagneDistribution : DistributionCanalService
{
    public SocialMediaCampagneDistribution(
        ICampaigneRepository campaignRepo, 
        ICanalRepository canalRepo, 
        ITemplateRepository templateRepo)
        : base(campaignRepo, canalRepo, templateRepo) { }

    protected override async Task<Template> construireTemplate(CampaignMarketing campaign, CanalDistribution canal)
    {
        // CASTING: Access the specific properties defined in your Class Diagram
        var smCanal = canal as SocialMediaCanal;
        string platform = smCanal?.Platform ?? "Social Media";
        string audience = smCanal?.Audience ?? "General Public";

        return new Template 
        {
            // Using the 'Content' and 'ToWho' fields from your Diagram's template box
            Content = $"🚀 {campaign.Nom}: Join us on {platform}! for our compaign #{platform}",
            ToWho = $"Target Audience: {audience}"
        };
    }

    protected override async Task<ResponseCampaign> preparerDonnees(CampaignMarketing campaign, Template template)
    {
        // Aligned with ReponseCampagne in the diagram (ClientId, typeReponse, idReponse)
        return new ResponseCampaign 
        {
            IdResponse = new Random().Next(1001, 2000),
            CampaignId = campaign.IdCampagne,
            TypeReponse = "SocialPost", // Matches the diagram's typeReponse
            Statut = "Posted",
            ClientId = 0 // In a real CRM, this would link to a specific customer segment
        };
    }

    protected override async Task envoyer(int responseId)
    {
        // This is where you would call the TikTok/Facebook/LinkedIn API
        Console.WriteLine($"📱 [API DISPATCH] Successfully posted to Social Media. Internal Trace ID: {responseId}");
        await Task.CompletedTask;
    }
}