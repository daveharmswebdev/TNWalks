using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TNWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataDiffAndRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0c740696-c55b-43ba-b729-304dfb376cdd"), "Difficult" },
                    { new Guid("a3edbed0-6920-4f66-a3d5-5d55ee5fe4ab"), "Medium" },
                    { new Guid("fd271b89-4949-423d-a975-e9ed523065e5"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2f30f07d-58e2-4a66-b343-af3ca22fc58e"), "CR", "Clarksville Region", "https://picsum.photos/200/300" },
                    { new Guid("8d6865c7-436c-46bf-a3e3-9e0c18212db4"), "SM", "Smokey Mountains", "https://picsum.photos/200/300" },
                    { new Guid("b2105bdd-c2a1-49d5-ba18-326bf12ae6e2"), "UT", "Knoxville Region", "https://picsum.photos/200/300" },
                    { new Guid("f509a56f-292f-4df0-b4ac-2aa58a10697a"), "NR", "Nashville Region", "https://picsum.photos/200/300" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0c740696-c55b-43ba-b729-304dfb376cdd"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a3edbed0-6920-4f66-a3d5-5d55ee5fe4ab"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("fd271b89-4949-423d-a975-e9ed523065e5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2f30f07d-58e2-4a66-b343-af3ca22fc58e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8d6865c7-436c-46bf-a3e3-9e0c18212db4"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b2105bdd-c2a1-49d5-ba18-326bf12ae6e2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f509a56f-292f-4df0-b4ac-2aa58a10697a"));
        }
    }
}
