using AutoMapper;
using TNWalks.API.Exceptions;
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