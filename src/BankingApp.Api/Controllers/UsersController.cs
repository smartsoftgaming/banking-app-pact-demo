using BankingApp.Application.Contracts;
using BankingApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountProvider.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{userId:int}/details")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] int userId)
    {
        var user = _repository.GetById(userId);

        if (user is null)
        {
            return NotFound(new ErrorResponse { Message = "User not found" });
        }

        return Ok(new UserResponse
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
        });
    }
}