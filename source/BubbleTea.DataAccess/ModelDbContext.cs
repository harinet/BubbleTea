using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BubbleTea.Models;

namespace BubbleTea.DataAccess
{
    class ModelDbContext : DbContext 
    {
        public ModelDbContext() : base("MyConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Size> Sizes { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemPrice> ItemPrices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<GroupOffering> GroupOfferings { get; set; }
        public DbSet<GroupItem> GroupItems { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
