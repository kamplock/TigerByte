using System;
using Microsoft.AspNetCore.Mvc;
using TigerByte_Web_Copy.Services;
using TigerByte_Web_Copy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TigerByte_Web_Copy.Controllers;

[Controller]
[Route("api/[controller]")]
public class ProblemsController : Controller
{

    private readonly MongoDBService _mongoDBService;

    public ProblemsController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Problems>> GetProblems()
    {
        return await _mongoDBService.GetProblemsAsync();

    }

    [HttpPost]
    public async Task<IActionResult> PostProblem([FromBody] Problems problem)
    {
        await _mongoDBService.CreateProblemAsync(problem);
        return CreatedAtAction(nameof(GetProblems), new { id = problem.Id }, problem);

    }

    [HttpPut("{problemName}")]
    public async Task<IActionResult> AddToProblems(string problemName, string problem, string solution, string type, [FromBody] string problemsList)
    {
        await _mongoDBService.AddToProblemsAsync(problemName, problem, solution, type, problemsList);
        return NoContent();
    }

    [HttpDelete("{problemName}")]
    public async Task<IActionResult> DeleteProblem(string problemName)
    {
        await _mongoDBService.DeleteProblemAsync(problemName);
        return NoContent();

    }

}