using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using TodoList.Dtos;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Route("api/Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext? _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
            if (_context is null )
            {
                throw new ArgumentNullException(nameof(context));
            }
        } 

        [HttpGet("GetTodos")]
        public IActionResult GetTodos()
        {
            var Todos = _context?.Todos.Select(t => t.ToGetTaskDto()).ToList() ;
            
            return Ok(Todos);
        }
        [HttpPost("CreateTodo")]
        public IActionResult CreateTodo(CreateTaskDTO Task)
        {
            Todo todo = new Todo {TaskName=Task.TaskName};
            _context?.Todos.Add(todo);
            if (_context?.SaveChanges()>0)
            { 

                return Ok("Created");
            }
            return BadRequest("Failed to create todo");


        }

        [HttpPut("UpdateTodo")]
        public IActionResult UpdateTodo(string Task)
        {
            return Ok("Updated");
        }

        [HttpDelete("DeleteTodo")]
        public IActionResult DeleteTodo(string Task)
        {
            return Ok("Deleted");

        }
       
        

    }
    public record CreateTaskDTO(string TaskName);
    public record UpdateTaskDTO(int Id,string TaskName);

    public record DeleteTaskDTO(int Id);


    

}
