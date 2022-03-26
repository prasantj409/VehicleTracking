using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
//using System.Text.Json.Serialization;

namespace VehicleTracking.Domain.Response
{
	public class ApiResponse
	{
		
		[DataMember]
		public bool success { get; set; }	
		

		[DataMember(EmitDefaultValue = false)]
		public object Result { get; set; }
		


		public ApiResponse(int statusCode, APIError apiError)
		{
			
			//this.ResponseException = apiError;
			this.success = false;
		}
		public ApiResponse(int statusCode, string message)
		{
			
			//this.Message = message;
			this.success = false;
		}
		[JsonConstructor]
		public ApiResponse(object result, bool issuccess, string message)
		{
			this.Result = result;
			this.success = issuccess;
			//this.Message = message;
		}

	}
}
