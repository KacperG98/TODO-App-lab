using TODO_App_lab.Models;

namespace TODO_App_lab.Services.Interfaces
{
    public interface ITodoItemRepository
    {
        List<TodoItem> GetAll();

        void Add(TodoItem item);

        void AddRange(List<TodoItem> items);

        void Update(TodoItem item);

        void Delete(TodoItem item);
    }
}
