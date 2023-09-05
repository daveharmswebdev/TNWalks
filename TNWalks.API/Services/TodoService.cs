using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TNWalks.API.Exceptions;
using TNWalks.API.Models;
using TNWalks.API.Models.Dtos;
using TNWalks.API.Repositories;
using TNWalks.Domain.Entities;

namespace TNWalks.API.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }
        
        public async Task<List<TodoListDto>> GetAllTodos()
        {
            var todos = await _todoRepository.GetAllAsync();

            var todoDtos = _mapper.Map<List<TodoListDto>>(todos);
            return todoDtos;
        }

        public async Task<PagedList<TodoListDto>> GetPagedTodos(int page, int pageSize, string search = "", string sortBy = "", bool isAscending = true, TodoStatus? status = null)
        {
            var todosQuery = await _todoRepository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                todosQuery = todosQuery.Where(todo =>
                    EF.Functions.Like(todo.Title, $"%{search}%") ||
                    EF.Functions.Like(todo.Description, $"%{search}%"));
            }

            if (status.HasValue)
            {
                todosQuery = todosQuery.Where(todo => todo.Status == status.Value);
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "id":
                        todosQuery = isAscending ? todosQuery.OrderBy(todo => todo.Id) : todosQuery.OrderByDescending(todo => todo.Id);
                        break;
                    case "title":
                        todosQuery = isAscending ? todosQuery.OrderBy(todo => todo.Title) : todosQuery.OrderByDescending(todo => todo.Title);
                        break;
                    case "createdat":
                        todosQuery = isAscending ? todosQuery.OrderBy(todo => todo.CreatedAt) : todosQuery.OrderByDescending(todo => todo.CreatedAt);
                        break;
                    default:
                        todosQuery = isAscending ? todosQuery.OrderBy(todo => todo.Id) : todosQuery.OrderByDescending(todo => todo.Id);
                        break;
                }
            }

            var totalCount = await todosQuery.CountAsync();

            var todoDtos = await todosQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(todo => _mapper.Map<TodoListDto>(todo))
                .ToListAsync();

            return new PagedList<TodoListDto>(todoDtos, page, pageSize, totalCount);
        }
        
        
        // public async Task<PagedList<TodoListDto>> GetPagedTodos(int page, int pageSize, string search = "", string sortBy = "", TodoStatus? status = null, bool isAscending = true)
        // {
        //     var todosQuery = await _todoRepository.GetQueryable();
        //
        //     if (!string.IsNullOrWhiteSpace(search))
        //     {
        //         todosQuery = todosQuery.Where(todo =>
        //             EF.Functions.Like(todo.Title, $"%{search}%") ||
        //             EF.Functions.Like(todo.Description, $"%{search}%"));
        //     }
        //
        //     if (status.HasValue)
        //     {
        //         todosQuery = todosQuery.Where(todo => todo.Status == status.Value);
        //     }
        //
        //     if (!string.IsNullOrWhiteSpace(sortBy))
        //     {
        //         switch (sortBy.ToLower())
        //         {
        //             case "title":
        //                 todosQuery = isAscending ? todosQuery.OrderBy(todo => todo.Title) : todosQuery.OrderByDescending(todo => todo.Title);
        //                 break;
        //             case "createdat":
        //                 todosQuery = isAscending ? todosQuery.OrderBy(todo => todo.CreatedAt) : todosQuery.OrderByDescending(todo => todo.CreatedAt);
        //                 break;
        //             default:
        //                 todosQuery = isAscending ? todosQuery.OrderBy(todo => todo.CreatedAt) : todosQuery.OrderByDescending(todo => todo.CreatedAt);
        //                 break;
        //         }
        //     }
        //
        //     var totalCount = await todosQuery.CountAsync();
        //
        //     var todoDtos = await todosQuery
        //         .Skip((page - 1) * pageSize)
        //         .Take(pageSize)
        //         .Select(todo => _mapper.Map<TodoListDto>(todo))
        //         .ToListAsync();
        //
        //     return new PagedList<TodoListDto>(todoDtos, page, pageSize, totalCount);
        // }


        public async Task<TodoDetailDto> GetTodoById(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);

            if (todo == null)
                throw new NotFoundException(nameof(Todo), id);

            var dto = _mapper.Map<TodoDetailDto>(todo);

            return dto;
        }

        public async Task<TodoDetailDto> CreateTodo(CreateTodoDto createTodoDto)
        {
            var newTodo = _mapper.Map<Todo>(createTodoDto);

            newTodo = await _todoRepository.CreateAsync(newTodo);

            return _mapper.Map<TodoDetailDto>(newTodo);
        }

        public async Task UpdateTodo(int id, UpdateTodoDto updateTodoDto)
        {
            var todo = await _todoRepository.GetByIdAsync(id);

            if (todo == null)
                throw new NotFoundException(nameof(Todo), id);

            _mapper.Map(updateTodoDto, todo);

            await _todoRepository.UpdateAsync(todo);
        }

        public async Task DeleteTodo(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);

            if (todo == null)
                throw new NotFoundException(nameof(Todo), id);

            await _todoRepository.DeleteAsync(todo);
        }
    }
}