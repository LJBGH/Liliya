using Microsoft.AspNetCore.Mvc;

namespace Liliya.AspNetCore.ApiBase
{
    [Route("liliya/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {

    }
}
