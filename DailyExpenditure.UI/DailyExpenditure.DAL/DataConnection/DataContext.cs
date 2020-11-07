using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DailyExpenditure.DAL.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DailyExpenditure.DAL.DataConnection
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=ProductsContext")
        {

        }
        public DbSet<UserDetail> UserDetail { get; set; }
        public DbSet<ExpenseTypeMaster> ExpenseTypeMaster { get; set; }
        public DbSet<PaymentTypeMaster> PaymentTypeMaster { get; set; }
        public DbSet<ExpenseMaster> ExpenseMaster { get; set; }
        public DbSet<SalaryMaster> SalaryMaster { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Create table for UserDetail
            modelBuilder.Entity<UserDetail>().HasKey(u => u.UserId); //primary key defination  
            modelBuilder.Entity<UserDetail>().Property(u => u.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);  //identity col 
            //Create table for ExpenseTypeMaster
            modelBuilder.Entity<ExpenseTypeMaster>().HasKey(e => e.ExpenseTypeId); //primary key defination  
            modelBuilder.Entity<ExpenseTypeMaster>().Property(e => e.ExpenseTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);  //identity col 

            modelBuilder.Entity<ExpenseTypeMaster>().HasRequired(e => e.UserDetails)
                .WithMany(u => u.ExpenseTypeMasters).HasForeignKey(e => e.UserId); //Foreign Key 

            //Create table for SalaryMaster
            modelBuilder.Entity<SalaryMaster>().HasKey(e => e.SalaryId); //primary key defination  
            modelBuilder.Entity<SalaryMaster>().Property(e => e.SalaryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);  //identity col 
            //Create table for PaymentTypeMaster
            modelBuilder.Entity<PaymentTypeMaster>().HasKey(p => p.PaymentTypeId); //primary key defination  
            modelBuilder.Entity<PaymentTypeMaster>().Property(p => p.PaymentTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);  //identity col

            modelBuilder.Entity<PaymentTypeMaster>().HasRequired(e => e.UserDetails)
                .WithMany(u => u.PaymentTypeMasters).HasForeignKey(e => e.UserId); //Foreign Key 

            //Create table for ExpenseMaster
            modelBuilder.Entity<ExpenseMaster>().HasKey(e => e.ExpenseId);
            modelBuilder.Entity<ExpenseMaster>().Property(e => e.ExpenseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ExpenseMaster>().HasRequired(e => e.UserDetails)
                .WithMany(u => u.ExpenseMasters).HasForeignKey(e => e.UserId); //Foreign Key 

            modelBuilder.Entity<ExpenseMaster>().HasRequired(e => e.ExpenseTypeMasters)
               .WithMany(u => u.ExpenseMasters).HasForeignKey(e => e.ExpenseTypeId); //Foreign Key 

            modelBuilder.Entity<ExpenseMaster>().HasRequired(e => e.PaymentTypeMasters)
               .WithMany(u => u.ExpenseMasters).HasForeignKey(e => e.PaymentTypeId); //Foreign Key 

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
         
        }

    }
}
