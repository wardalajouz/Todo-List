using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class Todo
    {

        public int Id { get; set; }
        public string?  TaskName { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime Date { get; set; } = DateTime.Now;


    }

}
