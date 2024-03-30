using journal.api.Domain;

namespace journal.api.Dto;

class TradeStationOrderResponse
{
    public IEnumerable<Order> Orders { get; set; } = Enumerable.Empty<Order>();

    public IEnumerable<dynamic> Errors { get; set; } = Enumerable.Empty<dynamic>();

    public string NextToken { get; set; }
}