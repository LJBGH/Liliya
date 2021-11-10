using Microsoft.AspNetCore.Mvc;

namespace Liliya.Shared.Controller
{
    [Route("liliya/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {

    }
}
