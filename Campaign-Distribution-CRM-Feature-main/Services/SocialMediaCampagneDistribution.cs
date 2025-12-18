using MarketingDemo.Entities;
using MarketingDemo.Repositories;

namespace MarketingDemo.Services;

public class SocialMediaCampagneDistribution : DistributionCanalService
{
    public SocialMediaCampagneDistribution(
        ICampaigneRepository campaignRepo, 
        ICanalRepository canalRepo, 
        ITemplateRepository templateRepo,IResponseRepository responseRepo)
        : base(campaignRepo, canalRepo, templateRepo,responseRepo) { }

    /// <summary>
    /// Now uses the 'chosenTemplate' provided by the base service (from UI selection)
    /// </summary>
    protected override async Task<Template> construireTemplate(CampaignMarketing campaign, CanalDistribution canal, Template chosenTemplate)
    {
        // CASTING: Access the specific properties defined in your Class Diagram
        var smCanal = canal as SocialMediaCanal;
        string platform = smCanal?.Platform ?? "Social Media";

        // Logic: Transform the chosen text into a social-media-friendly post with hashtags
        string hashTag = campaign.Nom.Replace(" ", "");
        string finalPost = $"🚀 {chosenTemplate.Content} \n\nCheck us out on {platform}! #{platform} #{hashTag}";

        return new Template 
        {
            Id = chosenTemplate.Id,
            Content = finalPost,
            ToWho = $"Audience Group: {smCanal?.Audience ?? "General Public"}"
        };
    }

    protected override async Task<ResponseCampaign> preparerDonnees(CampaignMarketing campaign, Template template)
    {
        return new ResponseCampaign 
        {
            IdResponse = new Random().Next(1001, 2000),
            CampaignId = campaign.IdCampagne,
            TypeReponse = "SocialMedia",
            Statut = "Successfully Published",
            DateEnvoi = DateTime.Now
        };
    }

    protected override async Task envoyer(int responseId)
    {
        // Simulating the API Call (TikTok, LinkedIn, or Facebook API)
        Console.WriteLine($"📱 [API DISPATCH] Content posted to platform API. Trace ID: {responseId}");
        await Task.CompletedTask;
    }
}