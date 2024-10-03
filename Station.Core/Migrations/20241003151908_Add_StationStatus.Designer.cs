﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Station.Core;

#nullable disable

namespace Station.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241003151908_Add_StationStatus")]
    partial class Add_StationStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Station.Core.Entities.ChargeStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("ChargeStations");
                });

            modelBuilder.Entity("Station.Core.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("Station.Core.Entities.StationEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChargeStationId")
                        .HasColumnType("int");

                    b.Property<int>("NewData")
                        .HasColumnType("int");

                    b.Property<int>("StationStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TimeStampe")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChargeStationId");

                    b.HasIndex("StationStatusId");

                    b.HasIndex("TypeId");

                    b.ToTable("StationEvents");
                });

            modelBuilder.Entity("Station.Core.Entities.StationEventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EnumValue")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("StationEventTypes");
                });

            modelBuilder.Entity("Station.Core.Entities.StationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EnumValue")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("StationStatuses");
                });

            modelBuilder.Entity("Station.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HashPasword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Station.Core.Entities.ChargeStation", b =>
                {
                    b.HasOne("Station.Core.Entities.Owner", "Owner")
                        .WithMany("ChargeStations")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Station.Core.Entities.StationEvent", b =>
                {
                    b.HasOne("Station.Core.Entities.ChargeStation", "ChargeStation")
                        .WithMany("StationEvents")
                        .HasForeignKey("ChargeStationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Station.Core.Entities.StationStatus", "StationStatus")
                        .WithMany()
                        .HasForeignKey("StationStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Station.Core.Entities.StationEventType", "EventType")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargeStation");

                    b.Navigation("EventType");

                    b.Navigation("StationStatus");
                });

            modelBuilder.Entity("Station.Core.Entities.ChargeStation", b =>
                {
                    b.Navigation("StationEvents");
                });

            modelBuilder.Entity("Station.Core.Entities.Owner", b =>
                {
                    b.Navigation("ChargeStations");
                });
#pragma warning restore 612, 618
        }
    }
}
