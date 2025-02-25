﻿using Microsoft.EntityFrameworkCore;
using WebApi.Testing;

namespace WebApi.Data
{
    public partial class MainDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions<MainDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<TblComment> TblComments { get; set; }

        public virtual DbSet<TblPost> TblPosts { get; set; }

        public virtual DbSet<TblUser> TblUsers { get; set; }

        public virtual DbSet<TblFormat> TblFormats { get; set; }

        public virtual DbSet<TblPolicy> TblPolicies { get; set; }

        public virtual DbSet<TblFileMetadata> TblFileMetadata { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("WebConnectionString"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblComment>(entity =>
            {
                entity.HasKey(e => e.CommentId).HasName("PK__Tbl_comm__E7957687DF511CFC");

                entity.ToTable("Tbl_comments");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.PostId).HasColumnName("post_id");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Post).WithMany(p => p.TblComments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tbl_comme__post___440B1D61");

                entity.HasOne(d => d.User).WithMany(p => p.TblComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tbl_comme__user___44FF419A");
            });

            modelBuilder.Entity<TblPost>(entity =>
            {
                entity.HasKey(e => e.PostId).HasName("PK__Tbl_post__3ED78766EA962577");

                entity.ToTable("Tbl_posts");

                entity.Property(e => e.PostId).HasColumnName("post_id");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("image_url");
                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User).WithMany(p => p.TblPosts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tbl_posts__user___3F466844");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Tbl_user__B9BE370F7AC7EA11");

                entity.ToTable("Tbl_user");

                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");
                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .HasColumnName("role");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<TblFormat>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Tbl_form__3213E83F19E7F4C8");

                entity.ToTable("Tbl_format");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.CurrentValue).HasColumnName("current_value");
                entity.Property(e => e.FormatName)
                    .HasMaxLength(50)
                    .HasColumnName("format_name");
                entity.Property(e => e.FormattingAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("formatting_at");
                entity.Property(e => e.Length).HasColumnName("length");
            });

            modelBuilder.Entity<TblPolicy>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Tbl_poli__3213E83F09C37CD1");

                entity.ToTable("Tbl_policy");

                entity.HasIndex(e => e.PolicyId, "UQ_policy_id").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.PolicyEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("policy_end_date");
                entity.Property(e => e.PolicyHolderName)
                    .HasMaxLength(100)
                    .HasColumnName("policy_holder_name");
                entity.Property(e => e.PolicyId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("policy_id");
                entity.Property(e => e.PolicyStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("policy_start_date");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<TblFileMetadata>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Tbl_File__3213E83F302146C2");

                entity.ToTable("Tbl_fileMetadata");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FileName)
                    .HasMaxLength(255)
                    .HasColumnName("file_name");
                entity.Property(e => e.FilePath)
                    .HasMaxLength(255)
                    .HasColumnName("file_path");
                entity.Property(e => e.PolicyId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("policy_id");
                entity.Property(e => e.UploadDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("upload_date");

                entity.HasOne(d => d.Policy).WithMany(p => p.TblFileMetadata)
                    .HasPrincipalKey(p => p.PolicyId)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK__Tbl_FileM__polic__1AD3FDA4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
