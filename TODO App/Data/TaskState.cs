namespace TODO_App.Data
{
    public static class TaskStateStringGetter
    {
        private const string _invalidTypeString = "Invalid";
        public static string GetString(TaskState state)
        {
            if (!Enum.IsDefined(state)) return _invalidTypeString;
            return state.ToString();
        }
        public static string GetString(int state)
        {
            TaskState taskState = (TaskState)state;
            return GetString(taskState);
        }
    }
    public enum TaskState
    {
        None = 0,
        Created = 1,
        InProcess = 2,
        Done = 3
    }
}
