using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NGC.DAL.Base;

namespace NGC.DAL.Migrations
{
    [DbContext(typeof(MerakiContext))]
    partial class MerakiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("NGC.Model.Configuration", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("key")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Value")
                        .HasColumnName("value")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("Key");

                    b.ToTable("Configuration");

                    b.HasAnnotation("MySql:TableName", "configuration");
                });

            modelBuilder.Entity("NGC.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<string>("Email");

                    b.Property<DateTime?>("LastSent")
                        .HasColumnName("last_sent")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Surname1")
                        .HasColumnName("surname_1")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Surname2")
                        .HasColumnName("surname_2")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Customer");

                    b.HasAnnotation("MySql:TableName", "customer");
                });

            modelBuilder.Entity("NGC.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("varchar(132)")
                        .HasMaxLength(132);

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasAnnotation("MySql:TableName", "user");
                });
        }
    }
}
