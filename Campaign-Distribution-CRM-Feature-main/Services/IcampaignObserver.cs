namespace MarketingDemo.Services;

public interface ICampaignObserver
{
    string SupportedChannel { get; } // "Email", "SocialMedia", or "All"
    Task NotifyAsync(string campaignName, string content);
}