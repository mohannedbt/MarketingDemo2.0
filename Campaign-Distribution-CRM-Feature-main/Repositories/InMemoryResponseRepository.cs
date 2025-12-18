using MarketingDemo.Entities;

namespace MarketingDemo.Repositories;

public class InMemoryResponseRepository : IResponseRepository {
    private readonly List<ResponseCampaign> _responses = new();

    public async Task AddResponseAsync(ResponseCampaign response) {
        _responses.Add(response);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<ResponseCampaign>> GetAllResponsesAsync() {
        return await Task.FromResult(_responses.OrderByDescending(r => r.DateEnvoi));
    }
}