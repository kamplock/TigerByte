using System;
using Microsoft.AspNetCore.Mvc;
using TigerByte_API.Services;
using TigerByte_API.Models;
namespace TigerByte_API.Controllers;

[Controller]
[Route("api/[controller]")]
public class UsersController : Controller
{

    private readonly MongoDBService _mongoDBService;

    public UsersController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Users>> GetUsers() {
        return await _mongoDBService.GetUsersAsync();

    }

    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] Users user) {
        await _mongoDBService.CreateUserAsync(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);

    }

    [HttpPut("{email}")]
    public async Task<IActionResult> AddToUsers(string email, [FromBody] string usersList) {
        await _mongoDBService.AddToUsersAsync(email, usersList);
        return NoContent();

    }

    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteUser(string email) {
        await _mongoDBService.DeleteUserAsync(email);
        return NoContent();
    }

}