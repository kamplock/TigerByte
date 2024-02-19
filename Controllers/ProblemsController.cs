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

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToProblems([FromBody] Problems problem, string id)
    {
        await _mongoDBService.UpdateProblemsAsync(id, problem.ProblemName, problem.Problem, problem.Solution, problem.Type);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProblem(string id)
    {
        await _mongoDBService.DeleteProblemAsync(id);
        return NoContent();

    }

}