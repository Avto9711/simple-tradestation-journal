using journal.api.Dto;
using journal.api.service;
using journal.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace journal.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IHttpClientFactory httpClientFactory, IOptions<TradeStationConfig> tradeStationConfig) : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IOptions<TradeStationConfig> _tradeStationConfig = tradeStationConfig;

    [HttpPost("Token")]
    public async Task<IActionResult> Token([FromBody] AccessTokenRequest accessTokenRequest)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var dict = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "client_id", _tradeStationConfig.Value.ClientId },
            { "client_secret", _tradeStationConfig.Value.SecretKey },
            { "code", accessTokenRequest.Code },
            { "redirect_uri", accessTokenRequest.RedirectUrl },
        };

        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            _tradeStationConfig.Value.IdentityServerUrl)
        {
            Content = new FormUrlEncodedContent(dict)
        };

        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        httpResponseMessage.EnsureSuccessStatusCode();

        var response =  await httpResponseMessage.Content.ReadFromJsonAsync<AccessTokenResponse>();

        return Ok(response);
    }

    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh([FromBody] AccessTokenRequest accessTokenRequest)
    {
        return Ok();
    }
}
