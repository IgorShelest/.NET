using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAPIApplication.ValidationRules;

namespace WebAPIApplication.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public IEnumerable<TransactionModel> Transactions { get; set; }
    }
}