using FluentValidation;
using TNWalks.API.Models.Dtos;

namespace TNWalks.API.Validators
{
    public class UpdateTodoValidator : AbstractValidator<UpdateTodoDto>
    {
        public UpdateTodoValidator()
        {
            RuleFor(todo => todo.Title)
                .NotEmpty().WithMessage("Title is required")
                .Length(5, 100).WithMessage("Title must be between 5 and 100 characters");

            RuleFor(todo => todo.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(5, 250).WithMessage("Description must be between 5 and 250 characters");
        }
        
    }
}