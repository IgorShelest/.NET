using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using WebAPIApplication.DbContext;
using WebAPIApplication.Dtos;
using WebAPIApplication.Models;
using WebAPIApplication.Services;

namespace WebAPIApplication.Controllers
{
    [RoutePrefix("api/transactions")]
    public class TransactionsController : ApiController
    {
        //---------------------------------------------------------------------

        private TransactionService _transactionService;

        //---------------------------------------------------------------------

        public TransactionsController()
        {
            _transactionService = new TransactionService();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Get particular Transaction
        /// </summary>
        /// <returns>IEnumerable of TransactionDto objects</returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetTransaction(int id)
        {
            var transaction = _transactionService.LoadById(id);

            if (transaction == null)
                NotFound();

            return Ok(Mapper.Map<TransactionModel, TransactionDto>(transaction));
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Get particular Transaction
        /// </summary>
        /// <returns>IEnumerable of TransactionDto objects</returns>
        [HttpGet]
        [Route("byCustomer/{id}")]
        public IEnumerable<TransactionDto> GetTransactionByCustomerId(int id)
        {
            return _transactionService.LoadByCustomerId(id)
                .Select(Mapper.Map<TransactionModel, TransactionDto>);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Create Transaction
        /// </summary>
        /// <param name="transactionDto">Info on the basis of which Transaction will be created</param>
        /// <returns>IHttpActionResult.Created(TransactionDto)</returns>
        [HttpPost]
        public IHttpActionResult CreateTransaction(TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transaction = Mapper.Map<TransactionDto, TransactionModel>(transactionDto);

            transactionDto.Id = _transactionService.Create(transaction);

            return Created(new Uri(Request.RequestUri + "/" + transactionDto.Id), transactionDto);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Update Transaction
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transactionDto"></param>
        /// <returns>IHttpActionResult.Ok()</returns>
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateTransaction(int id, TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var transaction = Mapper.Map<TransactionDto, TransactionModel>(transactionDto);

            var updateResult = _transactionService.Update(transaction);
            if (!updateResult)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Delete particular Transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IHttpActionResult.Ok()</returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteTransaction(int id)
        {
            var deleteResult = _transactionService.Delete(id);
            if (!deleteResult)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok();
        }

        //---------------------------------------------------------------------
    }
}
