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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmail([FromBody] Users user, string id)
    {

        await _mongoDBService.UpdateUserAsync(id, user.Email, user.Name);
        return NoContent();

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _mongoDBService.DeleteUserAsync(id);
        return NoContent();
    }

}