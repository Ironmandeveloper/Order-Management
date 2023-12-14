using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileDumpUsingPostgreSQL.Migrations
{
    public partial class tablealtered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "FileUpload",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FileExtension",
                table: "FileUpload",
                newName: "Image");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice",
                table: "FileUpload",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "FileUpload",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "FileUpload",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "FileUpload");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "FileUpload");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "FileUpload");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FileUpload",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "FileUpload",
                newName: "FileExtension");
        }
    }
}
