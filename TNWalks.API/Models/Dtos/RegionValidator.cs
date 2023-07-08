using FluentValidation;

namespace TNWalks.API.Models.Dtos
{
    public class RegionValidator : AbstractValidator<CreateRegionDto>
    {
        public RegionValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().MinimumLength(2).MaximumLength(3);
        }
        
    }
}