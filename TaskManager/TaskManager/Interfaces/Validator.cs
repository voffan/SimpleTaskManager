using System;

namespace TaskManager
{
	public class Validator
	{
		public static bool ValidateTask(TaskManager.UserTask task)
        {
            if (task.CreationDate < DateTime.Now ||
                task.CloseDate < DateTime.Now ||
                task.DeadLine < DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}
