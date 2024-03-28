using journal.api.Domain;
using journal.api.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace journal.api.controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IJournalService _journalService;

    public OrdersController(IJournalService journalService)
    {
        _journalService = journalService;
    }

    [HttpGet("{account}")]
    public async Task<IActionResult> Get([FromRoute] string account)
    {
        var balance = await _journalService.CalculateTodayBalance(account);

        return Ok(balance);
    }
}
