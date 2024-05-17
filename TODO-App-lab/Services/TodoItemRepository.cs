using Microsoft.EntityFrameworkCore;
using TODO_App_lab.Models;
using TODO_App_lab.Services.Interfaces;

namespace TODO_App_lab.Services
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly DefaultContext _context;

        public TodoItemRepository(DefaultContext context)
        {
            _context = context;
            if (context is not null && context.Database.EnsureCreated())
            {
                context.Database.Migrate();
                context.SaveChanges();
            }
        }

        public List<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        public void Add(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public void Update(TodoItem item)
        {
            var entity = _context.TodoItems.SingleOrDefault(i => item.Id == i.Id);
            _context.Entry(entity).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }

        public void Delete(TodoItem item)
        {
            _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
