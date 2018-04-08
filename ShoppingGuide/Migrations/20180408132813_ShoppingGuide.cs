using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShoppingGuide.Migrations
{
    public partial class ShoppingGuide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Patronimic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomersAdditional",
                columns: table => new
                {
                    Customerid = table.Column<Guid>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<long>(nullable: false),
                    PhoneFull = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersAdditional", x => x.Customerid);
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatePurchase = table.Column<DateTime>(nullable: false),
                    GustomerId = table.Column<Guid>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    SumReciept = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CustomersAdditional");

            migrationBuilder.DropTable(
                name: "Shipping");
        }
    }
}
