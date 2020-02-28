using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIApplication.Enums
{
    public enum TransactionStatus
    {
        UnknownStatus = 0, 
        Success = 1, 
        Failed = 2, 
        Cancelled = 3
    }
}