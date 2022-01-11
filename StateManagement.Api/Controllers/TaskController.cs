using Microsoft.AspNetCore.Mvc;
using StateManagement.Application.Task;
using StateManagement.Contract.Task.Requests;

namespace StateManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var result = await _taskService.GetTaskAsync(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostTaskRequest request)
    {
        var result = await _taskService.CreateTaskAsync(request);

        return Created($"task/{result.Id}", result);
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> Patch([FromRoute] long id, [FromBody] PatchTaskRequest request)
    {
        if (request.Id != id) return BadRequest();

        var result = await _taskService.UpdateTaskAsync(request);

        return Ok(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        await _taskService.DeleteTaskAsync(id);

        return NoContent();
    }
}