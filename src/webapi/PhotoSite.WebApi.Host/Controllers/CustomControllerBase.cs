using Microsoft.AspNetCore.Mvc;

namespace PhotoSite.WebApi.Controllers
{
    /// <summary>
    /// BaseController
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public abstract class CustomControllerBase : ControllerBase
    {
    }
}