using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop2City.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class dfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_productCategory",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_productGroupId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_productSubCategory",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_userId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "updateDateTime",
                schema: "Production",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "userId",
                schema: "Production",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "shortDescription",
                schema: "Production",
                table: "Products",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "productGroupId",
                schema: "Production",
                table: "Products",
                newName: "ProductGroupId");

            migrationBuilder.RenameColumn(
                name: "fullDescription",
                schema: "Production",
                table: "Products",
                newName: "FullDescription");

            migrationBuilder.RenameColumn(
                name: "registerDateTime",
                schema: "Production",
                table: "Products",
                newName: "LastUpdateDate");

            migrationBuilder.RenameColumn(
                name: "productSubCategory",
                schema: "Production",
                table: "Products",
                newName: "SubCategoryId");

            migrationBuilder.RenameColumn(
                name: "productIsDelete",
                schema: "Production",
                table: "Products",
                newName: "IsStatus");

            migrationBuilder.RenameColumn(
                name: "productCategory",
                schema: "Production",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "productId",
                schema: "Production",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_userId",
                schema: "Production",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_productGroupId",
                schema: "Production",
                table: "Products",
                newName: "IX_Products_ProductGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_productSubCategory",
                schema: "Production",
                table: "Products",
                newName: "IX_Products_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_productCategory",
                schema: "Production",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "isFinaly",
                schema: "Orders",
                table: "Factors",
                newName: "IsFinaly");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UserLevels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SlideShows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                schema: "Production",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Production",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                schema: "Production",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Orders",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Orders",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TypePostId",
                schema: "Orders",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "startDate",
                table: "DisCounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "endDate",
                table: "DisCounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "DisCounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DisCounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "DisCounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "DisCounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "DisCounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CategorySelectedCalculations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Calculations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CalculationDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Wages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TypePostId",
                schema: "Orders",
                table: "Orders",
                column: "TypePostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TypePosts_TypePostId",
                schema: "Orders",
                table: "Orders",
                column: "TypePostId",
                principalTable: "TypePosts",
                principalColumn: "TypePostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_CategoryId",
                schema: "Production",
                table: "Products",
                column: "CategoryId",
                principalTable: "ProductGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_ProductGroupId",
                schema: "Production",
                table: "Products",
                column: "ProductGroupId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_SubCategoryId",
                schema: "Production",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "ProductGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                schema: "Production",
                table: "Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TypePosts_TypePostId",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_CategoryId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_ProductGroupId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_SubCategoryId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Wages");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TypePostId",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TypePostId",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "DisCounts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DisCounts");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "DisCounts");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "DisCounts");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "DisCounts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Production",
                table: "Products",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                schema: "Production",
                table: "Products",
                newName: "shortDescription");

            migrationBuilder.RenameColumn(
                name: "ProductGroupId",
                schema: "Production",
                table: "Products",
                newName: "productGroupId");

            migrationBuilder.RenameColumn(
                name: "FullDescription",
                schema: "Production",
                table: "Products",
                newName: "fullDescription");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                schema: "Production",
                table: "Products",
                newName: "productSubCategory");

            migrationBuilder.RenameColumn(
                name: "LastUpdateDate",
                schema: "Production",
                table: "Products",
                newName: "registerDateTime");

            migrationBuilder.RenameColumn(
                name: "IsStatus",
                schema: "Production",
                table: "Products",
                newName: "productIsDelete");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "Production",
                table: "Products",
                newName: "productCategory");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Production",
                table: "Products",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                schema: "Production",
                table: "Products",
                newName: "IX_Products_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductGroupId",
                schema: "Production",
                table: "Products",
                newName: "IX_Products_productGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SubCategoryId",
                schema: "Production",
                table: "Products",
                newName: "IX_Products_productSubCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                schema: "Production",
                table: "Products",
                newName: "IX_Products_productCategory");

            migrationBuilder.RenameColumn(
                name: "IsFinaly",
                schema: "Orders",
                table: "Factors",
                newName: "isFinaly");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UserLevels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SlideShows",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "updateDateTime",
                schema: "Production",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Orders",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "startDate",
                table: "DisCounts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "endDate",
                table: "DisCounts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CategorySelectedCalculations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Calculations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CalculationDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_productCategory",
                schema: "Production",
                table: "Products",
                column: "productCategory",
                principalTable: "ProductGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_productGroupId",
                schema: "Production",
                table: "Products",
                column: "productGroupId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_productSubCategory",
                schema: "Production",
                table: "Products",
                column: "productSubCategory",
                principalTable: "ProductGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_userId",
                schema: "Production",
                table: "Products",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
