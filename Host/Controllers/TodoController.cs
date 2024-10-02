using Host.Auxiliary;
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
    }

    public class TodoEntry
    {

    }

}
