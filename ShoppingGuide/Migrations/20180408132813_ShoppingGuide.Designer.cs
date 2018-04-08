﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ShoppingGuide.DbModels;
using System;

namespace ShoppingGuide.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20180408132813_ShoppingGuide")]
    partial class ShoppingGuide
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShoppingGuide.DbModels.Customers", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Patronimic");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ShoppingGuide.DbModels.CustomersAdditional", b =>
                {
                    b.Property<Guid>("Customerid")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDay");

                    b.Property<string>("Email");

                    b.Property<long>("Phone");

                    b.Property<string>("PhoneFull");

                    b.HasKey("Customerid");

                    b.ToTable("CustomersAdditional");
                });

            modelBuilder.Entity("ShoppingGuide.DbModels.Shipping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatePurchase");

                    b.Property<Guid>("GustomerId");

                    b.Property<string>("Photo");

                    b.Property<decimal>("SumReciept");

                    b.HasKey("Id");

                    b.ToTable("Shipping");
                });
#pragma warning restore 612, 618
        }
    }
}
