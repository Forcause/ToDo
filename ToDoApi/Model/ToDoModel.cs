using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Model
{
    public class ToDoModel
    {
        [Key]
        public int Id { get; set; }

        public string ToDoName { get; set; }

        public string? ToDoDescription { get; set; }
    }
}
