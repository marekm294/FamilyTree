﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AppDatabaseContext))]
    partial class AppDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data.Schemes.FamilyMemberScheme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AboutMember")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FamilyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("MiddleNames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.ComplexProperty<Dictionary<string, object>>("Birth", "Data.Schemes.FamilyMemberScheme.Birth#Event", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly?>("Date")
                                .HasColumnType("date");

                            b1.ComplexProperty<Dictionary<string, object>>("Place", "Data.Schemes.FamilyMemberScheme.Birth#Event.Place#Place", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("City")
                                        .HasMaxLength(128)
                                        .HasColumnType("nvarchar(128)");

                                    b2.Property<Point>("Coordinates")
                                        .HasColumnType("geometry");

                                    b2.Property<string>("Country")
                                        .HasMaxLength(128)
                                        .HasColumnType("nvarchar(128)");
                                });
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Death", "Data.Schemes.FamilyMemberScheme.Death#Event", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly?>("Date")
                                .HasColumnType("date");

                            b1.ComplexProperty<Dictionary<string, object>>("Place", "Data.Schemes.FamilyMemberScheme.Death#Event.Place#Place", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("City")
                                        .HasMaxLength(128)
                                        .HasColumnType("nvarchar(128)");

                                    b2.Property<Point>("Coordinates")
                                        .HasColumnType("geometry");

                                    b2.Property<string>("Country")
                                        .HasMaxLength(128)
                                        .HasColumnType("nvarchar(128)");
                                });
                        });

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.ToTable("FamilyMembers", (string)null);
                });

            modelBuilder.Entity("Data.Schemes.FamilyScheme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("Data.Schemes.TenantScheme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Tenants", (string)null);
                });

            modelBuilder.Entity("Data.Schemes.WeddingScheme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PartnerId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PartnerId2")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.ComplexProperty<Dictionary<string, object>>("WeddingEvent", "Data.Schemes.WeddingScheme.WeddingEvent#Event", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly?>("Date")
                                .HasColumnType("date");

                            b1.ComplexProperty<Dictionary<string, object>>("Place", "Data.Schemes.WeddingScheme.WeddingEvent#Event.Place#Place", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("City")
                                        .HasMaxLength(128)
                                        .HasColumnType("nvarchar(128)");

                                    b2.Property<Point>("Coordinates")
                                        .HasColumnType("geometry");

                                    b2.Property<string>("Country")
                                        .HasMaxLength(128)
                                        .HasColumnType("nvarchar(128)");
                                });
                        });

                    b.HasKey("Id");

                    b.HasIndex("PartnerId1");

                    b.HasIndex("PartnerId2");

                    b.ToTable("Weddings", (string)null);
                });

            modelBuilder.Entity("Data.Schemes.FamilyMemberScheme", b =>
                {
                    b.HasOne("Data.Schemes.FamilyScheme", null)
                        .WithMany()
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Schemes.WeddingScheme", b =>
                {
                    b.HasOne("Data.Schemes.FamilyMemberScheme", null)
                        .WithMany()
                        .HasForeignKey("PartnerId1")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Data.Schemes.FamilyMemberScheme", null)
                        .WithMany()
                        .HasForeignKey("PartnerId2")
                        .OnDelete(DeleteBehavior.NoAction);
                });
#pragma warning restore 612, 618
        }
    }
}
