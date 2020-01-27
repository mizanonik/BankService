﻿// <auto-generated />
using System;
using BankServiceApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankServiceApi.Migrations
{
    [DbContext(typeof(BankServiceContext))]
    partial class BankServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BankServiceApi.Models.BankLedger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<int>("BankMasterId");

                    b.Property<int>("BatchId");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<int>("TransactionTypeId");

                    b.HasKey("Id");

                    b.HasIndex("BankMasterId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("BankLedgers");
                });

            modelBuilder.Entity("BankServiceApi.Models.BankMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<double>("Amount");

                    b.Property<DateTime>("LastTransactionDate");

                    b.HasKey("Id");

                    b.ToTable("BankMasters");
                });

            modelBuilder.Entity("BankServiceApi.Models.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeName");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeName = "Debit"
                        },
                        new
                        {
                            Id = 2,
                            TypeName = "Credit"
                        });
                });

            modelBuilder.Entity("BankServiceApi.Models.BankLedger", b =>
                {
                    b.HasOne("BankServiceApi.Models.BankMaster", "BankMaster")
                        .WithMany()
                        .HasForeignKey("BankMasterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BankServiceApi.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}