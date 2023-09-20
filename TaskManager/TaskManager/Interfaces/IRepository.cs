using System;
using System.Collections.Generic;

namespace TaskManager
{
	public interface ITaskRepository<UserTask>
	{
		public List<TaskManager.UserTask> GetAll();
		public TaskManager.UserTask Get(int id);
		public bool Create(TaskManager.UserTask task);
		public bool Update(TaskManager.UserTask task);
		public bool Delete(int id);
	}
}