using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryID = table.Column<string>(maxLength: 50, nullable: false),
                    CountryName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    PaymentStatusID = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.PaymentStatusID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyType",
                columns: table => new
                {
                    PropertyTypeID = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyType", x => x.PropertyTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ReservationStatus",
                columns: table => new
                {
                    ReservationStatusID = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationStatus", x => x.ReservationStatusID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityID = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CountryId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_Cities_Countries",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    PropertyID = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "(N'PropertyName')"),
                    Description = table.Column<string>(maxLength: 100, nullable: true, defaultValueSql: "(N'Property Discription')"),
                    Introduction = table.Column<string>(maxLength: 200, nullable: true, defaultValueSql: "(N'Property Introduction')"),
                    PropertyTypeID = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "((1))"),
                    Rating = table.Column<double>(nullable: false, defaultValueSql: "((5.0))"),
                    TimeCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TimeUpdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    OwnerID = table.Column<string>(maxLength: 50, nullable: false),
                    PricePerNight = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes",
                        column: x => x.PropertyTypeID,
                        principalTable: "PropertyType",
                        principalColumn: "PropertyTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<string>(maxLength: 50, nullable: false),
                    PropertyID = table.Column<string>(maxLength: 50, nullable: false),
                    UserID = table.Column<string>(maxLength: 450, nullable: false),
                    Comment = table.Column<string>(maxLength: 200, nullable: false, defaultValueSql: "(N'Default comment')"),
                    TimeCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TimeUpdated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Properties",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageID = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Url = table.Column<string>(maxLength: 100, nullable: false),
                    PropertyID = table.Column<string>(maxLength: 50, nullable: false),
                    Alt = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_Images_Properties",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAddress",
                columns: table => new
                {
                    PropertyID = table.Column<string>(maxLength: 50, nullable: false),
                    CityID = table.Column<string>(maxLength: 50, nullable: false),
                    StreetName = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "(N'Street Name default')"),
                    Number = table.Column<string>(unicode: false, maxLength: 10, nullable: true, defaultValueSql: "('Street number default')"),
                    Description = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAddresses", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_RoomAddresses_Cities",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomAddresses_Properties",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHistory",
                columns: table => new
                {
                    ReservationID = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PropertyID = table.Column<string>(maxLength: 50, nullable: false),
                    UserID = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    ToDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    TimeCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TotalCost = table.Column<decimal>(type: "money", nullable: false),
                    ReservationStatusID = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "((1))"),
                    PaymentStatusID = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHistories", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_ReservationHistories_PaymentStatus",
                        column: x => x.PaymentStatusID,
                        principalTable: "PaymentStatus",
                        principalColumn: "PaymentStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHistories_Properties",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHistories_ReservationStatus",
                        column: x => x.ReservationStatusID,
                        principalTable: "ReservationStatus",
                        principalColumn: "ReservationStatusID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPropertyLike",
                columns: table => new
                {
                    UserID = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PropertyID = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPropertyLikes", x => new { x.UserID, x.PropertyID });
                    table.ForeignKey(
                        name: "FK_UserPropertyLikes_Properties",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    CommentID = table.Column<string>(maxLength: 50, nullable: false),
                    ReplyToCommentID = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Replies_Comments",
                        column: x => x.CommentID,
                        principalTable: "Comment",
                        principalColumn: "CommentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Replies_Comments1",
                        column: x => x.ReplyToCommentID,
                        principalTable: "Comment",
                        principalColumn: "CommentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PropertyID",
                table: "Comment",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Image_PropertyID",
                table: "Image",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_PropertyTypeID",
                table: "Property",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAddress_CityID",
                table: "PropertyAddress",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_ReplyToCommentID",
                table: "Reply",
                column: "ReplyToCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistory_PaymentStatusID",
                table: "ReservationHistory",
                column: "PaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistory_PropertyID",
                table: "ReservationHistory",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistory_ReservationStatusID",
                table: "ReservationHistory",
                column: "ReservationStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPropertyLike_PropertyID",
                table: "UserPropertyLike",
                column: "PropertyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "PropertyAddress");

            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "ReservationHistory");

            migrationBuilder.DropTable(
                name: "UserPropertyLike");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "ReservationStatus");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "PropertyType");
        }
    }
}
