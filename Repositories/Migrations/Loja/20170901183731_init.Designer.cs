﻿// <auto-generated />
using Infra.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Infra.Repositories.Migrations.Loja
{
    [DbContext(typeof(LojaContext))]
    [Migration("20170901183731_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Domain.Models.ItensPedido", b =>
                {
                    b.Property<int>("PedidoId");

                    b.Property<int>("ProdutoId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("Quantidade");

                    b.HasKey("PedidoId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ItemPedido");
                });

            modelBuilder.Entity("Domain.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClienteId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("Domain.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Nome");

                    b.Property<decimal>("Preco");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Domain.Models.ItensPedido", b =>
                {
                    b.HasOne("Domain.Models.Pedido")
                        .WithMany("Itens")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Models.Pedido", b =>
                {
                    b.HasOne("Domain.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");
                });
#pragma warning restore 612, 618
        }
    }
}