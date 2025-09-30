using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UninunezRNIBackend.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgreementRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProposerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProposerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    ProposerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProposedOrganization = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RequiresMembershipPayment = table.Column<bool>(type: "bit", nullable: false),
                    MembershipPaymentAmount = table.Column<double>(type: "float", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    InternalObservations = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CommitteeComments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactPosition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AttachedDocumentsPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgreementRequestStatusHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgreementRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementRequestStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgreementRequestStatusHistories_AgreementRequests_AgreementRequestId",
                        column: x => x.AgreementRequestId,
                        principalTable: "AgreementRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementRequestStatusHistories_AgreementRequestId",
                table: "AgreementRequestStatusHistories",
                column: "AgreementRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreementRequestStatusHistories");

            migrationBuilder.DropTable(
                name: "AgreementRequests");
        }
    }
}
