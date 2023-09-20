using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaskManager
{
    [ApiController]
    [Route("api/[[tasks]]")]
    public class TaskController : ControllerBase
    {
        private ITaskRepository<TaskManager.UserTask> TasksRepository { get; set; }

        public TaskController(ITaskRepository<TaskManager.UserTask> taskRepository)
        {
            TasksRepository = taskRepository;
        }

        [HttpGet("GetById/{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(TasksRepository.Get(id));
        }

        [HttpGet("GetList")]
        public JsonResult GetAll(
            [FromQuery(Name = "page")] int pageNumber,
            [FromQuery(Name = "pagesize")] int pageSize,
            [FromQuery(Name = "name")] string name,
            [FromQuery(Name = "status")] int status
            )
        {
            List<TaskManager.UserTask> list;
            try
            {
                list = TasksRepository.GetAll();
                if (name.Length > 0) list = (from task in list
                                             where task.Name.Contains(name)
                                             select task).ToList<TaskManager.UserTask>();
                if (status >= 0 && status <= 2)
                    list = (from task in list
                            where task.Status == (TaskManager.TaskStatus)status
                            select task).ToList<TaskManager.UserTask>();
                if (pageNumber > 0 && pageSize > 0)
                    list = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList<TaskManager.UserTask>();
            } catch (Exception e)
            {
                list = new List<UserTask>();
            }
            return new JsonResult(list);
        }

        [HttpPut]
        public JsonResult Put(TaskManager.UserTask task)
        {

            return new JsonResult(TasksRepository.Create(task));
        }

        [HttpPost]
        public JsonResult Post(TaskManager.UserTask task)
        {
            return new JsonResult(TasksRepository.Update(task));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            return new JsonResult(TasksRepository.Delete(id));
        }
    }
}
