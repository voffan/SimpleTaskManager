using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TaskManager
{
    public class TaskContext: DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options): base(options)
        {

        }
        public DbSet<UserTask> Tasks { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseNpgSql(configuration.GetConnectionString("DefaultConnection"));
        }*/
    }
}