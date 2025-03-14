using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using SB.Application;
using SB.Application.Features.Users.Commands;
using SB.Application.Services.Interface;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMediator _mediator;

    public UsersController(IUserService userService, IMediator mediator)
    {
        _userService = userService;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new { UserId = userId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return user != null ? Ok(user) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        await _userService.AddUserAsync(userDto);
        return CreatedAtAction(nameof(Get), new { id = userDto.Id }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UserDto userDto)
    {
        if (id.ToString() != userDto.Id.ToString()) return BadRequest();
        await _userService.UpdateUserAsync(userDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
