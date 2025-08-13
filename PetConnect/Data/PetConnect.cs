using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PetConnect.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace PetConnect.Data;

public partial class PetConnect : DbContext
{
    public PetConnect()
    {
    }

    public PetConnect(DbContextOptions<PetConnect> options)
        : base(options)
    {
    }

    public virtual DbSet<Operador> Operadores { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Tutor> Tutores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=PetConnect;user=root;password=FerAmen123456;sslmode=None", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.4.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Operador>(entity =>
        {
            entity.HasKey(e => e.IdOperador).HasName("PRIMARY");

            entity.ToTable("operadores");

            entity.HasIndex(e => e.CpfTutor, "IX_Operadores_cpfTutor");

            entity.HasIndex(e => e.CnpjOperador, "UQ_Operadores_CNPJ").IsUnique();

            entity.HasIndex(e => e.CpfOperador, "UQ_Operadores_CPF").IsUnique();

            entity.HasIndex(e => e.EmailOperador, "UQ_Operadores_Email").IsUnique();

            entity.Property(e => e.IdOperador).HasColumnName("idOperador");
            entity.Property(e => e.CnpjOperador)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("cnpjOperador");
            entity.Property(e => e.CpfOperador)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpfOperador");
            entity.Property(e => e.CpfTutor)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpfTutor");
            entity.Property(e => e.EmailOperador)
                .HasMaxLength(200)
                .HasColumnName("emailOperador");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.TelefoneOperador)
                .HasMaxLength(20)
                .HasColumnName("telefoneOperador");

            entity.HasOne(d => d.CpfTutorNavigation).WithMany(p => p.Operadores)
                .HasPrincipalKey(p => p.CpfTutor)
                .HasForeignKey(d => d.CpfTutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operadores_Tutores");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.IdPet).HasName("PRIMARY");

            entity.ToTable("pets");

            entity.HasIndex(e => e.Cpf, "IX_Pets_CPF");

            entity.Property(e => e.IdPet).HasColumnName("idPet");
            entity.Property(e => e.Cor)
                .HasMaxLength(40)
                .HasColumnName("cor");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("CPF");
            entity.Property(e => e.DataDeNascimento).HasColumnName("dataDeNascimento");
            entity.Property(e => e.Especie)
                .HasMaxLength(60)
                .HasColumnName("especie");
            entity.Property(e => e.Nome)
                .HasMaxLength(120)
                .HasColumnName("nome");
            entity.Property(e => e.Raca)
                .HasMaxLength(120)
                .HasColumnName("raca");

            entity.HasOne(d => d.CpfNavigation).WithMany(p => p.Pets)
                .HasPrincipalKey(p => p.CpfTutor)
                .HasForeignKey(d => d.Cpf)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pets_Tutores_FK");
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.IdTutor).HasName("PRIMARY");

            entity.ToTable("tutores");

            entity.HasIndex(e => e.EmailTutor, "IX_Tutores_Email");

            entity.HasIndex(e => e.CpfTutor, "UQ_Tutores_Cpf").IsUnique();

            entity.Property(e => e.IdTutor).HasColumnName("idTutor");
            entity.Property(e => e.CpfTutor)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpfTutor");
            entity.Property(e => e.DataDeNascimentoTutor).HasColumnName("dataDeNascimentoTutor");
            entity.Property(e => e.EmailTutor)
                .HasMaxLength(200)
                .HasColumnName("emailTutor");
            entity.Property(e => e.EnderecoTutor)
                .HasMaxLength(250)
                .HasColumnName("enderecoTutor");
            entity.Property(e => e.NomeTutor)
                .HasMaxLength(150)
                .HasColumnName("nomeTutor");
            entity.Property(e => e.TelefoneTutor)
                .HasMaxLength(20)
                .HasColumnName("telefoneTutor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
