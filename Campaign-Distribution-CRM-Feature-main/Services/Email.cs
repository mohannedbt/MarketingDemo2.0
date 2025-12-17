using MarketingDemo.Entities;
using MarketingDemo.Repositories;

namespace MarketingDemo.Services;

public class EmailCampagneDistribution : DistributionCanalService
{
    public EmailCampagneDistribution(
        ICampaigneRepository campaignRepo, 
        ICanalRepository canalRepo, 
        ITemplateRepository templateRepo)
        : base(campaignRepo, canalRepo, templateRepo) { }

    protected override async Task<Template> construireTemplate(CampaignMarketing campaign, CanalDistribution canal)
    {
        // CASTING: Access the specialized EmailCanal from your diagram
        var emailCanal = canal as EmailCanal;
        
        // Realistic CRM: Merging campaign data with canal-specific data (Objet)
        return new Template 
        {
            Content = $"Subject: {emailCanal?.Objet ?? "Special Offer"} \n\n" +
                      $"Dear Client, our campaign '{campaign.Nom}' has launched! " +
                      $"Goal: hit our objective",
            ToWho = $"Mailing List: {string.Join(", ", emailCanal?.ListeEnvoi ?? new List<string> { "Subscribers" })}"
        };
    }

    protected override async Task<ResponseCampaign> preparerDonnees(CampaignMarketing campaign, Template template)
    {
        // Mapping to ReponseCampagne entity from the diagram
        return new ResponseCampaign 
        {
            IdResponse = new Random().Next(1, 1000), // idReponse in diagram
            CampaignId = campaign.IdCampagne,
            TypeReponse = "Email", // typeReponse in diagram
            Statut = "Queued",
            ClientId = 0 // In a real CRM, this would be the specific recipient ID
        };
    }

    protected override async Task envoyer(int responseId)
    {
        // This is the "Hook" where you would integrate with an SMTP service like SendGrid
        Console.WriteLine($"📧 [SMTP Service] Dispatching emails to the list. Tracking Trace: {responseId}");
        await Task.CompletedTask;
    }
}