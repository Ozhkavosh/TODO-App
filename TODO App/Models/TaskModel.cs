using Microsoft.AspNetCore.Mvc;
using TODO_App.Data;

namespace TODO_App.Models
{
    [BindProperties]
    public class TaskModel
    {
        
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TaskState State { get; set; }
    }

}
