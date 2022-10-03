using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TODO_App.Data;
using TODO_App.Models;

namespace TODO_App.Pages
{
    public class TaskAddModel : PageModel
    {
        public TaskModel Task { get; private set; }
        private TasksDBContext _context;
        public SelectList SelectListItems { get; private set; }
        public TaskAddModel()
        {
            _context = new TasksDBContext();
            Task = new TaskModel() {  Name = "New task", State = TaskState.Created };
            SelectListItems = new SelectList(Enum.GetValues<TaskState>(), TaskState.Created);
        }
        public void OnGet()
        {
        }
        public RedirectToPageResult OnPost(TaskModel task)
        {
            _context.AddTask(task);
            return RedirectToPage("TaskList");
        }
    }
}
