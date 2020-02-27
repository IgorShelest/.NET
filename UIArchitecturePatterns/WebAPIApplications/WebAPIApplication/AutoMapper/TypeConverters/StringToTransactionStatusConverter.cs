using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebAPIApplication.Enums;
using WebAPIApplication.Models;

namespace WebAPIApplication.AutoMapper.TypeConverters
{
    public class StringToTransactionStatusConverter : ITypeConverter<string, TransactionStatus>
    {
        public TransactionStatus Convert(ResolutionContext context)
        {
            bool sourceIsValid = Enum.IsDefined(typeof(TransactionStatus), context.SourceValue);

            return sourceIsValid
                ? (TransactionStatus)Enum.Parse(typeof(TransactionStatus), context.SourceValue.ToString())
                : TransactionStatus.UnknownStatus;
        }
    }
}