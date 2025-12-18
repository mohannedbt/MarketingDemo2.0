using MarketingDemo.Entities;
using MarketingDemo.Repositories;

namespace MarketingDemo.Services;

public class EmailCampagneDistribution : DistributionCanalService
{
    public EmailCampagneDistribution(
        ICampaigneRepository campaignRepo, 
        ICanalRepository canalRepo, 
        ITemplateRepository templateRepo,IResponseRepository responseRepo)
        : base(campaignRepo, canalRepo, templateRepo, responseRepo) { }

    /// <summary>
    /// Now uses the 'chosenTemplate' provided by the base service (from UI selection)
    /// </summary>
    protected override async Task<Template> construireTemplate(CampaignMarketing campaign, CanalDistribution canal, Template chosenTemplate)
    {
        var emailCanal = canal as EmailCanal;
        
        // Use the actual Content selected by the user in the UI
        string finalContent = $"Subject: {emailCanal?.Objet ?? "Update"} \n\n" +
                              $"Hello, \n" +
                              $"{chosenTemplate.Content} \n\n" +
                              $"Best regards, {campaign.Nom} Team.";

        return new Template 
        {
            Id = chosenTemplate.Id,
            Content = finalContent,
            ToWho = chosenTemplate.ToWho // Preserve the audience defined in the template
        };
    }

    protected override async Task<ResponseCampaign> preparerDonnees(CampaignMarketing campaign, Template template)
    {
        return new ResponseCampaign 
        {
            IdResponse = new Random().Next(1000, 9999),
            CampaignId = campaign.IdCampagne,
            TypeReponse = "Email",
            Statut = "Sent Successfully", // Transition from 'Queued' to 'Sent'
            DateEnvoi = DateTime.Now
        };
    }

    protected override async Task envoyer(int responseId)
    {
        // Simulating SMTP integration
        Console.WriteLine($"📧 [SMTP] Email sent via EmailCanal logic. Trace ID: {responseId}");
        await Task.CompletedTask;
    }
}