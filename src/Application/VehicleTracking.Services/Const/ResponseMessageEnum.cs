using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VehicleTracking.Service.Const
{
    public enum ResponseMessageEnum
    {
        [Description("401 Unauthorised authentication failure.")]
        AuthenticationFailure,
        [Description("Request responded with exceptions.")]
        Exception
    }
}
