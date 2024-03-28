using journal.api.Domain;
using journal.api.Dto;

namespace journal.api.service;

public class TradeStationService
{
    private readonly HttpClient _httpClient;

    public TradeStationService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.tradestation.com/v3/");
    }

    public async Task<IEnumerable<Order>> GetTodayOrders(string account) 
    {
        var response = await _httpClient.GetFromJsonAsync<TradeStationOrderResponse>(
        $"brokerage/accounts/{account}/orders");

        return response.Orders;
            
    }

    public async Task<IEnumerable<Order>> GetHistoricOrders(string account, DateOnly since) 
    {
        var response = await _httpClient.GetFromJsonAsync<TradeStationOrderResponse>(
        $"brokerage/accounts/{account}/historicalorders?since{since.ToString("MM-DD-YYY")}");

        return response.Orders;
    }
}