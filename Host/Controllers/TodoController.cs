using Host.Auxiliary;
using Host.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly IConfigurationHelper _config;

        public TodoController(IConfigurationHelper config)
        {
            _config = config;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<ITodo>> Get()
        //{

        //}
    }
}
