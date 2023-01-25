using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToDoApi.Model;

namespace ToDoApi.Data
{
    public class ToDoApplicationContext : DbContext
    {
        public ToDoApplicationContext(DbContextOptions<ToDoApplicationContext> options) : base(options)
        {
        }

        public DbSet<ToDoModel> ToDos => Set<ToDoModel>();
    }
}
