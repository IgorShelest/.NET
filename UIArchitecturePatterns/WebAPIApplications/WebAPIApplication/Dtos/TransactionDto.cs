using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIApplication.Enums;
using WebAPIApplication.Models;

namespace WebAPIApplication.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public  CurrencyCode  Currency { get; set; }

        public TransactionStatus Status { get; set; }

        public int CustomerId { get; set; }
    }
}