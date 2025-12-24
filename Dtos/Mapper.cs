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

    }
}
