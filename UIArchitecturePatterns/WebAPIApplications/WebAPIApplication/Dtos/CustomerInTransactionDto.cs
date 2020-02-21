using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAPIApplication.ValidationRules;

namespace WebAPIApplication.Dtos
{
    public class CustomerInTransactionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Email Address is invalid")]
        public string Email { get; set; }

        [PhoneValidation(ErrorMessage = "Phone Number is incorrect. Use '(xxx)xxx-xx-xx' pattern")]
        public int MobileNumber { get; set; }
    }
}