using journal.api.service;
using Microsoft.AspNetCore.Mvc;

namespace journal.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(TradeStationService tradeStationService) :ControllerBase
{
    private readonly TradeStationService _tradeStationService = tradeStationService;

    [HttpGet]
    public async Task<IActionResult> Accounts()
    {
        return Ok(await _tradeStationService.GetAccounts());
    }
}