using FluentValidation.TestHelper;
using TNWalks.API.Models.Dtos;
using TNWalks.API.Validators;

namespace TNWalks.Test.Validators
{
    public class CreateTodoValidatorTests
    {
        private readonly CreateTodoValidator _validator;

        public CreateTodoValidatorTests()
        {
            _validator = new CreateTodoValidator();
        }

        [Fact]
        public void Validate_WithValidTodo_ReturnsNoValidationErrors()
        {
            var todoDto = new CreateTodoDto()
            {
                Title = "Valid Title",
                Description = "Valid Description"
            };

            var result = _validator.TestValidate(todoDto);
            
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("", "Valid Description", "Title is required")]
        [InlineData("Valid title", "", "Description is required")]
        [InlineData("Bad", "Valid Description", "Title must be between 5 and 100 characters")]
        public void Validate_InvalidData_ReturnsValidationErrors(string title, string description, string errorMessage)
        {
            var invalidDto = new CreateTodoDto()
            {
                Title = title,
                Description = description,
            };

            var result = _validator.TestValidate(invalidDto);

            result.ShouldHaveAnyValidationError()
                .WithErrorMessage(errorMessage);
        }
        
    }
}