﻿using System.Configuration;
using System.Data.Entity;

namespace BasicMessageSender.Data.Models
{
    public class BMSContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<BlockedUsers> BlockedUsers { get; set; }

        public BMSContext() : base("BMS_DB")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                        .HasMany(g => g.BlockedUsers)
                        .WithRequired(s => s.BlockerUser)
                        .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Message>()
            //            .HasOptional(m => m.Receiver)
            //            .WithOptionalDependent()
            //            .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Message>()
            //            .HasOptional(m => m.Sender)
            //            .WithOptionalDependent()
            //            .WillCascadeOnDelete(false);
            //.HasForeignKey<int>(s => s.BlockedUserId);
        }
        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    {
        //        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BMSDatabase"].ConnectionString);
        //    }
    }
}
