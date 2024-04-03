using journal.api.Domain;
using journal.api.Dto;
using Microsoft.Identity.Client;

namespace journal.api.service;

public class TradeStationService
{
    private readonly HttpClient _httpClient;

    public TradeStationService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.tradestation.com/v3/");
    }

    public async Task<IEnumerable<Order>> GetTodayOrders(string account) => 
        await LoadAllOrders($"brokerage/accounts/{account}/orders?"); 

    public async Task<IEnumerable<Order>> GetHistoricOrders(string account, DateOnly since) => 
        await LoadAllOrders($"brokerage/accounts/{account}/historicalorders?since={since.ToString("MM-dd-yyyy")}&");
    
    public async Task<IEnumerable<Account>> GetAccounts() => 
        (await _httpClient.GetFromJsonAsync<GetAccountsResponse>("brokerage/accounts")).Accounts;
    

    //TODO: This logic can be taken outside to another service
    private async Task<IEnumerable<Order>> LoadAllOrders(string endpoint)
    {
        var pageSize = 250;
        var orders = new List<Order>();
        string nextToken = null;

        var endpointWithPage = $"{endpoint}pageSize={pageSize}";

        do{
            var endpointWithPageAndToken = string.IsNullOrEmpty(nextToken) ? endpointWithPage : endpointWithPage + $"&nextToken={nextToken}";
            var response = await _httpClient.GetFromJsonAsync<TradeStationOrderResponse>(endpointWithPageAndToken);
            orders.AddRange(response.Orders);
            nextToken = response.NextToken;
        }while(nextToken is not null);

        return orders;
    }
}