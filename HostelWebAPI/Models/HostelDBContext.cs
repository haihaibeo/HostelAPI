using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class HostelDBContext : DbContext
    {
        public HostelDBContext()
        {
        }

        public HostelDBContext(DbContextOptions<HostelDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<PropertyAddress> PropertyAddress { get; set; }
        public virtual DbSet<PropertyType> PropertyType { get; set; }
        public virtual DbSet<Reply> Reply { get; set; }
        public virtual DbSet<ReservationHistory> ReservationHistory { get; set; }
        public virtual DbSet<ReservationStatus> ReservationStatus { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPropertyLike> UserPropertyLike { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId)
                    .HasColumnName("CityID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_Countries");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId)
                    .HasColumnName("CommentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CommentString)
                    .IsRequired()
                    .HasColumnName("Comment")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(N'Default comment')");

                entity.Property(e => e.PropertyId)
                    .IsRequired()
                    .HasColumnName("PropertyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TimeUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Properties");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Users");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryId)
                    .HasColumnName("CountryID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.ImageId)
                    .HasColumnName("ImageID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Alt).HasMaxLength(100);

                entity.Property(e => e.PropertyId)
                    .IsRequired()
                    .HasColumnName("PropertyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Images_Properties");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_Owners");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassportNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('default passport')");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Owner)
                    .HasForeignKey<Owner>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Owners_Users");
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.Property(e => e.PaymentStatusId)
                    .HasColumnName("PaymentStatusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.PropertyId)
                    .HasColumnName("PropertyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(N'Property Discription')");

                entity.Property(e => e.Introduction)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(N'Property Introduction')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'PropertyName')");

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasColumnName("OwnerID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PricePerNight).HasColumnType("money");

                entity.Property(e => e.PropertyTypeId)
                    .IsRequired()
                    .HasColumnName("PropertyTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Rating).HasDefaultValueSql("((5.0))");

                entity.Property(e => e.TimeCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TimeUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Property)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Properties_Owners");

                entity.HasOne(d => d.PropertyType)
                    .WithMany(p => p.Property)
                    .HasForeignKey(d => d.PropertyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Properties_PropertyTypes");
            });

            modelBuilder.Entity<PropertyAddress>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("PK_RoomAddresses");

                entity.Property(e => e.PropertyId)
                    .HasColumnName("PropertyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CityId)
                    .IsRequired()
                    .HasColumnName("CityID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Number)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Street number default')");

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Street Name default')");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.PropertyAddresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomAddresses_Cities");

                entity.HasOne(d => d.Property)
                    .WithOne(p => p.PropertyAddress)
                    .HasForeignKey<PropertyAddress>(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomAddresses_Properties");
            });

            modelBuilder.Entity<PropertyType>(entity =>
            {
                entity.Property(e => e.PropertyTypeId)
                    .HasColumnName("PropertyTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK_Replies");

                entity.Property(e => e.CommentId)
                    .HasColumnName("CommentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReplyToCommentId)
                    .IsRequired()
                    .HasColumnName("ReplyToCommentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Comment)
                    .WithOne(p => p.ReplyComment)
                    .HasForeignKey<Reply>(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Replies_Comments");

                entity.HasOne(d => d.ReplyToComment)
                    .WithMany(p => p.ReplyReplyToComment)
                    .HasForeignKey(d => d.ReplyToCommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Replies_Comments1");
            });

            modelBuilder.Entity<ReservationHistory>(entity =>
            {
                entity.HasKey(e => e.ReservationId)
                    .HasName("PK_ReservationHistories");

                entity.Property(e => e.ReservationId)
                    .HasColumnName("ReservationID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentStatusId)
                    .IsRequired()
                    .HasColumnName("PaymentStatusID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PropertyId)
                    .IsRequired()
                    .HasColumnName("PropertyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReservationStatusId)
                    .IsRequired()
                    .HasColumnName("ReservationStatusID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TimeCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TotalCost).HasColumnType("money");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.ReservationHistory)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationHistories_PaymentStatus");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.ReservationHistory)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationHistories_Properties");

                entity.HasOne(d => d.ReservationStatus)
                    .WithMany(p => p.ReservationHistory)
                    .HasForeignKey(d => d.ReservationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationHistories_ReservationStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReservationHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationHistories_Users");
            });

            modelBuilder.Entity<ReservationStatus>(entity =>
            {
                entity.Property(e => e.ReservationStatusId)
                    .HasColumnName("ReservationStatusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Default name')");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'default phone')");

                entity.Property(e => e.TimeCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<UserPropertyLike>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PropertyId })
                    .HasName("PK_UserPropertyLikes");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyId)
                    .HasColumnName("PropertyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.UserPropertyLike)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPropertyLikes_Properties");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPropertyLike)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPropertyLikes_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
