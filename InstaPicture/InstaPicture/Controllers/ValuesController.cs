using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InstaPicture.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		// GET api/values
		[HttpGet]
		public ActionResult<string> Get() => "All is ok!";
	}
}
