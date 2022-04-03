using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Domain.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}