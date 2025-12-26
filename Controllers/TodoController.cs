using Microsoft.AspNetCore.Mvc;
using TodoList.Dtos;
using TodoList.Services;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_todoService.GetAllTodos());
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateTaskDTO dto)
        {
            var newTask = _todoService.CreateTodo(dto);

            if (newTask == null)
            {
                return BadRequest("A task with this name already exists.");
            }

            return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateTaskDTO dto)
        {
            if (!_todoService.UpdateTodo(dto)) return NotFound();
            return Ok("Updated");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(DeleteTaskDTO dto)
        {
            if (!_todoService.DeleteTodo(dto.Id)) return NotFound();
            return Ok("Deleted");
        }
        [HttpPatch("Toggle/{id}")]
        public IActionResult Toggle(int id)
        {
            if (!_todoService.ToggleStatus(id)) return NotFound();
            return Ok("Status updated");
        }
    }
}