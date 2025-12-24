namespace TodoList.Dtos
{

    public class GetTaskDto
    {
       public required string TaskName { get; set; }
       public required int Id { get; set; }
        public required string IsCompleted{ get; set; }

    }



}
