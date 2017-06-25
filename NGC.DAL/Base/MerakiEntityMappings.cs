﻿using Microsoft.EntityFrameworkCore;
using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.DAL.Base
{
    public static class MerakiEntityMappings
    {
        public static void MapUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>((build)=> {
                build.ForMySqlToTable("user");

                build.Property(u => u.Id).HasColumnName("id").HasColumnType("int");
                build.Property(u => u.Login).HasColumnType("login").HasColumnType("varchar(40)").HasMaxLength(40);
                build.Property(u => u.Password).HasColumnName("password").HasColumnType("varchar(132)").HasMaxLength(132);

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
    }
}
