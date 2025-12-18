namespace MarketingDemo.Services;
public class FacebookObserver : ICampaignObserver
{
    public string SupportedChannel => "SocialMedia";
    public async Task NotifyAsync(string campaignName, string content)
    {
        Console.WriteLine($"📱 [Facebook API] Post created for: {campaignName}");
        await Task.CompletedTask;
    }
}

public class InstagramObserver : ICampaignObserver
{
    // Tells the Orchestrator to only call this for Social Media campaigns
    public string SupportedChannel => "SocialMedia";

    public async Task NotifyAsync(string campaignName, string content)
    {
        // Simulate API interaction with Instagram Business Graph API
        Console.WriteLine($"📸 [Instagram API] Story/Post draft created for campaign: {campaignName}");
        
        // In a real CRM, you might log the specific API response status here
        await Task.CompletedTask;
    }
}
public class EmailAnalyticsObserver : ICampaignObserver
{
    public string SupportedChannel => "Email";
    public async Task NotifyAsync(string campaignName, string content)
    {
        Console.WriteLine($"📧 [Email API] Tracking pixels initialized for: {campaignName}");
        await Task.CompletedTask;
    }
}