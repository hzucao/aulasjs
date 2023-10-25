﻿// <auto-generated />
using InduMovel.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InduMovel.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230910192046_inicial")]
    partial class inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("InduMovel.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CategoriaID");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("InduMovel.Models.Movel", b =>
                {
                    b.Property<int>("MovelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmProducao")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagemCurta")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagemUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Promocao")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Valor")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("MovelId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Moveis");
                });

            modelBuilder.Entity("InduMovel.Models.Movel", b =>
                {
                    b.HasOne("InduMovel.Models.Categoria", "Categoria")
                        .WithMany("Moveis")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("InduMovel.Models.Categoria", b =>
                {
                    b.Navigation("Moveis");
                });
#pragma warning restore 612, 618
        }
    }
}
