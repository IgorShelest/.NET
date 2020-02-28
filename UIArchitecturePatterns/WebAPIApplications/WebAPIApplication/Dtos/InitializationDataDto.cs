using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIApplication.Enums;

namespace WebAPIApplication.Dtos
{
    public class InitializationDataDto
    {
        //---------------------------------------------------------------------

        public Dictionary<int, string> TransactionStatusMappings = new Dictionary<int, string>
        {
            { (int) CurrencyCode.UnknownCurrency, CurrencyCode.UnknownCurrency.ToString() },
            { (int) CurrencyCode.USD, CurrencyCode.USD.ToString() },
            { (int) CurrencyCode.JPY, CurrencyCode.JPY.ToString() },
            { (int) CurrencyCode.THB, CurrencyCode.THB.ToString() },
            { (int) CurrencyCode.SGD, CurrencyCode.SGD.ToString() }
        };

        //---------------------------------------------------------------------

        public Dictionary<int, string> CurrencyCodeMappings = new Dictionary<int, string>
        {
            { (int)TransactionStatus.UnknownStatus, TransactionStatus.UnknownStatus.ToString() },
            { (int)TransactionStatus.Success, TransactionStatus.Success.ToString() },
            { (int)TransactionStatus.Failed, TransactionStatus.Failed.ToString() },
            { (int)TransactionStatus.Cancelled, TransactionStatus.Cancelled.ToString() }
        };

        //---------------------------------------------------------------------
    }
}