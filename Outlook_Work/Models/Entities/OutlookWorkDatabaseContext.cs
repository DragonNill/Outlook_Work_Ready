using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Outlook_Work.Models.Entities
{
    public partial class OutlookWorkDatabaseContext : DbContext
    {
        public static OutlookWorkDatabaseContext DbContext { get; private set; }

        static OutlookWorkDatabaseContext() => DbContext = new OutlookWorkDatabaseContext();

        public OutlookWorkDatabaseContext()
        {
        }

        public OutlookWorkDatabaseContext(DbContextOptions<OutlookWorkDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user=root;password=12345;database=outlook_work_db", x => x.ServerVersion("8.0.30-mysql")).UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Idfeedback)
                    .HasName("PRIMARY");

                entity.ToTable("feedback");

                entity.HasIndex(e => e.CodeCompanyEmployee)
                    .HasName("Fk_FeedBack_User_idx");

                entity.HasIndex(e => e.CodeOrder)
                    .HasName("FK_FeedBack_Order_idx");

                entity.Property(e => e.Idfeedback).HasColumnName("idfeedback");

                entity.Property(e => e.FeedbackContent)
                    .IsRequired()
                    .HasColumnName("feedbackContent")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.CodeCompanyEmployeeNavigation)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.CodeCompanyEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_FeedBack_User");

                entity.HasOne(d => d.CodeOrderNavigation)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.CodeOrder)
                    .HasConstraintName("FK_FeedBack_Order");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PRIMARY");

                entity.ToTable("order");

                entity.HasIndex(e => e.CodeEmployeer)
                    .HasName("FK_Order_User1_idx");

                entity.HasIndex(e => e.CodeHeadDepartament)
                    .HasName("FK_Order_User_idx");

                entity.HasIndex(e => e.CodeStatus)
                    .HasName("FK_Order_Status_idx");

                entity.Property(e => e.IdOrder).HasColumnName("idOrder");

                entity.Property(e => e.OrderContent)
                    .IsRequired()
                    .HasColumnName("orderContent")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("orderDate")
                    .HasColumnType("date");

                entity.Property(e => e.OrderName)
                    .IsRequired()
                    .HasColumnName("orderName")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.CodeEmployeerNavigation)
                    .WithMany(p => p.OrderCodeEmployeerNavigation)
                    .HasForeignKey(d => d.CodeEmployeer)
                    .HasConstraintName("FK_Order_User1");

                entity.HasOne(d => d.CodeHeadDepartamentNavigation)
                    .WithMany(p => p.OrderCodeHeadDepartamentNavigation)
                    .HasForeignKey(d => d.CodeHeadDepartament)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Order_User");

                entity.HasOne(d => d.CodeStatusNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CodeStatus)
                    .HasConstraintName("FK_Order_Status");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.Idreport)
                    .HasName("PRIMARY");

                entity.ToTable("report");

                entity.HasIndex(e => e.CodeHeadDepartament)
                    .HasName("FK_Report_User_idx");

                entity.HasIndex(e => e.CodeOrder)
                    .HasName("FK_Report_Order_idx");

                entity.Property(e => e.Idreport).HasColumnName("idreport");

                entity.Property(e => e.ReportContent)
                    .IsRequired()
                    .HasColumnName("reportContent")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.CodeHeadDepartamentNavigation)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.CodeHeadDepartament)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_User");

                entity.HasOne(d => d.CodeOrderNavigation)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.CodeOrder)
                    .HasConstraintName("FK_Report_Order");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PRIMARY");

                entity.ToTable("roles");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Idstatus)
                    .HasName("PRIMARY");

                entity.ToTable("status");

                entity.Property(e => e.Idstatus).HasColumnName("idstatus");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasColumnName("statusName")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.HasIndex(e => e.UserCodeRole)
                    .HasName("FK_User_Role_idx");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.UserCodeRole).HasColumnName("userCodeRole");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("userEmail")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasColumnName("userLogin")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("userPassword")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserPatronomyc)
                    .HasColumnName("userPatronomyc")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserSurname)
                    .IsRequired()
                    .HasColumnName("userSurname")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserTelephone)
                    .IsRequired()
                    .HasColumnName("userTelephone")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.UserCodeRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserCodeRole)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
