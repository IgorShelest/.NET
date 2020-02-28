using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using WebAPIApplication.DbContext;
using WebAPIApplication.Dtos;
using WebAPIApplication.Models;

namespace WebAPIApplication.Services
{
    public class CustomerService
    {
        //---------------------------------------------------------------------

        private ApplicationDbContext _dbContext;

        //---------------------------------------------------------------------

        public CustomerService()
        {
            _dbContext = new ApplicationDbContext();
        }

        //---------------------------------------------------------------------

        public IEnumerable<CustomerModel> LoadCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        //---------------------------------------------------------------------

        public CustomerModel LoadCustomerById(int id)
        {
            return _dbContext.Customers.SingleOrDefault(customer => customer.Id == id);
        }

        //---------------------------------------------------------------------

        public CustomerModel LoadCustomerByInquiryCriteria(InquiryCriteriaDto criteria)
        {
            CustomerModel searchedCustomer = null;

            if (criteria.Id != null && !string.IsNullOrEmpty(criteria.Email) && !string.IsNullOrWhiteSpace(criteria.Email))
            {
                searchedCustomer = _dbContext.Customers.FirstOrDefault(customer =>
                    (customer.Id == criteria.Id) && (customer.Email == criteria.Email));
            }
            else if (criteria.Id != null)
            {
                searchedCustomer = _dbContext.Customers.FirstOrDefault(customer => customer.Id == criteria.Id);
            }
            else if (!string.IsNullOrEmpty(criteria.Email) && !string.IsNullOrWhiteSpace(criteria.Email))
            {
                searchedCustomer = _dbContext.Customers.FirstOrDefault(customer => customer.Email == criteria.Email);
            }

            return searchedCustomer;
        }

        //---------------------------------------------------------------------

        public int CreateCustomer(CustomerModel customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return customer.Id;
        }

        //---------------------------------------------------------------------

        public bool UpdateCustomer(CustomerModel customer)
        {
            var customerDoesNotExist = LoadCustomerById(customer.Id) == null;
            if (customerDoesNotExist)
                return false;

            _dbContext.Entry(customer).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return true;
        }

        //---------------------------------------------------------------------

        public bool DeleteCustomer(int id)
        {
            var customer = LoadCustomerById(id);
            if (customer == null)
                return false;

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();

            return true;
        }

        //---------------------------------------------------------------------
    }
}