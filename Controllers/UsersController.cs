using System;
using Microsoft.AspNetCore.Mvc;
using TigerByte_Web_Copy.Services;
using TigerByte_Web_Copy.Models;
namespace TigerByte_Web_Copy.Controllers;
using Microsoft.AspNetCore.Http;


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
    public async Task<List<Users>> GetUsers()
    {
        return await _mongoDBService.GetUsersAsync();

    }

    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] Users user)
    {
        await _mongoDBService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);

    }

    [HttpPut("{email}")]
    public async Task<IActionResult> AddToUsers(string email, [FromBody] string usersList)
    {
        await _mongoDBService.AddToUsersAsync(email, usersList);
        return NoContent();

    }

    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        await _mongoDBService.DeleteUserAsync(email);
        return NoContent();
    }

}