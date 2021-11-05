using Microsoft.AspNetCore.Mvc;

namespace AkliaJob.Shared.Controller
{
    [Route("akliajob/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {

    }
}
