using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebAPIApplication.Models;

namespace WebAPIApplication.AutoMapper.TypeConverters
{
    public class CurrencyCodeToStringConverter : ITypeConverter<CurrencyCode, string>
    {
        public string Convert(ResolutionContext context)
        {
            bool sourceIsValid = Enum.IsDefined(typeof(CurrencyCode), context.SourceValue);

            return sourceIsValid
                ? context.SourceValue.ToString()
                : CurrencyCode.UnknownCurrency.ToString();
        }
    }
}