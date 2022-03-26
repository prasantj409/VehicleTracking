using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Domain.Response;
using VehicleTracking.Service.Const;
using VehicleTracking.Service.Exceptions;

namespace VehicleTracking.API.Extensions
{
    
    public class APIResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public APIResponseMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

		public async Task InvokeAsync(HttpContext context)
		{
			if (IsSwagger(context))
				await this._next(context);
			else
			{
				var stopWatch = Stopwatch.StartNew();

				var request = await FormatRequest(context.Request);

				var originalBodyStream = context.Response.Body;

				using (var bodyStream = new MemoryStream())
				{
					try
					{
						context.Response.Body = bodyStream;

						await _next.Invoke(context);

						context.Response.Body = originalBodyStream;
						var bodyAsText = await FormatResponse(bodyStream);

						if (context.Response.StatusCode == (int)HttpStatusCode.OK || 
							context.Response.StatusCode == (int)HttpStatusCode.Created||
							context.Response.StatusCode == (int)HttpStatusCode.Accepted)
						{
							
							
							await HandleSuccessRequestAsync(context, bodyAsText, context.Response.StatusCode);
								
							
						}
						else
						{
							await HandleNotSuccessRequestAsync(context, bodyAsText, context.Response.StatusCode);
						}
					}
					catch (Exception ex)
					{
						await HandleExceptionAsync(context, ex);
						bodyStream.Seek(0, SeekOrigin.Begin);
						await bodyStream.CopyToAsync(originalBodyStream);
					}
					finally
					{
						stopWatch.Stop();
						//_logger.Log(LogLevel.Information,
						//			$@"Request: {request} Responded with [{context.Response.StatusCode}] in {stopWatch.ElapsedMilliseconds}ms");
					}
				}
			}
		}

		private Task HandleSuccessRequestAsync(HttpContext context, object body, int code)
		{
			string jsonString = string.Empty;
			
			var bodyText = !body.ToString().IsValidJson() ? ConvertToJSONString(body) : body.ToString();			
			ApiResponse APIResponse = JsonConvert.DeserializeObject<ApiResponse>(bodyText);
			APIResponse.success = true;
			
			context.Response.ContentType = "application/json";

			return context.Response.WriteAsync(ConvertToJSONString(APIResponse));
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var jsonString = "";
			
			
			if (exception is ValidationException)
			{
				APIError APIError1 = null;
				var code1 = 200;
				string type = _httpContextAccessor.HttpContext == null ? string.Empty : _httpContextAccessor.HttpContext.Request.Path.ToString();

				var message = exception.Message;
				APIError1 = new APIError(message);
				context.Response.StatusCode = code1;				
				
				var defaultError = new DefaultError()
				{
					type = type,
					title = message
				};
				DefaultError bodyErrorContent = JsonConvert.DeserializeObject<DefaultError>(JsonConvert.SerializeObject(defaultError));
				jsonString = ConvertToJSONString(GetErrorResponse(code1, bodyErrorContent, APIError1));
				context.Response.ContentType = "application/json";
			}
			else if (exception is NotFoundException)
			{
				APIError APIError1 = null;
				var code1 = 404;
				string type = _httpContextAccessor.HttpContext == null ? string.Empty : _httpContextAccessor.HttpContext.Request.Path.ToString();
				var message = exception.Message;
				APIError1 = new APIError(message);
				context.Response.StatusCode = code1;
				
				var defaultError = new DefaultError()
				{
					type = type,
					title = message
				};
				DefaultError bodyErrorContent = JsonConvert.DeserializeObject<DefaultError>(JsonConvert.SerializeObject(defaultError));
				jsonString = ConvertToJSONString(GetErrorResponse(code1, bodyErrorContent, APIError1));
				context.Response.ContentType = "application/json";
			}
			else if (exception is BadRequestException)
			{
				APIError APIError1 = null;
				var code1 = 400;
				string type = _httpContextAccessor.HttpContext == null ? string.Empty : _httpContextAccessor.HttpContext.Request.Path.ToString();
				
				var message = exception.Message;
				APIError1 = new APIError(message);
				context.Response.StatusCode = code1;
				
				var innerMsg = message.Split("|");
				
				var defaultError = new DefaultError()
				{
					type = type,
					title = message,
					
				};
				DefaultError bodyErrorContent = JsonConvert.DeserializeObject<DefaultError>(JsonConvert.SerializeObject(defaultError));

				jsonString = ConvertToJSONString(GetErrorResponse(code1, bodyErrorContent, APIError1));
				context.Response.ContentType = "application/json";
			}			
			else
			{
				var message = exception.Message;
				string stackTrace = exception.StackTrace;
				APIError APIError = null;
				APIError = new APIError(message) { Details = stackTrace };
				
				int code = 0;
				code = (int)HttpStatusCode.UnprocessableEntity;
				context.Response.StatusCode = code;
				jsonString = ConvertToJSONString(APIError);

				context.Response.ContentType = "application/json";
			}
			return context.Response.WriteAsync(jsonString);
		}

		private Task HandleNotSuccessRequestAsync(HttpContext context, object body, int code)
		{
			APIError APIError = null;
			var jsonString = "";
			string type = _httpContextAccessor.HttpContext == null ? string.Empty : _httpContextAccessor.HttpContext.Request.Path.ToString();
			//var message = ResponseMessageEnum.AuthenticationFailure.GetDescription();
			string message= string.Empty;
			APIError = new APIError(message);
			if (code == 401 || code == 403)
			{
				message = "Unauthorised authentication failure.";
				var defaultError = new DefaultError()
				{
					type = type,
					title = message
				};
				DefaultError bodyErrorContent = JsonConvert.DeserializeObject<DefaultError>(JsonConvert.SerializeObject(defaultError));
				jsonString = ConvertToJSONString(GetErrorResponse(code, bodyErrorContent, APIError));
			}
			
			context.Response.ContentType = "application/problem+json";
			return context.Response.WriteAsync(jsonString);
		}




		private async Task<string> FormatRequest(HttpRequest request)
		{
			string bodyAsText = string.Empty;
			if (request.ContentType != null)
			{
				request.EnableBuffering();
				var buffer = new byte[Convert.ToInt32(request.ContentLength)];
				await request.Body.ReadAsync(buffer, 0, buffer.Length);
				bodyAsText = Encoding.UTF8.GetString(buffer);
				request.Body.Seek(0, SeekOrigin.Begin);
			}
			return $"{request.Method} {request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
		}

		private async Task<string> FormatResponse(Stream bodyStream)
		{
			bodyStream.Seek(0, SeekOrigin.Begin);
			var plainBodyText = await new StreamReader(bodyStream).ReadToEndAsync();
			bodyStream.Seek(0, SeekOrigin.Begin);
			return plainBodyText;
		}

		private bool IsSwagger(HttpContext context)
		{
			return context.Request.Path.StartsWithSegments("/swagger");

		}
		private JsonSerializerSettings JSONSettings()
		{
			return new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				//Converters = new List<JsonConverter> { new StringEnumConverter() }
			};
		}

		private ApiResponse GetErrorResponse(int code, APIError APIError)
		{
			return new ApiResponse(code, APIError) { };
		}
		
		private ApiResponse GetErrorResponse(int code, object content, APIError APIError)
		{
			
			return new ApiResponse(content,false,null);
		}
		
		private string ConvertToJSONString(ApiResponse APIResponse)
		{
			return JsonConvert.SerializeObject(APIResponse, JSONSettings());
		}

		private string ConvertToJSONString(object rawJSON)
		{
			return JsonConvert.SerializeObject(rawJSON, JSONSettings());
		}
	}

	
}
