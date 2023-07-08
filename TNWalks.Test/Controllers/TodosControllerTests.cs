using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TNWalks.API.Controllers;
using TNWalks.API.Exceptions;
using TNWalks.API.Models.Dtos;
using TNWalks.API.Services;
using TNWalks.Domain.Entities;

namespace TNWalks.Test.Controllers
{
    public class TodosControllerTests
    {
        private readonly Mock<ITodoService> _mockTodoService;
        private readonly TodosController _todosController;
        private readonly Mock<IValidator<CreateTodoDto>> _mockCreateTodoValidator;
        private readonly Mock<IValidator<UpdateTodoDto>> _mockUpdateTodoValidator;

        public TodosControllerTests()
        {
            _mockTodoService = new Mock<ITodoService>();
            _todosController = new TodosController(_mockTodoService.Object);
            _mockCreateTodoValidator = new Mock<IValidator<CreateTodoDto>>();
            _mockUpdateTodoValidator = new Mock<IValidator<UpdateTodoDto>>();
        }

        [Fact]
        public async Task GetAll_ReturnsListOfTodoListDto()
        {
            var listTodoDtos = new List<TodoListDto>
            {
                new TodoListDto { Id = 1, Title = "Title 1", Description = "Description 1" },
                new TodoListDto { Id = 2, Title = "Title 2", Description = "Description 2" },
            };
            _mockTodoService.Setup(service => service.GetAllTodos())
                .ReturnsAsync(listTodoDtos);

            var result = await _todosController.GetAll();

            Assert.IsType<ActionResult<List<TodoListDto>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTodoListDtos = Assert.IsType<List<TodoListDto>>(okResult.Value);
            Assert.Equal(listTodoDtos, returnedTodoListDtos);
        }

        [Fact]
        public async Task GetTodoById_WithValidId_ReturnsOkResult()
        {
            // arrange
            var id = 1;
            var todoDetail = new TodoDetailDto { Id = id, Title = "Title 1", Description = "Description 1" };
            _mockTodoService.Setup(service => service.GetTodoById(id))
                .ReturnsAsync(todoDetail);
            
            // act
            var result = await _todosController.GetTodoById(id);
            
            // assert
            Assert.IsType<ActionResult<TodoDetailDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTodoDetailDto = Assert.IsType<TodoDetailDto>(okResult.Value);
            Assert.Equal(todoDetail, returnedTodoDetailDto);
        }

        [Fact]
        public async Task GetTodoById_WithInvalidId_ReturnsNotFoundException()
        {
            var nonExistentId = 99;
            _mockTodoService.Setup(service => service.GetTodoById(nonExistentId))
                .ThrowsAsync(new NotFoundException(nameof(Todo), nonExistentId));

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(() => _todosController.GetTodoById(nonExistentId));
            Assert.Equal("Todo (99) was not found.", exception.Message);
        }

        [Fact]
        public async Task CreateTodo_WithValidData_ReturnsCreatedActionResult()
        {
            var createTodoDto = new CreateTodoDto()
            {
                Title = "Title 1",
                Description = "Description 2"
            };
            var returnDto = new TodoDetailDto()
            {
                Id = 1,
                Title = "Title 1",
                Description = "Description 2",
                Status = TodoStatus.New,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            var validationResult = new ValidationResult();

            _mockCreateTodoValidator.Setup(validator => validator.ValidateAsync(createTodoDto, new CancellationToken()))
                .ReturnsAsync(validationResult);
            _mockTodoService.Setup(service => service.CreateTodo(createTodoDto))
                .ReturnsAsync(returnDto);

            var result = await _todosController.CreateTodo(createTodoDto, _mockCreateTodoValidator.Object);

            Assert.IsType<CreatedAtActionResult>(result.Result);
            var createAtActionResult = (CreatedAtActionResult)result.Result!;
            Assert.Equal(nameof(TodosController.GetTodoById), createAtActionResult.ActionName);
            Assert.Equal(returnDto.Id, createAtActionResult.RouteValues!["id"]);
            Assert.Equal(returnDto, createAtActionResult.Value);
        }
        
        [Fact]
        public async Task CreateTodo_WithInvalidData_ThrowsBadRequestException()
        {
            // Arrange
            var createTodoDto = new CreateTodoDto()
            {
                Title = "no",
                Description = "no"
            };
            var validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Property", "Error message"));

            _mockCreateTodoValidator.Setup(validator => validator.ValidateAsync(createTodoDto, new CancellationToken()))
                .ReturnsAsync(validationResult);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _todosController.CreateTodo(createTodoDto, _mockCreateTodoValidator.Object));
        }

        [Fact]
        public async Task UpdateTodo_WithValidData_ReturnsOkResult()
        {
            var id = 1;
            var updateDto = new UpdateTodoDto()
            {
                Title = "Title Update",
                Description = "Description Update"
            };
            ValidationResult validationResult = new();
            _mockUpdateTodoValidator.Setup(v => v.ValidateAsync(updateDto, new CancellationToken()))
                .ReturnsAsync(validationResult);
            _mockTodoService.Setup(s => s.UpdateTodo(id, updateDto))
                .Verifiable();

            var result = await _todosController.UpdateTodo(id, updateDto, _mockUpdateTodoValidator.Object);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateTodo_InvalidDto_ReturnsBadRequestException()
        {
            var id = 1;
            var updateDto = new UpdateTodoDto()
            {
                Title = "bad",
                Description = "bad"
            };
            ValidationResult validationResult = new();
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Property", "Error message"));
            
            _mockUpdateTodoValidator.Setup(validator => validator.ValidateAsync(updateDto, new CancellationToken()))
                .ReturnsAsync(validationResult);
            _mockTodoService.Setup(s => s.UpdateTodo(id, updateDto))
                .Verifiable();
            
            await Assert.ThrowsAsync<BadRequestException>(() => _todosController.UpdateTodo(id, updateDto, _mockUpdateTodoValidator.Object));
        }
        
        [Fact]
        public async Task UpdateTodo_InvalidId_ReturnsNotFoundException()
        {
            var nonExistentId = 99;
            var updateDto = new UpdateTodoDto()
            {
                Title = "Valid Title",
                Description = "Valid Description"
            };
            ValidationResult validationResult = new();
            _mockUpdateTodoValidator.Setup(validator => validator.ValidateAsync(updateDto, new CancellationToken()))
                .ReturnsAsync(validationResult);
            _mockTodoService.Setup(service => service.UpdateTodo(nonExistentId, updateDto))
                .ThrowsAsync(new NotFoundException(nameof(Todo), nonExistentId));

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(() => _todosController.UpdateTodo(nonExistentId, updateDto, _mockUpdateTodoValidator.Object));
            Assert.Equal("Todo (99) was not found.", exception.Message);
            
        }
        
        [Fact]
        public async Task DeleteTodo_ValidId_ReturnsOkResult()
        {
            var id = 1;
            _mockTodoService.Setup(service => service.DeleteTodo(id))
                .Verifiable();

            var result = await _todosController.DeleteTodo(id);
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async Task DeleteTodo_InvalidId_ReturnsNotFoundException()
        {
            var nonExistentId = 99;
            _mockTodoService.Setup(service => service.DeleteTodo(nonExistentId))
                .ThrowsAsync(new NotFoundException(nameof(Todo), nonExistentId));

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(() => _todosController.DeleteTodo(nonExistentId));
            Assert.Equal("Todo (99) was not found.", exception.Message);
        }
    }
}