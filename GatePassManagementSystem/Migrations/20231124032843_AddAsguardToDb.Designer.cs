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
    [Migration("20231124032843_AddAsguardToDb")]
    partial class AddAsguardToDb
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

                    b.Property<string>("ASguard")
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

                    b.Property<bool>("ChkifDeptHeadUn")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepId")
                        .HasColumnType("int");

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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PersonalGPId");

                    b.HasIndex("DepId");

                    b.HasIndex("UserId");

                    b.ToTable("PersonalGP");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartId")
                        .HasColumnType("int");

                    b.Property<string>("Designation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EPFNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NICNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RFIDNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartId");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRole");
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

                    b.Property<int>("DepId")
                        .HasColumnType("int");

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

                    b.Property<int>("WrkId")
                        .HasColumnType("int");

                    b.HasKey("WorkerGPId");

                    b.HasIndex("DepId");

                    b.HasIndex("WrkId");

                    b.ToTable("WorkerGP");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.Workers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("Designation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EPFNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeptId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.PersonalGP", b =>
                {
                    b.HasOne("GatePassManagementSystem.Model.Department", "Department")
                        .WithMany("PersonalGP")
                        .HasForeignKey("DepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GatePassManagementSystem.Model.User", "User")
                        .WithMany("PersonalGP")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.User", b =>
                {
                    b.HasOne("GatePassManagementSystem.Model.Department", "Department")
                        .WithMany("User")
                        .HasForeignKey("DepartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GatePassManagementSystem.Model.UserRole", "UserRole")
                        .WithMany("User")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.WorkerGP", b =>
                {
                    b.HasOne("GatePassManagementSystem.Model.Department", "Department")
                        .WithMany("WorkerGP")
                        .HasForeignKey("DepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GatePassManagementSystem.Model.Workers", "Workers")
                        .WithMany("WorkerGP")
                        .HasForeignKey("WrkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Workers");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.Workers", b =>
                {
                    b.HasOne("GatePassManagementSystem.Model.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.Department", b =>
                {
                    b.Navigation("PersonalGP");

                    b.Navigation("User");

                    b.Navigation("WorkerGP");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.User", b =>
                {
                    b.Navigation("PersonalGP");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.UserRole", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.Workers", b =>
                {
                    b.Navigation("WorkerGP");
                });
#pragma warning restore 612, 618
        }
    }
}
