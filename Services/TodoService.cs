using TodoList.Models;
using TodoList.Dtos; 

namespace TodoList.Services
{
    public class TodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public List<GetTaskDto> GetAllTodos()
        {
            return _context.Todos.Select(t => t.ToGetTaskDto()).ToList();
        }

        public GetTaskDto? CreateTodo(CreateTaskDTO dto)
        {
            var exists = _context.Todos.Any(t => t.TaskName.ToLower() == dto.TaskName.ToLower());
            if (exists)
            {
                return null;
            }

            var todoModel = dto.ToTodoFromCreateDTO();
            _context.Todos.Add(todoModel);
            _context.SaveChanges();

            return todoModel.ToGetTaskDto();
        }


        public bool UpdateTodo(UpdateTaskDTO dto)
        {
            var existingTodo = _context.Todos.Find(dto.Id);
            if (existingTodo == null) return false;

            existingTodo.UpdateTodoFromDTO(dto); 

            _context.SaveChanges();
            return true;
        }

        public bool DeleteTodo(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null) return false;

            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return true;
        }
        public bool ToggleStatus(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null) return false;

            todo.IsCompleted = !todo.IsCompleted; 
            _context.SaveChanges();
            return true;
        }
        
    }
}