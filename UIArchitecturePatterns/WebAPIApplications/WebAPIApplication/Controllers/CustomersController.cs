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
using WebAPIApplication.Services;

namespace WebAPIApplication.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        //---------------------------------------------------------------------

        private readonly CustomerService _customerService;

        //---------------------------------------------------------------------

        public CustomersController()
        {
            _customerService = new CustomerService();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Get all the Customers
        /// </summary>
        /// <returns>IEnumerable of CustomerDto objects</returns>
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customerDtos = 
                _customerService.LoadCustomers().Select(Mapper.Map<CustomerModel, CustomerDto>);

            return customerDtos;
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
            var customer = _customerService.LoadCustomerById(id);
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
            var customer = _customerService.LoadCustomerByInquiryCriteria(criteria);
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
            
            // Update Customer with Db Ids
            customerDto.Id = _customerService.CreateCustomer(customer);

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

            var customer = Mapper.Map<CustomerDto, CustomerModel>(customerDto);

            var updateResult = _customerService.UpdateCustomer(customer);
            if (!updateResult)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
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
            var deleteResult = _customerService.DeleteCustomer(id);
            if (!deleteResult)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
            return Ok();
        }

        //---------------------------------------------------------------------
    }
}
