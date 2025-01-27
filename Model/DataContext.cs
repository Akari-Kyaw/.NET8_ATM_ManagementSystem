using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("deciaml(18,4)");
            });
            modelBuilder.Entity<Transactions>().Property(t => t.Amount).HasPrecision(18, 4);

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }


    }
}
