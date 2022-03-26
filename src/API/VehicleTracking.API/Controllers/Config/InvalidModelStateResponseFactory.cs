using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.API.Extensions;

namespace VehicleTracking.API.Controllers.Config
{
    public class InvalidModelStateResponseFactory
    {
		public static IActionResult ProduceErrorResponse(ActionContext context)
		{
			var problemDetails = new CustomBadRequest(context);
			return new BadRequestObjectResult(problemDetails)
			{
			};
		}
	}
}
