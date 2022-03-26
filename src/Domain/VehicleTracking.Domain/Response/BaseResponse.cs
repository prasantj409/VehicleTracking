using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace VehicleTracking.Domain.Response
{
    public class BaseResponse<T>
    {
        [DataMember]
        public bool success { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Message { get; private set; } 
        
        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }

        protected BaseResponse(T resource)
        {

            Message = string.Empty;
            Result = resource;
        }

        protected BaseResponse(string message)
        {

            Message = message;
        }
       
    }
}
