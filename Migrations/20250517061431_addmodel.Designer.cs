﻿// <auto-generated />
using System;
using AspnetCoreMvcFull.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspnetCoreMvcFull.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250517061431_addmodel")]
    partial class addmodel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AspnetCoreMvcFull.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tenxuong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AspnetCoreMvcFull.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("aplucdaudunloithep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("caosubemat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("caosubo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("caosuketdinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("caosuloplot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("caosur514")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudai1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudai2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudaicatlon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudaicatnho")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudailoithep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudaithanchinhbemat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudaithanchinhloplot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudaithannoibemat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudaithannoiloplot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("docobemat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("docoloplot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("doday")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("doday1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("doday2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dodaycaosubo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dodaycaosuketdinh3t")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kho")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kho1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kho2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khoangcach2daumoinoibo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khoangcach2daumoinoiloithep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khocaosubo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khocaosuketdinh3t")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kholoithep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khotieuchuanbemat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khotieuchuanloplot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khuondunbemat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khuondunloplot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khuonlodie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khuonsoiholder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kichthuoccuacaosudanmoinoi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loaicaosu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loaicaosu1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loaicaosu2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loaikhuondun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loaikhuondun1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loaikhuondun2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loithepsaukhidun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loitheptruockhidun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("mahang")
                        .HasColumnType("int");

                    b.Property<string>("mahangctl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("may")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nhietdodaumaydun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nhietdotrucxoan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pitch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("quycachloithep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sodaycatduoc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("soi1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("soi2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("solink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("solinkthanchinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("solinkthannoi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("sosoi")
                        .HasColumnType("int");

                    b.Property<string>("sosoiloithep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("thucte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tieuchuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdocolingdrum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdodun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdoduncaosu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdokeo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdomaydun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdomotor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdomotor1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdomotor2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdoquan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluogtest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluong")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluong1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluong2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluongdaukibemat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluongdaukiloplot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluongloithepspinning")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluongthanchinhbemat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluongthanchinhloplot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluongthannoibemat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trongluongthannoiloplot")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AspnetCoreMvcFull.Models.Product", b =>
                {
                    b.HasOne("AspnetCoreMvcFull.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AspnetCoreMvcFull.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
