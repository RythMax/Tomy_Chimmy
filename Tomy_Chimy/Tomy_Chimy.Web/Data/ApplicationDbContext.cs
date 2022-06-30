using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tomy_Chimy.Web.Data.Entities;

namespace Tomy_Chimy.Web.Data
{
    //4174
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Food> Comidas { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<FoodType> FoodTypes { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<PayingMethod> PayingMethods { get; set; }

        public DbSet<Queue> Queues { get; set; }

        public DbSet<QueueDetail> QueueDetails { get; set; }

        public DbSet<Status> Statuses { get; set; }
}
}
