using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using AutoMapper;
using WebAPIApplication.DbContext;
using WebAPIApplication.Models;

namespace WebAPIApplication.Services
{
    public class TransactionService
    {
        //---------------------------------------------------------------------

        private ApplicationDbContext _dbContext;

        //---------------------------------------------------------------------

        public TransactionService()
        {
            _dbContext = new ApplicationDbContext();
        }

        //---------------------------------------------------------------------

        public TransactionModel LoadById(int id)
        {
            return _dbContext.Transactions
                .SingleOrDefault(transaction => transaction.Id == id);
        }

        //---------------------------------------------------------------------

        public IEnumerable<TransactionModel> LoadByCustomerId(int id)
        {
            return _dbContext.Transactions
                .Where(transaction => transaction.CustomerId == id);
        }

        //---------------------------------------------------------------------

        public int Create(TransactionModel transaction)
        {
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return transaction.Id;
        }

        //---------------------------------------------------------------------

        public bool Update(TransactionModel transaction)
        {
            var transactionDoesNotExist = LoadById(transaction.Id) == null;
            if (transactionDoesNotExist)
                return false;

            _dbContext.Entry(transaction).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return true;
        }

        //---------------------------------------------------------------------

        public bool Delete(int id)
        {
            var transaction = LoadById(id);
            if (transaction == null)
                return false;

            _dbContext.Transactions.Remove(transaction);
            _dbContext.SaveChanges();

            return true;
        }

        //---------------------------------------------------------------------
    }
}