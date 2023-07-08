﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TNWalks.API.Data;

#nullable disable

namespace TNWalks.API.Migrations
{
    [DbContext(typeof(TnWalksDbContext))]
    [Migration("20230703105134_TodosCreate")]
    partial class TodosCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TNWalks.API.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fd271b89-4949-423d-a975-e9ed523065e5"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("a3edbed0-6920-4f66-a3d5-5d55ee5fe4ab"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("0c740696-c55b-43ba-b729-304dfb376cdd"),
                            Name = "Difficult"
                        });
                });

            modelBuilder.Entity("TNWalks.API.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f509a56f-292f-4df0-b4ac-2aa58a10697a"),
                            Code = "NR",
                            Name = "Nashville Region",
                            RegionImageUrl = "https://picsum.photos/200/300"
                        },
                        new
                        {
                            Id = new Guid("2f30f07d-58e2-4a66-b343-af3ca22fc58e"),
                            Code = "CR",
                            Name = "Clarksville Region",
                            RegionImageUrl = "https://picsum.photos/200/300"
                        },
                        new
                        {
                            Id = new Guid("b2105bdd-c2a1-49d5-ba18-326bf12ae6e2"),
                            Code = "UT",
                            Name = "Knoxville Region",
                            RegionImageUrl = "https://picsum.photos/200/300"
                        },
                        new
                        {
                            Id = new Guid("8d6865c7-436c-46bf-a3e3-9e0c18212db4"),
                            Code = "SM",
                            Name = "Smokey Mountains",
                            RegionImageUrl = "https://picsum.photos/200/300"
                        });
                });

            modelBuilder.Entity("TNWalks.API.Models.Domain.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TNWalks.API.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("TNWalks.API.Models.Domain.Walk", b =>
                {
                    b.HasOne("TNWalks.API.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TNWalks.API.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}