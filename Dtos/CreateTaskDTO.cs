using System.ComponentModel.DataAnnotations;
namespace TodoList.Dtos
{

    
    public record CreateTaskDTO(
        [Required(ErrorMessage =" Task name is required")]
        [StringLength(100,MinimumLength =1, ErrorMessage ="Task name must be between 1 and 100 characters")]
        string TaskName);
}
