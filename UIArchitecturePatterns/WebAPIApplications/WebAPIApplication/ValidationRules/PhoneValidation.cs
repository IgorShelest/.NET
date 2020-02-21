using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebAPIApplication.Models;

namespace WebAPIApplication.ValidationRules
{
    public class PhoneValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (CustomerModel)validationContext.ObjectInstance;

            // Patterns:
            // (xxx)xxx-xx-xx
            // (xxx) xxx-xx-xx
            const string phonePattern = @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{2}-\d{2}";
            Match match = Regex.Match(customer.MobileNumber.ToString(), phonePattern, RegexOptions.IgnoreCase);

            return (match.Success)
                ? ValidationResult.Success
                : new ValidationResult("Phone Number is incorrect. Use '(xxx)xxx-xx-xx' pattern");
        }
    }
}