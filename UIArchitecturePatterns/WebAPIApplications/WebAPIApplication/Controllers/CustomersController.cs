using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using WebAPIApplication.DbContext;
using WebAPIApplication.Dtos;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        //---------------------------------------------------------------------

        private ApplicationDbContext _dbContext;

        //---------------------------------------------------------------------

        CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        //---------------------------------------------------------------------

        private List<CustomerModel> LoadCustomersFromDb()
        {
            return _dbContext.Customers.ToList();
        }

        //---------------------------------------------------------------------

        private List<TransactionModel> LoadTransactionsFromDb()
        {
            return _dbContext.Transactions.ToList();
        }

        //---------------------------------------------------------------------

        private CustomerModel LoadCustomerByIdFromDb(int id)
        {
            return _dbContext.Customers.SingleOrDefault(customer => customer.Id == id);
        }

        //---------------------------------------------------------------------

        private CustomerModel LoadCustomerByInquiryCriteriaFromDb(InquiryCriteriaDto criteria)
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

        /// <summary>
        /// Get all the Customers
        /// </summary>
        /// <returns>IEnumerable of CustomerDto objects</returns>
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customers = LoadCustomersFromDb().Select(Mapper.Map<CustomerModel, CustomerDto>).ToList();

            return customers;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Get particular Customer by Id
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>IHttpActionResult.NotFound()</returns>
        /// <returns>IHttpActionResult.Ok(CustomerDto)</returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetCustomerById(int id)
        {
            var customer = LoadCustomerByIdFromDb(id);
            if (customer == null)
                NotFound();

            var customerDto = Mapper.Map<CustomerModel, CustomerDto>(customer);

            return Ok(customerDto);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Get particular Customer by Inquiry Criteria
        /// </summary>
        /// <param name="criteria">Includes parameters by which the Customer should be found</param>
        [HttpPost]
        [Route("inquiry")]
        public IHttpActionResult GetCustomerByInquiryCriteria(InquiryCriteriaDto criteria)
        {
            var customer = LoadCustomerByInquiryCriteriaFromDb(criteria);
            if (customer == null)
                NotFound();

            var customerDto = Mapper.Map<CustomerModel, CustomerDto>(customer);

            return Ok(customerDto);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="customerDto">Info on the basis of which Customer will be created</param>
        /// <returns>IHttpActionResult.Created(CustomerDto)</returns>
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, CustomerModel>(customerDto);

            // Add Customer to Db
            _dbContext.Customers.Add(customer);

            _dbContext.SaveChanges();

            // Update Customer with Db Ids
            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="customerDto">Data with which a particular Customer should be updated</param>
        /// <returns>IHttpActionResult.Ok()</returns>
        [HttpPut]
        public IHttpActionResult UpdateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = LoadCustomerByIdFromDb(customerDto.Id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // Update Customer in Db
            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            customer.MobileNumber = customerDto.MobileNumber;

            _dbContext.SaveChanges();

            return Ok();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Delete particular Customer
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>IHttpActionResult.Ok()</returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var searchedCustomer = LoadCustomerByIdFromDb(id);

            if (searchedCustomer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Customers.Remove(searchedCustomer);
            _dbContext.SaveChanges();

            return Ok();
        }

        //---------------------------------------------------------------------
    }
}
