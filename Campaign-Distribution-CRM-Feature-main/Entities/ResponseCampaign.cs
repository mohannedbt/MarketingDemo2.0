namespace MarketingDemo.Entities;

public class ResponseCampaign
{
    // Matches 'idReponse' in diagram
    public int IdResponse { get; set; } 
    
    // Matches 'ClientId' in diagram
    public int ClientId { get; set; } 
    
    // Matches 'typeReponse' in diagram
    public string TypeReponse { get; set; } = string.Empty; 
    
    public int CampaignId { get; set; }
    public string Statut { get; set; } = string.Empty;
    public DateTime DateEnvoi { get; set; } = DateTime.UtcNow;
}