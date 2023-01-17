using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage3.Core;

namespace Garage3.Data
{
    public class Garage3Context : DbContext
    {
        public Garage3Context (DbContextOptions<Garage3Context> options)
            : base(options)
        {
        }
        public DbSet<Garage3.Core.Member> Member { get; set; } //= default!;

        public DbSet<Garage3.Core.Vehicle> Vehicle { get; set; } //= default!;
        //public DbSet<Garage3.Core.Receipt> Receipt { get; set; } //= default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.Member)
                .WithMany(c => c.Vehicles);
                //.HasForeignKey(o => o.MemberID);
        }
    }
}
