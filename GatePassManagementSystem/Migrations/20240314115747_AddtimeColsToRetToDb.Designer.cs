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
    [Migration("20240314115747_AddtimeColsToRetToDb")]
    partial class AddtimeColsToRetToDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GatePassManagementSystem.Model.ApprovalChange", b =>
                {
                    b.Property<int>("ChId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("deptId")
                        .HasColumnType("int");

                    b.HasKey("ChId");

                    b.ToTable("ApprovalChange");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeptName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Md")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TempAprvl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.NonReturnItemDsc", b =>
                {
                    b.Property<int>("NonReturnItemDscId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NonReturnItemDscId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NonGPId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NonReturnItemDscId1")
                        .HasColumnType("int");

                    b.Property<string>("NonReturnableGPId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("NonReturnItemDscId");

                    b.HasIndex("NonReturnItemDscId1");

                    b.HasIndex("NonReturnableGPId");

                    b.ToTable("NonReturnItemDsc");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.NonReturnableGP", b =>
                {
                    b.Property<string>("NonReturnableGPId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ASdgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASdgmtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASgmtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASguard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AShod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AShodtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASmd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASmdtime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AcknoledgedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChApprvlId")
                        .HasColumnType("int");

                    b.Property<bool>("ChkifDeptHeadUn")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepId")
                        .HasColumnType("int");

                    b.Property<string>("DrHelname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FromLocation")
                        .HasColumnType("int");

                    b.Property<string>("HODRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MachineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Other")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RejctReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Satisfy")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SinIntime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SinOuttime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToDept")
                        .HasColumnType("int");

                    b.Property<int>("ToLocation")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NonReturnableGPId");

                    b.HasIndex("DepId");

                    b.HasIndex("UserId");

                    b.ToTable("NonReturnableGP");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.PersonalGP", b =>
                {
                    b.Property<string>("PersonalGPId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ASdgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASdgmtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASgmtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASguard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AShod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AShodtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASmd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASmdtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChApprvlId")
                        .HasColumnType("int");

                    b.Property<bool>("ChkCusVisit")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkHalfd")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkLunch")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkMadu")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkOfficialwork")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkOther")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkPamunugama")
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

                    b.Property<string>("Description")
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

                    b.Property<string>("RejctReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SinIntime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SinOuttime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PersonalGPId");

                    b.HasIndex("DepId");

                    b.HasIndex("UserId");

                    b.ToTable("PersonalGP");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.ReturnItemDsc", b =>
                {
                    b.Property<int>("ReturnItemDscId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OutTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("ReturnableGPId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SinIntime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SinOuttime")
                        .HasColumnType("datetime2");

                    b.HasKey("ReturnItemDscId");

                    b.HasIndex("ReturnableGPId");

                    b.ToTable("ReturnItemDsc");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.ReturnableGP", b =>
                {
                    b.Property<string>("ReturnableGPId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ASAccnt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ASdgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASdgmtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASgmtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASguard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AShod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AShodtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASmd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASmdtime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AcknoledgedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChApprvlId")
                        .HasColumnType("int");

                    b.Property<bool>("ChkifDeptHeadUn")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepId")
                        .HasColumnType("int");

                    b.Property<string>("DrHelname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FromLocation")
                        .HasColumnType("int");

                    b.Property<string>("HODRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("OtherLocation")
                        .HasColumnType("int");

                    b.Property<DateTime>("OutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuotationNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RejctReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepairContNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepairName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReturnPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Satisfy")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SinIntime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SinOuttime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToDept")
                        .HasColumnType("int");

                    b.Property<int>("ToLocation")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReturnableGPId");

                    b.HasIndex("DepId");

                    b.HasIndex("UserId");

                    b.ToTable("ReturnableGP");
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

                    b.Property<DateTime>("ASdgmtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASgm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASgmtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASguard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AShod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AShodtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ASmd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ASmdtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChAprlId")
                        .HasColumnType("int");

                    b.Property<bool>("ChkHalfd")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkLunch")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkMadu")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkOther")
                        .HasColumnType("bit");

                    b.Property<bool>("ChkPamunugama")
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

                    b.Property<string>("RejctReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SinIntime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SinOuttime")
                        .HasColumnType("datetime2");

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

                    b.Property<string>("RFIDNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeptId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.NonReturnItemDsc", b =>
                {
                    b.HasOne("GatePassManagementSystem.Model.NonReturnItemDsc", null)
                        .WithMany("NonReturnItemDscsl")
                        .HasForeignKey("NonReturnItemDscId1");

                    b.HasOne("GatePassManagementSystem.Model.NonReturnableGP", null)
                        .WithMany("NonReturnItemDsc")
                        .HasForeignKey("NonReturnableGPId");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.NonReturnableGP", b =>
                {
                    b.HasOne("GatePassManagementSystem.Model.Department", "Department")
                        .WithMany("NonReturnableGP")
                        .HasForeignKey("DepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GatePassManagementSystem.Model.User", "User")
                        .WithMany("NonReturnableGP")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
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

            modelBuilder.Entity("GatePassManagementSystem.Model.ReturnItemDsc", b =>
                {
                    b.HasOne("GatePassManagementSystem.Model.ReturnableGP", null)
                        .WithMany("ReturnItemDsc")
                        .HasForeignKey("ReturnableGPId");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.ReturnableGP", b =>
                {
                    b.HasOne("GatePassManagementSystem.Model.Department", "Department")
                        .WithMany("ReturnableGP")
                        .HasForeignKey("DepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GatePassManagementSystem.Model.User", "User")
                        .WithMany("ReturnableGP")
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
                    b.Navigation("NonReturnableGP");

                    b.Navigation("PersonalGP");

                    b.Navigation("ReturnableGP");

                    b.Navigation("User");

                    b.Navigation("WorkerGP");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.NonReturnItemDsc", b =>
                {
                    b.Navigation("NonReturnItemDscsl");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.NonReturnableGP", b =>
                {
                    b.Navigation("NonReturnItemDsc");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.ReturnableGP", b =>
                {
                    b.Navigation("ReturnItemDsc");
                });

            modelBuilder.Entity("GatePassManagementSystem.Model.User", b =>
                {
                    b.Navigation("NonReturnableGP");

                    b.Navigation("PersonalGP");

                    b.Navigation("ReturnableGP");
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
