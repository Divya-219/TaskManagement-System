using Microsoft.AspNetCore.Mvc;
using taskManagement.Domain.Entities;
using TaskManagement.Application.Abstractions.Persistence;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Services;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskService _service;

    public TaskController(TaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _service.GetAllTaskAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var createdTask = await _service.CreateAsync(request.Title, request.Description);
        return Ok(createdTask);
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> MarkTaskCompleted(int id)
    {
        await _service.MarkTaskCompleted(id);
        return Ok("Task marked as completed");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskRequest request)
    {
        await _service.UpdateTaskAsync(id, request.Title, request.Description);
        return Ok();
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
