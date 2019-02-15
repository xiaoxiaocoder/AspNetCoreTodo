using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{

    public class TodoItemService: ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemAsync(TodoItem model, IdentityUser currentUser)
        {
            model.Id = Guid.NewGuid();
            model.IsDone = false;
            model.DueAt = DateTimeOffset.Now.AddDays(3);
            model.UserId = currentUser.Id;

            _context.Items.Add(model);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemAsync(IdentityUser currentUser)
        {
            var items = await _context.Items
                .Where(x => x.UserId == currentUser.Id)
                .ToArrayAsync();
            return items;
        }

        public async Task<bool> MakeDoneAsync(Guid id, IdentityUser currentUser)
        {
            var item = await _context.Items
            .Where(x => x.Id == id && x.UserId == currentUser.Id)
            .SingleOrDefaultAsync();

            if(item == null) return false;

            item.IsDone = !item.IsDone;

            var saveResult =  await _context.SaveChangesAsync();
            return saveResult == 1;
        }

    }
}
