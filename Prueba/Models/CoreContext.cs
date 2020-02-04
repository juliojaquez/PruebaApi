using Microsoft.EntityFrameworkCore;
using PruebaAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Models
{
    public class CoreContext : DbContext
    {

        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Receipt> Receipts { get; set; }



    }
}
