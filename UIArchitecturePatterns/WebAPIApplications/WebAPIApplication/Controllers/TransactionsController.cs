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

namespace WebAPIApplication.Controllers
{
    [RoutePrefix("api/transactions")]
    public class TransactionsController : ApiController
    {
        //---------------------------------------------------------------------

        private ApplicationDbContext _dbContext;

        //---------------------------------------------------------------------

        TransactionsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        //---------------------------------------------------------------------

        private TransactionModel LoadTransactionByIdFromDb(int id)
        {
            return _dbContext.Transactions
                .SingleOrDefault(transaction => transaction.Id == id);
        }

        //---------------------------------------------------------------------

        private List<TransactionModel> LoadTransactionsByCustomerIdFromDb(int id)
        {
            return _dbContext.Transactions
                .Where(transaction => transaction.CustomerId == id)
                .ToList();
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
            var transaction = LoadTransactionByIdFromDb(id);

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
            return LoadTransactionsByCustomerIdFromDb(id).Select(Mapper.Map<TransactionModel, TransactionDto>);
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

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            transactionDto.Id = transaction.Id;

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

            var transaction = LoadTransactionByIdFromDb(id);

            if (transaction == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(transactionDto, transaction);

            _dbContext.SaveChanges();

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
            var searchedTransaction = LoadTransactionByIdFromDb(id);

            if (searchedTransaction == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Transactions.Remove(searchedTransaction);
            _dbContext.SaveChanges();

            return Ok();
        }

        //---------------------------------------------------------------------
    }
}
