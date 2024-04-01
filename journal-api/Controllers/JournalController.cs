using journal.api.Domain;
using journal.api.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace journal.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JournalController(IJournalService journalService) : ControllerBase
{
    private readonly IJournalService _journalService = journalService;

    [HttpGet("Today/{account}")]
    public async Task<IActionResult> GetTodayJournal([FromRoute] string account)
    {
        var balance = await _journalService.CalculateTodayBalance(account);

        return Ok(balance);
    }

    [HttpGet("Historical/{account}")]
    public async Task<IActionResult> GetHistoricalJournal([FromRoute] string account, [FromQuery] string since)
    {
        var parsed = DateOnly.TryParse(since, out var sinceParsed);
        var balance = await _journalService.CalculateHistoricalBalance(account, parsed ? sinceParsed: null);

        return Ok(balance);
    }
}
