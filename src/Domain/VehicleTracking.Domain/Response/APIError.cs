using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Domain.Response
{
    public class APIError
    {
		public string ExceptionMessage { get; set; }
		public string Details { get; set; }
		public string ReferenceErrorCode { get; set; }
		public string ReferenceDocumentLink { get; set; }
		public IEnumerable<ValidationError> ValidationErrors { get; set; }

		[JsonConstructor]
		public APIError(string message)
		{
			this.ExceptionMessage = message;

		}

		public APIError(string message, IEnumerable<ValidationError> validationErrors)
		{
			this.ExceptionMessage = message;
			this.ValidationErrors = validationErrors;

		}
	}
}
