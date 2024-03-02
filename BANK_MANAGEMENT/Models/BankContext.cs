using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BANK_MANAGEMENT.Models;

public partial class BankContext : DbContext
{
    public BankContext()
    {
    }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<InterestRates> InterestRates { get; set; }

    public virtual DbSet<UserModel> User { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Bank;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<CustomerAccount>(entity =>
        {

            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.CustomerAadharno)
                .HasColumnType("decimal(12, 0)")
                .HasColumnName("CUSTOMER_AADHARNO");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_ADDRESS");
            entity.Property(e => e.CustomerDob)
                .HasColumnType("datetime")
                .HasColumnName("CUSTOMER_DOB");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_EMAIL");
            entity.Property(e => e.CustomerFirstname)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_FIRSTNAME");
            entity.Property(e => e.CustomerLastname)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_LASTNAME");
            entity.Property(e => e.CustomerMiddletname)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_MIDDLETNAME");
            entity.Property(e => e.CustomerMobileno)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("CUSTOMER_MOBILENO");
            entity.Property(e => e.AccountNumber)
              .HasColumnType("decimal(10, 0)")
              .HasColumnName("ACCOUNT_NUMBER");
            entity.Property(e => e.AccountStatus).HasColumnName("ACCOUNT_STATUS");
            entity.Property(e => e.AccountType).HasColumnName("ACCOUNT_TYPE");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("AMOUNT");

            entity.Property(e => e.InterestRate)
                .HasColumnType("float")
                .HasColumnName("INTEREST_RATE");
            entity.Property(e => e.OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("OPEN_DATE");

        });



        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountNumber).HasName("PK__ACCOUNTS__ECCCDF041A30CBF9");

            entity.ToTable("ACCOUNTS");

            entity.Property(e => e.AccountNumber)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(12, 0)")
                .HasColumnName("ACCOUNT_NUMBER");
            entity.Property(e => e.AccountStatus).HasColumnName("ACCOUNT_STATUS");
            entity.Property(e => e.AccountType).HasColumnName("ACCOUNT_TYPE");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(30, 0)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.CustomerId)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.InterestRate)
                .HasColumnType("float")
                .HasColumnName("INTEREST_RATE");
            entity.Property(e => e.OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("OPEN_DATE");


        });

        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__USERS__E0B75F6EB41BF55A");

            entity.ToTable("USERS");



            entity.Property(e => e.Role).HasColumnName("ROLE");


            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");


        });

        modelBuilder.Entity<InterestRates>(entity =>
        {
           

            entity.ToTable("INTEREST_RATE");

            
            
            entity.Property(e => e.AccountType).HasColumnName("ACCOUNT_TYPE");
          
            
            entity.Property(e => e.InterestRate)
                .HasColumnType("float")
                .HasColumnName("INTEREST_RATE");
           


        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__CUSTOMER__1CE12D3718BF440F");

            entity.ToTable("CUSTOMERS");

            entity.HasIndex(e => e.CustomerAadharno, "UQ__CUSTOMER__9C73A9F06A5CFCD8").IsUnique();

            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.CustomerAadharno)
                .HasColumnType("decimal(12, 0)")
                .HasColumnName("CUSTOMER_AADHARNO");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_ADDRESS");
            entity.Property(e => e.CustomerDob)
                .HasColumnType("datetime")
                .HasColumnName("CUSTOMER_DOB");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_EMAIL");
            entity.Property(e => e.CustomerFirstname)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_FIRSTNAME");
            entity.Property(e => e.CustomerLastname)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_LASTNAME");
            entity.Property(e => e.CustomerMiddletname)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_MIDDLETNAME");
            entity.Property(e => e.CustomerMobileno)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("CUSTOMER_MOBILENO");

        });
       

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__TRANSACT__16998B6133AF4217");

            entity.ToTable("TRANSACTIONS");

            entity.Property(e => e.TransactionId)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("TRANSACTION_ID");
            entity.Property(e => e.AccountNumber)
                .HasColumnType("decimal(12, 0)")
                .HasColumnName("ACCOUNT_NUMBER");
            entity.Property(e => e.ReceiverAccountNumber)
               .HasColumnType("decimal(12, 0)")
               .HasColumnName("RECEIVER_ACCOUNT_NUMBER");
            entity.Property(e => e.TansactionType)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("TANSACTION_TYPE");
            entity.Property(e => e.TransactionAmount)
                .HasColumnType("decimal(30, 0)")
                .HasColumnName("TRANSACTION_AMOUNT");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("TRANSACTION_DATE");
            entity.Property(e => e.TransactionDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TRANSACTION_DESCRIPTION");

            //entity.HasOne(d => d.AccountNumberNavigation).WithMany(p => p.Transactions)
            //    .HasForeignKey(d => d.AccountNumber)
            //    .HasConstraintName("FK__TRANSACTI__ACCOU__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
