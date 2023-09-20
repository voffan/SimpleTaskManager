using System;
using System.Linq;
using System.Collections.Generic;

namespace TaskManager
{
	public class TaskRepository<UserTask> : ITaskRepository<UserTask>
	{
		private TaskContext Context { get; set; }

		public TaskRepository(TaskContext context)
        {
			Context = context;
        }

		public List<TaskManager.UserTask> GetAll()
        {
			return Context.Tasks.ToList();
        }

		public TaskManager.UserTask Get(int id)
        {
            TaskManager.UserTask task = Context.Tasks.FirstOrDefault(t => t.Id == id);

            return task;
        }

		public bool Create(TaskManager.UserTask task)
        {
            if(!Validator.ValidateTask(task))
                return false;
            TaskManager.UserTask newTask = new TaskManager.UserTask
            {
                Name = task.Name,
                Description = task.Description,
                DeadLine = task.DeadLine,
                Status = task.Status
            };
            try
            {
                Context.Tasks.Add(newTask);
                Context.SaveChanges();
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }

		public bool Update(TaskManager.UserTask task)
        {
            if (!Validator.ValidateTask(task))
                return false;

            TaskManager.UserTask toEdit = Context.Tasks.FirstOrDefault(t => t.Id == task.Id);
            if (toEdit == default(TaskManager.UserTask))
                return false;
            
            toEdit.Name = task.Name;
            toEdit.Description = task.Description;
            toEdit.CreationDate = task.CreationDate;
            toEdit.DeadLine = task.DeadLine;
            toEdit.CloseDate = task.CloseDate;
            toEdit.Status = task.Status;
            try
            {
                Context.Tasks.Update(toEdit);
                //Context.Entry(toEdit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Context.SaveChanges();
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }

		public bool Delete(int id)
        {
            TaskManager.UserTask toDelete = Context.Tasks.FirstOrDefault(t => t.Id == id);
            if (toDelete == default(TaskManager.UserTask))
                return false;
            try
            {
                Context.Tasks.Remove(toDelete);
                Context.SaveChanges();
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }
	}
}
