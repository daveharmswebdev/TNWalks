using Microsoft.AspNetCore.Mvc;
using TNWalks.API.Models;
using TNWalks.API.Models.Dtos;
using TNWalks.Domain.Entities;

namespace TNWalks.API.Services
{
    public interface ITodoService
    {
        Task<List<TodoListDto>> GetAllTodos();
        Task<PagedList<TodoListDto>> GetPagedTodos(int page, int pageSize, string search = "", string sortBy = "", bool isAscending = true, TodoStatus? status = null);
        Task<TodoDetailDto> GetTodoById(int id);
        Task<TodoDetailDto> CreateTodo(CreateTodoDto createTodoDto);
        Task UpdateTodo(int id, UpdateTodoDto updateTodoDto);
        Task DeleteTodo(int id);
    }
}