namespace journal.Settings;

public class TradeStationConfig()
{
    public const string Key = "TradeStation";
    public string ClientId { get; set; }
    public string SecretKey { get; set; }
    public string IdentityServerUrl { get; set; }
}