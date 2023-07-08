using Microsoft.AspNetCore.Mvc;
using TNWalks.API.Models.Dtos;

namespace TNWalks.API.Services
{
    public interface ITodoService
    {
        Task<List<TodoListDto>> GetAllTodos();
        Task<TodoDetailDto> GetTodoById(int id);
        Task<TodoDetailDto> CreateTodo(CreateTodoDto createTodoDto);
        Task UpdateTodo(int id, UpdateTodoDto updateTodoDto);
        Task DeleteTodo(int id);
    }
}