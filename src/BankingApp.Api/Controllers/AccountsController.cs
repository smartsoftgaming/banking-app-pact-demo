using BankingApp.Application.Contracts;
using BankingApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace AccountProvider.Controllers;

[ApiController]
[Route("api/accounts")]
public sealed class AccountsController : ControllerBase
{
    private readonly IAccountRepository _repository;

    public AccountsController(IAccountRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{accountId:int}/balance")]
    [ProducesResponseType(typeof(BalanceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public IActionResult GetBalance([FromRoute] int accountId)
    {
        var account = _repository.GetById(accountId);

        if (account is null)
        {
            return NotFound(new ErrorResponse { Message = "Account not found" });
        }

        return Ok(new BalanceResponse
        {
            AccountId = account.AccountId,
            Balance = account.Balance,
            Currency = account.Currency
        });
    }

    [HttpGet("{accountId:int}/overdraft")]
    [ProducesResponseType(typeof(OverdraftResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public IActionResult GetOverdraft([FromRoute] int accountId)
    {
        var account = _repository.GetById(accountId);
        if (account is null)
        {
            return NotFound(new ErrorResponse { Message = "Account not found" });
        }

        return Ok(new OverdraftResponse
        {
            AccountId = account.AccountId,
            Overdraft = account.Overdraft,
            Currency = account.Currency
        });
    }
}
