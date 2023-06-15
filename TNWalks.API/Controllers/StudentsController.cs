using Microsoft.AspNetCore.Mvc;

namespace TNWalks.API.Controllers
{
    // https://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "Dave", "Jennifer", "Orman", "Marianna", "Ethan" };
            return Ok(studentNames);
        }
    }
}
