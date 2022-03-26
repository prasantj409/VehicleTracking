using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.API.Test.FunctionalTest
{
	public class VehicleScenarioBase : IntegrationTest
	{
		private const string ApiUrlBase = "/api";

		public HttpContent CreateJsonContent(object obj)
		{
			var json = JsonConvert.SerializeObject(obj);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

#pragma warning disable IDE0059 // Unnecessary assignment of a value

			var contentLenth = content.Headers.ContentLength;
#pragma warning restore IDE0059 // Unnecessary assignment of a value

			return content;
		}

		public static class Get
		{
			public static string GetDeviceList()
			{
				return $"{ApiUrlBase}/vehicle/device";
			}

			public static string GetDeviceCurrentPosition(string device_no)
			{
				return $"{ApiUrlBase}/vehicle/track/currentposition/device/{device_no}";
			}

			public static string GetDevicePositions(string device_no,DateTime startTime, DateTime endTime, int page, int limit)
			{
				return $"{ApiUrlBase}/vehicle/track/device/{device_no}?StartTime={startTime}&EndTime={endTime}&page={page}&limit={limit}";
			}
		}

		public static class Post
		{
			public static string AdminLogin()
			{
				return $"{ApiUrlBase}/admin/login";
			}

			public static string RegisterDevice()
			{
				return $"{ApiUrlBase}/device/register";
			}

			public static string DeviceLogin()
			{
				return $"{ApiUrlBase}/device/login";
			}

			public static string RecordDevicePosition()
			{
				return $"{ApiUrlBase}/device/record_position";
			}
		}
	}
}
