using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TNWalks.API.Exceptions;
using TNWalks.API.Models.Dtos;
using TNWalks.API.Services;
using TNWalks.Domain.Entities;

namespace TNWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoListDto>>> GetAll()
        {
            var todoDtos = await _todoService.GetAllTodos();
            return Ok(todoDtos);
        }
        
        [HttpGet]
        [Route("dropdowns")]
        public async Task<ActionResult<List<TodoDropdownDto>>> GetDropdowns([FromQuery] string search = "")
        {
            var todoDtos = await _todoService.GetDropdowns(search);
            return Ok(todoDtos);
        }

        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult<List<TodoListDto>>> GetPagedTodos(
            int page = 1, 
            int pageSize = 10,
            string search = "",
            string sortBy = "",
            bool isAscending = true,
            TodoStatus? status = null)
        {
            var todoDtos = await _todoService.GetPagedTodos(page, pageSize, search, sortBy, isAscending, status);
            return Ok(todoDtos);
        }
        
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("{id:int}")]
        public async Task<ActionResult<TodoDetailDto>> GetTodoById([FromRoute] int id)
        {
            var dto = await _todoService.GetTodoById(id);
            return Ok(dto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TodoDetailDto>> CreateTodo(
            [FromBody] CreateTodoDto createTodoDto,
            [FromServices] IValidator<CreateTodoDto> validator)
        {
            var validationResult = await validator.ValidateAsync(createTodoDto);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Invalid Create Todo Request", validationResult);
            }
            
            var returnDto = await _todoService.CreateTodo(createTodoDto);
            return CreatedAtAction(nameof(GetTodoById), new { id = returnDto.Id }, returnDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTodo(
            [FromRoute] int id,
            [FromBody] UpdateTodoDto updateTodoDto,
            [FromServices] IValidator<UpdateTodoDto> validator)
        {
            var validationResult = await validator.ValidateAsync(updateTodoDto);

            if (!validationResult.IsValid)
                throw new BadRequestException("Invalid Update Todo Request", validationResult);
            
            await _todoService.UpdateTodo(id, updateTodoDto);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            await _todoService.DeleteTodo(id);
            return Ok($"Todo with {id} was deleted.");
        }
    }
}