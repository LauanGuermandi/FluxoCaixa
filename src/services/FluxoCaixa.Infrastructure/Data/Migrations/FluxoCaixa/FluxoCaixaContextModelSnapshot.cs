﻿// <auto-generated />
using System;
using FluxoCaixa.Infrastructure.Data.Context.FluxoCaixa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FluxoCaixa.Infrastructure.Data.Migrations.FluxoCaixa
{
    [DbContext(typeof(FluxoCaixaContext))]
    partial class FluxoCaixaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Caixa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<decimal>("Saldo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValue(0m)
                        .HasColumnName("SALDO");

                    b.HasKey("Id")
                        .HasName("PK_CAIXAS");

                    b.ToTable("CAIXAS", (string)null);
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Lancamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<DateTime>("DataLancamento")
                        .HasColumnType("date")
                        .HasColumnName("DATALANCAMENTO");

                    b.Property<TimeSpan>("HoraLancamento")
                        .HasColumnType("time")
                        .HasColumnName("HORALANCAMENTO");

                    b.Property<Guid>("IdCaixa")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IDCAIXA");

                    b.Property<byte>("TipoLancamento")
                        .HasColumnType("tinyint")
                        .HasColumnName("TIPOLANCAMENTO");

                    b.Property<decimal>("Valor")
                        .HasColumnType("money")
                        .HasColumnName("VALOR");

                    b.HasKey("Id")
                        .HasName("PK_LANCAMENTOS");

                    b.HasIndex("DataLancamento");

                    b.HasIndex("IdCaixa");

                    b.HasIndex("TipoLancamento");

                    b.ToTable("LANCAMENTOS", (string)null);
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Loja", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)")
                        .HasColumnName("CNPJ");

                    b.Property<Guid>("IdCaixa")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IDCAIXA");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("NOME");

                    b.HasKey("Id")
                        .HasName("PK_LOJA");

                    b.HasIndex("IdCaixa")
                        .IsUnique();

                    b.ToTable("LOJAS", (string)null);
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.RelatorioAggregation.Relatorio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("STATUS");

                    b.HasKey("Id")
                        .HasName("PK_RELATORIOS");

                    b.ToTable("RELATORIOS", (string)null);
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.RelatorioAggregation.RelatorioMetadados", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<Guid>("IdRelatorio")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IDRELATORIO");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("VALOR");

                    b.HasKey("Id")
                        .HasName("PK_RELATORIOMETADADOS");

                    b.HasIndex("IdRelatorio")
                        .IsUnique();

                    b.ToTable("RELATORIO_METADADOS", (string)null);
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.UsuarioAggregation.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("EMAIL");

                    b.Property<Guid>("IdLoja")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IDLOJA");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("NOME");

                    b.HasKey("Id")
                        .HasName("PK_USUARIOS");

                    b.HasIndex("IdLoja");

                    b.ToTable("USUARIOS", (string)null);
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Lancamento", b =>
                {
                    b.HasOne("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Caixa", "Caixa")
                        .WithMany("Lancamentos")
                        .HasForeignKey("IdCaixa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_LANCAMENTOS_CAIXAS_CAIXAID");

                    b.Navigation("Caixa");
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Loja", b =>
                {
                    b.HasOne("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Caixa", "Caixa")
                        .WithOne("Loja")
                        .HasForeignKey("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Loja", "IdCaixa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CAIXAS_LOJA_LOJAID");

                    b.Navigation("Caixa");
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.RelatorioAggregation.RelatorioMetadados", b =>
                {
                    b.HasOne("FluxoCaixa.Domain.Aggregates.RelatorioAggregation.Relatorio", "Relatorio")
                        .WithOne("Metadados")
                        .HasForeignKey("FluxoCaixa.Domain.Aggregates.RelatorioAggregation.RelatorioMetadados", "IdRelatorio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_RELATORIOS_RELATORIOMETADADOS_METADADOSID");

                    b.Navigation("Relatorio");
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.UsuarioAggregation.Usuario", b =>
                {
                    b.HasOne("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Loja", "Loja")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdLoja")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_USUARIOS_LOJA_LOJAID");

                    b.Navigation("Loja");
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Caixa", b =>
                {
                    b.Navigation("Lancamentos");

                    b.Navigation("Loja");
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.CaixaAggregation.Loja", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("FluxoCaixa.Domain.Aggregates.RelatorioAggregation.Relatorio", b =>
                {
                    b.Navigation("Metadados");
                });
#pragma warning restore 612, 618
        }
    }
}
