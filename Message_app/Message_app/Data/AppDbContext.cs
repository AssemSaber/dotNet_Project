using System;
using System.Collections.Generic;
using Message_app.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Message_app.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server =ASSEM ; Database =message_service; Integrated Security = SSPI ; TrustServerCertificate = True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__messages__0BBC6ACE8126CF73");

            entity.ToTable("messages");

            entity.Property(e => e.MessageId).HasColumnName("message_Id");
            entity.Property(e => e.DateMessge)
                .HasColumnType("datetime")
                .HasColumnName("date_messge");
            entity.Property(e => e.LandlordId).HasColumnName("landlord_Id");
            entity.Property(e => e.message)
                .HasMaxLength(255)
                .HasColumnName("Message");
            entity.Property(e => e.TenantId).HasColumnName("tenant_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
