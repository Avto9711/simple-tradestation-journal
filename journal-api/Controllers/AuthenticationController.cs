using journal.api.Dto;
using journal.api.service;
using Microsoft.AspNetCore.Mvc;

namespace journal.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IJournalService journalService) : ControllerBase
{
    private readonly IJournalService _journalService = journalService;

    [HttpGet("Token")]
    public async Task<IActionResult> Token([FromBody] AccessTokenRequest accessTokenRequest)
    {
        // call https://signin.tradestation.com/oauth/token with the token from the UI and return the bearer token

        return Ok();
    }

    [HttpGet("Refresh")]
    public async Task<IActionResult> Refresh([FromBody] AccessTokenRequest accessTokenRequest)
    {
        return Ok();
    }
}
