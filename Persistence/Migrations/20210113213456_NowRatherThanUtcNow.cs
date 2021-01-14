using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class NowRatherThanUtcNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "TaskSets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(9443),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(9643));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "TaskSets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(9128),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(9336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(8303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(8500));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(7354),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(7640));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(5491),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(6201));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 92, DateTimeKind.Local).AddTicks(7858),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 751, DateTimeKind.Local).AddTicks(4088));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "TaskSets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(9643),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(9443));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "TaskSets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(9336),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(9128));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(8500),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(8303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(7640),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(7354));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 761, DateTimeKind.Local).AddTicks(6201),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 102, DateTimeKind.Local).AddTicks(5491));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 22, 30, 55, 751, DateTimeKind.Local).AddTicks(4088),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 13, 22, 34, 56, 92, DateTimeKind.Local).AddTicks(7858));
        }
    }
}
