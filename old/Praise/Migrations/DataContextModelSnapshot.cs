﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Praise.Contexts;

namespace Praise.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Praise.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EndHour")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Local")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Note")
                        .HasColumnType("varchar(1000)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("StartHour")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("events");
                });

            modelBuilder.Entity("Praise.Models.EventMusic", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("MusicId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<ulong>("Play")
                        .HasColumnType("bit");

                    b.HasKey("EventId", "MusicId");

                    b.HasIndex("MusicId");

                    b.ToTable("EventsMusics");
                });

            modelBuilder.Entity("Praise.Models.EventUser", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("EventsUsers");
                });

            modelBuilder.Entity("Praise.Models.Music", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Lirycs")
                        .HasColumnType("text");

                    b.Property<string>("Notation")
                        .HasColumnType("text");

                    b.Property<ulong>("Play")
                        .HasColumnType("bit");

                    b.Property<string>("Reminder")
                        .HasColumnType("varchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Singer")
                        .HasColumnType("varchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Title")
                        .HasColumnType("varchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Video")
                        .HasColumnType("varchar(2048)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("musics");
                });

            modelBuilder.Entity("Praise.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("Disabled")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastLogon")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .HasColumnType("char(32)")
                        .IsFixedLength(true);

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(14)")
                        .HasMaxLength(14);

                    b.Property<string>("Photo")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Username")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("Username");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Praise.Models.EventMusic", b =>
                {
                    b.HasOne("Praise.Models.Event", "Event")
                        .WithMany("EventMusics")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Praise.Models.Music", "Music")
                        .WithMany("EventMusics")
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Praise.Models.EventUser", b =>
                {
                    b.HasOne("Praise.Models.Event", "Event")
                        .WithMany("EventUsers")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Praise.Models.User", "User")
                        .WithMany("EventUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}