﻿// <auto-generated />
using System;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HostelWebAPI.Migrations
{
    [DbContext(typeof(HostelDBContext))]
    [Migration("20210502210205_fix_reviewtable")]
    partial class fix_reviewtable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HostelWebAPI.Models.City", b =>
                {
                    b.Property<string>("CityId")
                        .HasColumnName("CityID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("HostelWebAPI.Models.Comment", b =>
                {
                    b.Property<string>("CommentId")
                        .HasColumnName("CommentID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CommentString")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Comment")
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValueSql("(N'Default comment')")
                        .HasMaxLength(200);

                    b.Property<string>("PropertyId")
                        .IsRequired()
                        .HasColumnName("PropertyID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("TimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("TimeUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("UserID")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.HasKey("CommentId");

                    b.HasIndex("PropertyId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("HostelWebAPI.Models.Country", b =>
                {
                    b.Property<string>("CountryId")
                        .HasColumnName("CountryID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CountryId");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("HostelWebAPI.Models.Image", b =>
                {
                    b.Property<string>("ImageId")
                        .HasColumnName("ImageID")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Alt")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("DeleteHash")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("PropertyId")
                        .IsRequired()
                        .HasColumnName("PropertyID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ImageId");

                    b.HasIndex("PropertyId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("HostelWebAPI.Models.PaymentStatus", b =>
                {
                    b.Property<string>("PaymentStatusId")
                        .HasColumnName("PaymentStatusID")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("PaymentStatusId");

                    b.ToTable("PaymentStatus");
                });

            modelBuilder.Entity("HostelWebAPI.Models.Property", b =>
                {
                    b.Property<string>("PropertyId")
                        .HasColumnName("PropertyID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<decimal>("CleaningFee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValue(0m);

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("(N'Property Discription')")
                        .HasMaxLength(100);

                    b.Property<string>("Introduction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(2000)")
                        .HasDefaultValueSql("(N'Property Introduction')")
                        .HasMaxLength(2000);

                    b.Property<int>("MaxPeople")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("(N'PropertyName')")
                        .HasMaxLength(50);

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnName("OwnerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("money");

                    b.Property<string>("PropertyTypeId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PropertyTypeID")
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("((1))")
                        .HasMaxLength(50);

                    b.Property<decimal>("ServiceFee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValue(0m);

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("TimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("TimeUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("TotalReview")
                        .HasColumnType("int");

                    b.Property<int>("TotalStar")
                        .HasColumnType("int");

                    b.HasKey("PropertyId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PropertyTypeId");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("HostelWebAPI.Models.PropertyAddress", b =>
                {
                    b.Property<string>("PropertyId")
                        .HasColumnName("PropertyID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CityId")
                        .IsRequired()
                        .HasColumnName("CityID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Number")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("(N'Street Name default')")
                        .HasMaxLength(50);

                    b.HasKey("PropertyId")
                        .HasName("PK_RoomAddresses");

                    b.HasIndex("CityId");

                    b.ToTable("PropertyAddress");
                });

            modelBuilder.Entity("HostelWebAPI.Models.PropertyService", b =>
                {
                    b.Property<string>("PropertyId")
                        .HasColumnName("PropertyId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("Breakfast")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("FreeParking")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("Kitchen")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("PetAllowed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("Wifi")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("PropertyId");

                    b.ToTable("PropertyServices");
                });

            modelBuilder.Entity("HostelWebAPI.Models.PropertyType", b =>
                {
                    b.Property<string>("PropertyTypeId")
                        .HasColumnName("PropertyTypeID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ThumbnailImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("PropertyTypeId");

                    b.ToTable("PropertyType");
                });

            modelBuilder.Entity("HostelWebAPI.Models.Reply", b =>
                {
                    b.Property<string>("CommentId")
                        .HasColumnName("CommentID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ReplyToCommentId")
                        .IsRequired()
                        .HasColumnName("ReplyToCommentID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CommentId")
                        .HasName("PK_Replies");

                    b.HasIndex("ReplyToCommentId");

                    b.ToTable("Reply");
                });

            modelBuilder.Entity("HostelWebAPI.Models.ReservationHistory", b =>
                {
                    b.Property<string>("ReservationId")
                        .HasColumnName("ReservationID")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("AdultNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("ChildrenNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("FromDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("InfantNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("PaymentStatusId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PaymentStatusID")
                        .HasColumnType("varchar(50)")
                        .HasDefaultValueSql("((1))")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("PropertyId")
                        .HasColumnName("PropertyID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ReservationStatusId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ReservationStatusID")
                        .HasColumnType("varchar(50)")
                        .HasDefaultValueSql("((1))")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("TimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("ToDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("money");

                    b.Property<string>("UserId")
                        .HasColumnName("UserID")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450)
                        .IsUnicode(false);

                    b.HasKey("ReservationId")
                        .HasName("PK_ReservationHistories");

                    b.HasIndex("PaymentStatusId");

                    b.HasIndex("PropertyId");

                    b.HasIndex("ReservationStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("ReservationHistory");
                });

            modelBuilder.Entity("HostelWebAPI.Models.ReservationStatus", b =>
                {
                    b.Property<string>("ReservationStatusId")
                        .HasColumnName("ReservationStatusID")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ReservationStatusId");

                    b.ToTable("ReservationStatus");
                });

            modelBuilder.Entity("HostelWebAPI.Models.Review", b =>
                {
                    b.Property<string>("ReviewId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PropId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ReservationId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ReviewComment")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int>("Star")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReviewId");

                    b.HasIndex("PropId");

                    b.HasIndex("UserId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("HostelWebAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("HostelWebAPI.Models.UserPropertyLike", b =>
                {
                    b.Property<string>("UserPropertyId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PropertyId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.HasKey("UserPropertyId");

                    b.HasIndex("UserId");

                    b.HasIndex("PropertyId", "UserId")
                        .IsUnique()
                        .HasFilter("[PropertyId] IS NOT NULL AND [UserId] IS NOT NULL");

                    b.ToTable("UserPropertyLikes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HostelWebAPI.Models.Owner", b =>
                {
                    b.HasBaseType("HostelWebAPI.Models.User");

                    b.Property<string>("PassportNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Owner");
                });

            modelBuilder.Entity("HostelWebAPI.Models.City", b =>
                {
                    b.HasOne("HostelWebAPI.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_Cities_Countries")
                        .IsRequired();
                });

            modelBuilder.Entity("HostelWebAPI.Models.Comment", b =>
                {
                    b.HasOne("HostelWebAPI.Models.Property", "Property")
                        .WithMany("Comments")
                        .HasForeignKey("PropertyId")
                        .HasConstraintName("FK_Comments_Properties")
                        .IsRequired();

                    b.HasOne("HostelWebAPI.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HostelWebAPI.Models.Image", b =>
                {
                    b.HasOne("HostelWebAPI.Models.Property", "Property")
                        .WithMany("Images")
                        .HasForeignKey("PropertyId")
                        .HasConstraintName("FK_Images_Properties")
                        .IsRequired();
                });

            modelBuilder.Entity("HostelWebAPI.Models.Property", b =>
                {
                    b.HasOne("HostelWebAPI.Models.Owner", "Owner")
                        .WithMany("Properties")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HostelWebAPI.Models.PropertyType", "PropertyType")
                        .WithMany("Properties")
                        .HasForeignKey("PropertyTypeId")
                        .HasConstraintName("FK_Properties_PropertyTypes")
                        .IsRequired();
                });

            modelBuilder.Entity("HostelWebAPI.Models.PropertyAddress", b =>
                {
                    b.HasOne("HostelWebAPI.Models.City", "City")
                        .WithMany("PropertyAddresses")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK_RoomAddresses_Cities")
                        .IsRequired();

                    b.HasOne("HostelWebAPI.Models.Property", "Property")
                        .WithOne("PropertyAddress")
                        .HasForeignKey("HostelWebAPI.Models.PropertyAddress", "PropertyId")
                        .HasConstraintName("FK_RoomAddresses_Properties")
                        .IsRequired();
                });

            modelBuilder.Entity("HostelWebAPI.Models.PropertyService", b =>
                {
                    b.HasOne("HostelWebAPI.Models.Property", "Property")
                        .WithOne("PropertyService")
                        .HasForeignKey("HostelWebAPI.Models.PropertyService", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HostelWebAPI.Models.Reply", b =>
                {
                    b.HasOne("HostelWebAPI.Models.Comment", "Comment")
                        .WithOne("ReplyComment")
                        .HasForeignKey("HostelWebAPI.Models.Reply", "CommentId")
                        .HasConstraintName("FK_Replies_Comments")
                        .IsRequired();

                    b.HasOne("HostelWebAPI.Models.Comment", "ReplyToComment")
                        .WithMany("ReplyReplyToComments")
                        .HasForeignKey("ReplyToCommentId")
                        .HasConstraintName("FK_Replies_Comments1")
                        .IsRequired();
                });

            modelBuilder.Entity("HostelWebAPI.Models.ReservationHistory", b =>
                {
                    b.HasOne("HostelWebAPI.Models.PaymentStatus", "PaymentStatus")
                        .WithMany("ReservationHistories")
                        .HasForeignKey("PaymentStatusId")
                        .HasConstraintName("FK_ReservationHistories_PaymentStatus")
                        .IsRequired();

                    b.HasOne("HostelWebAPI.Models.Property", "Property")
                        .WithMany("ReservationHistories")
                        .HasForeignKey("PropertyId")
                        .HasConstraintName("FK_ReservationHistories_Properties");

                    b.HasOne("HostelWebAPI.Models.ReservationStatus", "ReservationStatus")
                        .WithMany("ReservationHistories")
                        .HasForeignKey("ReservationStatusId")
                        .HasConstraintName("FK_ReservationHistories_ReservationStatus")
                        .IsRequired();

                    b.HasOne("HostelWebAPI.Models.User", "User")
                        .WithMany("ReservationHistories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HostelWebAPI.Models.Review", b =>
                {
                    b.HasOne("HostelWebAPI.Models.Property", "Property")
                        .WithMany("Reviews")
                        .HasForeignKey("PropId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HostelWebAPI.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HostelWebAPI.Models.UserPropertyLike", b =>
                {
                    b.HasOne("HostelWebAPI.Models.Property", "Property")
                        .WithMany("UserPropertyLikes")
                        .HasForeignKey("PropertyId");

                    b.HasOne("HostelWebAPI.Models.User", "User")
                        .WithMany("UserPropertyLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HostelWebAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HostelWebAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HostelWebAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HostelWebAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}