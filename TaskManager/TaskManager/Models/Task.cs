using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager
{
    public enum TaskStatus { Created, InProgress, Closed }

    public class UserTask
    {
        public UserTask()
        {
            CreationDate = DateTime.Now;
        }

        public int Id { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime? CloseDate { get; set; }
        public TaskStatus Status { get; set; }
    }
}