using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO_App_lab.Models;

namespace TODO_App_lab.Services.Interfaces
{
    public interface ITodoItemRepository
    {
        List<TodoItem> GetAll();

        void Add(TodoItem item);

        void Update(TodoItem item);

        void Delete(TodoItem item);
    }
}
