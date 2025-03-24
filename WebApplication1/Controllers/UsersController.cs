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
  // private readonly SB.Application.Services.Interface.IUserService<SB.Domain.Entities.User> _userService;
    private readonly IMediator _mediator;

    public UsersController(IUserService<EmployeeUser> userService, IUserService<EmployerUser> _employerService, IMediator mediator)
    {
        _employeeService = userService;
        _employerService = _employerService;
        _mediator = mediator;
    }

    //[HttpPost("register")]
    //public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    //{
    //    var userId = await _mediator.Send(command);
    //    return Ok(new { UserId = userId });
    //}

    

    [HttpPost("registerEmployee")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterUserCommand command)
    {
        //if (userDto == null)
        //{
        //    return BadRequest("Invalid user data.");
        //}

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

    //[HttpGet("{id}")]
    //public async Task<IActionResult> Get(string id)
    //{
    //    var user = await _employeeService.GetUserByIdAsync(id);
    //    return user != null ? Ok(user) : NotFound();
    //}

    //[HttpPost]
    //public async Task<IActionResult> Create([FromBody] EmployeeUser userDto)
    //{
    //    await _employeeService.AddUserAsync(userDto);
    //    return CreatedAtAction(nameof(Get), new { id = userDto.Id }, userDto);
    //}

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
