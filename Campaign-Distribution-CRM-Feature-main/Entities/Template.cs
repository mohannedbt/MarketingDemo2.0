namespace MarketingDemo.Entities;

public class Template
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string ToWho { get; set; } = string.Empty; // Target audience description
}