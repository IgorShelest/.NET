using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPIApplication.Models;

namespace WebAPIApplication.DbContext
{
    public class ApplicationDbContext : System.Data.Entity.DbContext
    {
        public ApplicationDbContext() : base("DbContextConnectionString")
        {
        }

        public virtual DbSet<TransactionModel> Transactions { get; set; }
        public virtual DbSet<CustomerModel> Customers { get; set; }
    }
}