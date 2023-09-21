using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using TaskManager;

namespace Tests
{
    [TestClass]
    public class TestDB
    {
        private static ITaskRepository<UserTask> repos;
        private static TaskContext dbContext;

        [ClassInitialize()]
        public static void InitDBTesting(TestContext context)
        {
            var builder = new DbContextOptionsBuilder<TaskContext>().UseSqlServer("Server=(LocalDB)\\mssqllocaldb;Initial Catalog=tasks;Trusted_Connection=True;");
            dbContext = new TaskContext(builder.Options);
            repos = new TaskRepository<UserTask>(dbContext);
        }

        [TestMethod]
        public void AddTask()
        {
            System.DateTime testTime = System.DateTime.Now;
            UserTask task = new UserTask()
            {
                Name = "test",
                Description = "Test Desciption",
                CreationDate = testTime,
                DeadLine = testTime,
                CloseDate = testTime,
                Status = TaskStatus.Created
            };
            repos.Create(task);
            UserTask fromDB = (from taskInDB in dbContext.Tasks
                               where taskInDB.Name.Contains("test")
                               select taskInDB
                               ).FirstOrDefault();
            Assert.IsNotNull(fromDB);
            Assert.AreEqual(fromDB.Name, "test");
            Assert.AreEqual(fromDB.Description, "Test Desciption");
            Assert.AreEqual(fromDB.CreationDate, testTime);
            Assert.AreEqual(fromDB.DeadLine, testTime);
            Assert.AreEqual(fromDB.CloseDate, testTime);
            Assert.AreEqual(fromDB.Status, TaskStatus.Created);
        }
    }
}
