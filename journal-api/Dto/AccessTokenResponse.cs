using System.Text.Json.Serialization;
namespace journal.api.Dto;

public class AccessTokenResponse()
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}