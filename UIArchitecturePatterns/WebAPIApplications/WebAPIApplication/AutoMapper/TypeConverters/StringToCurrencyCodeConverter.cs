using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebAPIApplication.Enums;
using WebAPIApplication.Models;

namespace WebAPIApplication.AutoMapper.TypeConverters
{
    public class StringToCurrencyCodeConverter : ITypeConverter<string, CurrencyCode>
    {
        public CurrencyCode Convert(ResolutionContext context)
        {
            bool sourceIsValid = Enum.IsDefined(typeof(CurrencyCode), context.SourceValue);

            return sourceIsValid
                ? (CurrencyCode)Enum.Parse(typeof(CurrencyCode), context.SourceValue.ToString())
                : CurrencyCode.UnknownCurrency;
        }
    }
}