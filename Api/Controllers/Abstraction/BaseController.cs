using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Abstraction;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
}