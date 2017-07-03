using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NGC.DAL.Base;
using NGC.Model;

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

                    b.Property<int>("ChildrenCount");

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<string>("Email");

                    b.Property<bool>("Gender");

                    b.Property<int>("IdTemplate");

                    b.Property<DateTime?>("LastSent")
                        .HasColumnName("last_sent")
                        .HasColumnType("datetime");

                    b.Property<int>("MaritalState")
                        .HasColumnType("int");

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

                    b.HasIndex("IdTemplate");

                    b.ToTable("Customer");

                    b.HasAnnotation("MySql:TableName", "customer");
                });

            modelBuilder.Entity("NGC.Model.EmailTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Subject")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Template")
                        .HasColumnName("template")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmailTemplate");

                    b.HasAnnotation("MySql:TableName", "emailtemplate");
                });

            modelBuilder.Entity("NGC.Model.MerakiTextImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Bytes");

                    b.Property<string>("FontName");

                    b.Property<double>("Height");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Text")
                        .HasColumnType("varchar(100)");

                    b.Property<double>("Width");

                    b.Property<double>("X");

                    b.Property<double>("Y");

                    b.HasKey("Id");

                    b.ToTable("MerakiTextImage");

                    b.HasAnnotation("MySql:TableName", "meraki_text_image");
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
                        .HasColumnType("varchar(176)")
                        .HasMaxLength(176);

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(16)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasAnnotation("MySql:TableName", "user");
                });

            modelBuilder.Entity("NGC.Model.Customer", b =>
                {
                    b.HasOne("NGC.Model.EmailTemplate", "Template")
                        .WithMany("Customers")
                        .HasForeignKey("IdTemplate");
                });
        }
    }
}
