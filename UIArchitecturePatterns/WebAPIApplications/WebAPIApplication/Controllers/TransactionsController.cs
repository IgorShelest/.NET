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

        private List<TransactionModel> LoadTransactionsFromDb()
        {
            return _dbContext.Transactions
                .Include("Customer")
                .ToList();
        }

        //---------------------------------------------------------------------

        private TransactionModel LoadTransactionByIdFromDb(int id)
        {
            return _dbContext.Transactions
                .Include("Customer")
                .SingleOrDefault(transaction => transaction.Id == id);
        }

        //---------------------------------------------------------------------

        // GET /api/transactions/id
        [HttpGet]
        public IHttpActionResult GetTransaction(int id)
        {
            var transaction = LoadTransactionByIdFromDb(id);

            if (transaction == null)
                NotFound();

            return Ok(Mapper.Map<TransactionModel, TransactionDto>(transaction));
        }

        //---------------------------------------------------------------------

        // POST /api/transactions
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

        // PUT /api/transactions/id
        [HttpPut]
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

        // DELETE /api/transactions/id
        [HttpDelete]
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
