﻿// <auto-generated />
using System;
using KBR.Domain.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KBR.Domain.Infra.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20220422130216_release")]
    partial class release
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KBR.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("complemento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("addresses");
                });

            modelBuilder.Entity("KBR.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("paid")
                        .HasColumnType("bit");

                    b.Property<Guid?>("statusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("statusId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("KBR.Domain.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<Guid>("productId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("qtd")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("orderItems");
                });

            modelBuilder.Entity("KBR.Domain.Entities.OrderStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("order")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("orderStatus");
                });

            modelBuilder.Entity("KBR.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("addressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("code")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("confirmationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("orderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("total")
                        .HasColumnType("float");

                    b.Property<double>("value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("addressId");

                    b.HasIndex("orderId");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("KBR.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<Guid?>("typeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("typeId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("KBR.Domain.Entities.ProductType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("productTypes");
                });

            modelBuilder.Entity("KBR.Domain.Entities.Order", b =>
                {
                    b.HasOne("KBR.Domain.Entities.OrderStatus", "status")
                        .WithMany()
                        .HasForeignKey("statusId");

                    b.Navigation("status");
                });

            modelBuilder.Entity("KBR.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("KBR.Domain.Entities.Order", null)
                        .WithMany("items")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("KBR.Domain.Entities.Payment", b =>
                {
                    b.HasOne("KBR.Domain.Entities.Address", "address")
                        .WithMany()
                        .HasForeignKey("addressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KBR.Domain.Entities.Order", "order")
                        .WithMany()
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("address");

                    b.Navigation("order");
                });

            modelBuilder.Entity("KBR.Domain.Entities.Product", b =>
                {
                    b.HasOne("KBR.Domain.Entities.ProductType", "type")
                        .WithMany()
                        .HasForeignKey("typeId");

                    b.Navigation("type");
                });

            modelBuilder.Entity("KBR.Domain.Entities.Order", b =>
                {
                    b.Navigation("items");
                });
#pragma warning restore 612, 618
        }
    }
}