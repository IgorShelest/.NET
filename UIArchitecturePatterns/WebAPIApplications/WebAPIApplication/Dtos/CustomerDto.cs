using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIApplication.Models;

namespace WebAPIApplication.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int MobileNumber { get; set; }
        public IEnumerable<TransactionInCustomerDto> Transactions { get; set; }
    }
}