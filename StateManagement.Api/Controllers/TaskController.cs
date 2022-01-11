using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace StateManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {

        [HttpGet]
        public IEnumerable Get()
        {
            return Enumerable.Range(1, 5).ToArray();
        }
    }
}