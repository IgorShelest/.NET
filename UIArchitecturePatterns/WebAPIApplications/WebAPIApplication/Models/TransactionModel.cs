using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIApplication.Models
{
    //-------------------------------------------------------------------------
    
    public enum CurrencyCode
    {
        UnknownCurrency, USD, JPY, THB, SGD
    }

    //-------------------------------------------------------------------------

    public enum TransactionStatus
    {
        UnknownStatus, Success, Failed, Cancelled
    }

    //-------------------------------------------------------------------------

    public class TransactionModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public CurrencyCode Currency { get; set; }
        public TransactionStatus Status { get; set; }

        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
    }

    //-------------------------------------------------------------------------
}