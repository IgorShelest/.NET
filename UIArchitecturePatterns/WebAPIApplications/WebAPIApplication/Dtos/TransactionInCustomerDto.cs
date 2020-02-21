using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIApplication.Models;

namespace WebAPIApplication.Dtos
{
    public class TransactionInCustomerDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Status { get; set; }
    }
}