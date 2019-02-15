using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        //Task<TodoItem[]> GetIncompleteItemAsync(ApplicationUser currentUser);

        //Task<bool> AddItemAsync(TodoItem model, ApplicationUser currentUser);

        Task<TodoItem[]> GetIncompleteItemAsync(IdentityUser currentUser);

        Task<bool> AddItemAsync(TodoItem model, IdentityUser currentUser);
        Task<bool> MakeDoneAsync(Guid id, IdentityUser currentUser);
    }
}
