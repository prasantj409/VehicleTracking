using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Domain.Response
{
    public class GenericResponse<T> : BaseResponse<T>
    {
        public GenericResponse(T resource) : base(resource) { }

        public GenericResponse(string message) : base(message) { }
    }
}
