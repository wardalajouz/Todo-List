namespace TodoList.Dtos
{
    public static class Mapper
    {
        public static GetTaskDto ToGetTaskDto(this Models.Todo todo)
        {
            return new GetTaskDto
            {
                Id = todo.Id,
                TaskName = todo.TaskName ?? string.Empty,
                IsCompleted = todo.IsCompleted ? "Yes" : "No"
            };
        }

        // 2. For CREATE (Add this!)
        public static Models.Todo ToTodoFromCreateDTO(this CreateTaskDTO dto)
        {
            return new Models.Todo
            {
                TaskName = dto.TaskName
            };
        }

        // 3. For UPDATE (Add this!)
        public static void UpdateTodoFromDTO(this Models.Todo existingTodo, UpdateTaskDTO dto)
        {
            existingTodo.TaskName = dto.TaskName;
        }
    }
}