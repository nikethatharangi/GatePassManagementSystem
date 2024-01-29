﻿// <auto-generated />
using System;
using GatePassManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GatePassManagementSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231030060109_AddDeptToDb")]
    partial class AddDeptToDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GatePassManagementSystem.Model.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeptName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.PersonalGP", b =>
                {
                    b.Property<string>("PersonalGPId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ASdgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ASgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AShod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ASmd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ChkHalfd")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkLunch")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkMadu")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkOther")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkShrt")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkSinthawatta")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonalGPId");

                    b.ToTable("PersonalGP");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.WorkerGP", b =>
                {
                    b.Property<string>("WorkerGPId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ASdgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ASgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AShod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ASmd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ChkHalfd")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkLunch")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkMadu")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkOther")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkShrt")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkSinthawatta")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkerGPId");

                    b.ToTable("WorkerGP");
                });
#pragma warning restore 612, 618
        }
    }
}