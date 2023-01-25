using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoClient.Model;

namespace ToDoClient.DataService
{
    public interface IRestDataService
    {
        Task<List<ToDoModel>> GetAllToDosAsync();
        Task AddToDoAsync(ToDoModel toDoModel);
        Task DeleteToDoAsync(int id);
        Task UpdateToDoModelAsync(ToDoModel toDoModel);

    }
}
