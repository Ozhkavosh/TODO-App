using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TODO_App.Data;
using TODO_App.Models;

namespace TODO_App.Pages
{
    public class TaskEditModel : PageModel
    {
        public TaskModel Task { get; private set; }
        private List<TaskModel> List;
        private TasksDBContext _context;
        public SelectList? SelectListItems { get; private set; }
        public TaskEditModel()
        {
            _context = new TasksDBContext();
            List = new TasksDBContext().GetTasks();
            Task = new TaskModel() { Id = -1, Name = "Null task" };
        }
        public void OnGet(int id)
        {
            var res = List.Find((t) => t.Id == id);
            if (res != null) Task = res;
            SelectListItems = new SelectList(Enum.GetValues<TaskState>(), Task.State);
        }

        public RedirectToPageResult OnPost(TaskModel task)
        {
            _context.EditTask(task.Id, task);

            return RedirectToPage(pageName: "TaskList");
        }
    }
}
