using MarketingDemo.Entities;

namespace MarketingDemo.Repositories;

// Inside your Repositories folder
public interface IResponseRepository {
    Task AddResponseAsync(ResponseCampaign response);
    Task<IEnumerable<ResponseCampaign>> GetAllResponsesAsync();
}

