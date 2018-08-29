using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkOrder.Infra.Migrations
{
    public partial class WorkOrderVictorV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "PageComponents",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    AppName = table.Column<string>(nullable: true),
                    Hostname = table.Column<string>(maxLength: 300, nullable: false),
                    Theme = table.Column<string>(nullable: true),
                    ConnectionString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Role = table.Column<int>(nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    Password_ConfirmPassword = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Phone_Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    PublicPlace = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Complement = table.Column<string>(nullable: true),
                    AddressType = table.Column<int>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomForms",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomForms_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomFields",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Mandatory = table.Column<bool>(nullable: false),
                    CustomFormId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomFields_CustomForms_CustomFormId",
                        column: x => x.CustomFormId,
                        principalSchema: "dbo",
                        principalTable: "CustomForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomFields_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomFormAnswers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    CustomFormId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFormAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomFormAnswers_CustomForms_CustomFormId",
                        column: x => x.CustomFormId,
                        principalSchema: "dbo",
                        principalTable: "CustomForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomFormAnswers_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomFormAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormPageComponents",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    PageComponentId = table.Column<Guid>(nullable: false),
                    CustomFormId = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormPageComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormPageComponents_CustomForms_CustomFormId",
                        column: x => x.CustomFormId,
                        principalSchema: "dbo",
                        principalTable: "CustomForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormPageComponents_PageComponents_PageComponentId",
                        column: x => x.PageComponentId,
                        principalSchema: "dbo",
                        principalTable: "PageComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormPageComponents_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomFieldOptions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CustomFieldId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFieldOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomFieldOptions_CustomFields_CustomFieldId",
                        column: x => x.CustomFieldId,
                        principalSchema: "dbo",
                        principalTable: "CustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomFieldOptions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomFieldAnswers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    CustomFormAnswerId = table.Column<Guid>(nullable: false),
                    CustomFieldId = table.Column<Guid>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    CustomFieldOptionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFieldAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomFieldAnswers_CustomFields_CustomFieldId",
                        column: x => x.CustomFieldId,
                        principalSchema: "dbo",
                        principalTable: "CustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomFieldAnswers_CustomFieldOptions_CustomFieldOptionId",
                        column: x => x.CustomFieldOptionId,
                        principalSchema: "dbo",
                        principalTable: "CustomFieldOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomFieldAnswers_CustomFormAnswers_CustomFormAnswerId",
                        column: x => x.CustomFormAnswerId,
                        principalSchema: "dbo",
                        principalTable: "CustomFormAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomFieldAnswers_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_TenantId",
                schema: "dbo",
                table: "Address",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFieldAnswers_CustomFieldId",
                schema: "dbo",
                table: "CustomFieldAnswers",
                column: "CustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFieldAnswers_CustomFieldOptionId",
                schema: "dbo",
                table: "CustomFieldAnswers",
                column: "CustomFieldOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFieldAnswers_CustomFormAnswerId",
                schema: "dbo",
                table: "CustomFieldAnswers",
                column: "CustomFormAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFieldAnswers_TenantId",
                schema: "dbo",
                table: "CustomFieldAnswers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFieldOptions_CustomFieldId",
                schema: "dbo",
                table: "CustomFieldOptions",
                column: "CustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFieldOptions_TenantId",
                schema: "dbo",
                table: "CustomFieldOptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFields_CustomFormId",
                schema: "dbo",
                table: "CustomFields",
                column: "CustomFormId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFields_TenantId",
                schema: "dbo",
                table: "CustomFields",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFormAnswers_CustomFormId",
                schema: "dbo",
                table: "CustomFormAnswers",
                column: "CustomFormId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFormAnswers_TenantId",
                schema: "dbo",
                table: "CustomFormAnswers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFormAnswers_UserId",
                schema: "dbo",
                table: "CustomFormAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomForms_TenantId",
                schema: "dbo",
                table: "CustomForms",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_FormPageComponents_CustomFormId",
                schema: "dbo",
                table: "FormPageComponents",
                column: "CustomFormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormPageComponents_PageComponentId",
                schema: "dbo",
                table: "FormPageComponents",
                column: "PageComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_FormPageComponents_TenantId",
                schema: "dbo",
                table: "FormPageComponents",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Hostname",
                schema: "dbo",
                table: "Tenants",
                column: "Hostname",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomFieldAnswers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FormPageComponents",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomFieldOptions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomFormAnswers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PageComponents",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomFields",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CustomForms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "dbo");
        }
    }
}
