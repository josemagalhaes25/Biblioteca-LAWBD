﻿// <auto-generated />
using System;
using LAWBD_fase3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LAWBD_fase3.Migrations
{
    [DbContext(typeof(BibliotecaContext))]
    [Migration("20241120221819_AddNomeToLeitor")]
    partial class AddNomeToLeitor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LAWBD_fase3.Models.Administrador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Biblioteca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bibliotecas");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Bibliotecario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BibliotecaId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BibliotecaId");

                    b.ToTable("Bibliotecarios");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Emprestimo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataDevolucao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataEmprestimo")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeitorId")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeitorId");

                    b.HasIndex("LivroId");

                    b.ToTable("Emprestimos");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Leitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Leitores");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BibliotecaId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BibliotecaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Bibliotecario", b =>
                {
                    b.HasOne("LAWBD_fase3.Models.Biblioteca", "Biblioteca")
                        .WithMany("Bibliotecarios")
                        .HasForeignKey("BibliotecaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Biblioteca");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Emprestimo", b =>
                {
                    b.HasOne("LAWBD_fase3.Models.Leitor", "Leitor")
                        .WithMany("Emprestimos")
                        .HasForeignKey("LeitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAWBD_fase3.Models.Livro", "Livro")
                        .WithMany("Emprestimos")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Leitor");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Livro", b =>
                {
                    b.HasOne("LAWBD_fase3.Models.Biblioteca", "Biblioteca")
                        .WithMany("Livros")
                        .HasForeignKey("BibliotecaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAWBD_fase3.Models.Categoria", "Categoria")
                        .WithMany("Livros")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Biblioteca");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Biblioteca", b =>
                {
                    b.Navigation("Bibliotecarios");

                    b.Navigation("Livros");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Categoria", b =>
                {
                    b.Navigation("Livros");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Leitor", b =>
                {
                    b.Navigation("Emprestimos");
                });

            modelBuilder.Entity("LAWBD_fase3.Models.Livro", b =>
                {
                    b.Navigation("Emprestimos");
                });
#pragma warning restore 612, 618
        }
    }
}
