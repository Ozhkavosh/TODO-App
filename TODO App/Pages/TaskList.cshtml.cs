using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Npgsql;
using TODO_App.Models;
using TODO_App.Data;

namespace TODO_App.Pages
{
    public class TaskListModel : PageModel
    {
        private TasksDBContext _context;
        public List<TaskModel> TaskList { get; private set; } = new List<TaskModel>();
        public TaskListModel()
        {
            _context = new TasksDBContext();
        }
        public void OnGet()
        {
            TaskList = _context.GetTasks();
        }
        public RedirectToPageResult OnPostEdit(int id)
        {
            var task = TaskList.Find((t) => t.Id == id);
            RouteValueDictionary dict = new RouteValueDictionary();
            dict.Add("id", id);
            return RedirectToPage("TaskEdit", routeValues:dict);
        }
        public RedirectToPageResult OnPostRemove(int id)
        {
            _context.RemoveTask(id);
            return RedirectToPage();
        }
    }
}
