using Microsoft.AspNetCore.Mvc;

namespace TNWalks.API.Models
{
    public class CustomValidationProblemsDetails : ProblemDetails
    {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}