using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebAPIApplication.Enums;
using WebAPIApplication.Models;

namespace WebAPIApplication.AutoMapper.TypeConverters
{
    public class TransactionStatusToStringConverter : ITypeConverter<TransactionStatus, string>
    {
        public string Convert(ResolutionContext context)
        {
            bool sourceIsValid = Enum.IsDefined(typeof(TransactionStatus), context.SourceValue);

            return sourceIsValid
                ? context.SourceValue.ToString()
                : TransactionStatus.UnknownStatus.ToString();
        }
    }
}