using Microsoft.EntityFrameworkCore;
using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.DAL.Base
{
    public static class MerakiEntityMappings
    {
        public static void MapMerakiImage(ModelBuilder builder)
        {
            builder.Entity<MerakiTextImage>(b => {
                b.ForMySqlToTable("meraki_text_image");

                b.HasKey(i => i.Id);
                b.Property(i => i.Name).HasColumnType("varchar(100)");
                b.Property(i => i.Height);
                b.Property(i => i.FontName);
                b.Property(i=>i.Text).HasColumnType("varchar(100)");
                b.Property(i => i.Width);
                b.Property(i => i.X);
                b.Property(i => i.Y);


            });
        }
        public static void MapUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>((build)=> {
                build.ForMySqlToTable("user");

                build.Property(u => u.Id).HasColumnName("id").HasColumnType("int");
                build.Property(u => u.Login).HasColumnType("login").HasColumnType("varchar(40)").HasMaxLength(40);
                build.Property(u => u.Password).HasColumnName("password").HasColumnType("varchar(176)").HasMaxLength(176);
                build.Property(u => u.Salt).HasColumnType("varbinary(16)");
            });
        }
        public static void MapCustomer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(config=> {
                config.ForMySqlToTable("customer");

                config.Property(u => u.Id).HasColumnName("id");
                config.Property(u => u.Name).HasColumnName("name").HasColumnType("varchar(40)").HasMaxLength(40);
                config.Property(u => u.Surname1).HasColumnName("surname_1").HasColumnType("varchar(100)").HasMaxLength(100);
                config.Property(u => u.Surname2).HasColumnName("surname_2").HasColumnType("varchar(100)").HasMaxLength(100);
                config.Property(u => u.Date).HasColumnName("date").HasColumnType("datetime");
                config.Property(u => u.LastSent).HasColumnName("last_sent").HasColumnType("datetime");
                config.Property(u => u.ChildrenCount);
                config.Property(u => u.IdTemplate);
                config.Property(u => u.Gender);
                config.Property(u => u.MaritalState).HasColumnType("int");
                
            });
        }
        public static void MapConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Configuration>(config => {
                config.ForMySqlToTable("configuration");
                config.HasKey(c => c.Key);

                config.Property(u => u.Key).HasColumnName("key").HasColumnType("varchar(40)").HasMaxLength(40);
                config.Property(u => u.Value).HasColumnName("value").HasColumnType("varchar(40)").HasMaxLength(40);
            });
        }
        public static void MapEmailTemplate(ModelBuilder builder)
        {
            builder.Entity<EmailTemplate>(cfg=> {
                cfg.ForMySqlToTable("emailtemplate");
                cfg.HasKey(e => e.Id);


                cfg.Property(e => e.Id).HasColumnName("id");
                cfg.Property(e => e.Name).HasColumnName("name").HasColumnType("varchar(40)");
                cfg.Property(e => e.Subject).HasColumnType("subj").HasColumnType("varchar(255)");
                cfg.Property(e => e.Template).HasColumnName("template").HasColumnType("text");

                cfg.HasMany(e => e.Customers).WithOne(c => c.Template).HasForeignKey(c => c.IdTemplate).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            });
        }
    }
}
