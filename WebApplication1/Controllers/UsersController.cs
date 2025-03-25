using MediatR;
using Microsoft.AspNetCore.Mvc;
using SB.Application;
using SB.Application.Features.Users.Commands;
using SB.Application.Services.Implementation;
using SB.Application.Services.Interface;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService<EmployeeUser> _employeeService;
    private readonly IUserService<EmployerUser> _employerService;
    private readonly IMediator _mediator;

    public UsersController(IUserService<EmployeeUser> userService, IUserService<EmployerUser> employerService, IMediator mediator)
    {
        _employeeService = userService;
        _employerService = employerService;
        _mediator = mediator;
    }

    [HttpPost("registerEmployee")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterUserCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid user data.");
        }

        var userId = await _mediator.Send(command);
        return Ok(new { UserId = userId });        
    }

    [HttpPost("registerEmployer")]
    public async Task<IActionResult> RegisterEmployer([FromBody] RegisterEmployerCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid user data.");
        }

        var userId = await _mediator.Send(command);
        return Ok(new { UserId = userId });
    }

   
    [HttpGet("Employee/{id}")]
    public async Task<IActionResult> GetEmployeeById(string id)
    {
        var employer = await _employerService.GetUserByIdAsync(id);
        if (employer != null) return Ok(employer);

        return NotFound($"User with ID {id} not found.");
    }

    [HttpGet("Employer/{id}")]
    public async Task<IActionResult> GetEmployerById(string id)
    {
        var employee = await _employeeService.GetUserByIdAsync(id);
        if (employee != null) return Ok(employee);

        return NotFound($"User with ID {id} not found.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _employeeService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] EmployeeUser userDto)
    {
        if (id.ToString() != userDto.Id.ToString()) return BadRequest();
        await _employeeService.UpdateUserAsync(userDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _employeeService.DeleteUserAsync(id);
        return NoContent();
    }
}
