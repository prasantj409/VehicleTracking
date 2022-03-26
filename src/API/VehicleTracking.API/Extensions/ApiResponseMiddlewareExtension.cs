using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTracking.API.Extensions
{
	public static class ApiResponseMiddlewareExtension
	{
		public static IApplicationBuilder UseApiResponseAndExceptionWrapper(this IApplicationBuilder builder)
		{			
			return builder.UseMiddleware<APIResponseMiddleware>();
		}
	}
}
