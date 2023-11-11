﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(LmsDbContext))]
    partial class LmsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            GroupId = new Guid("84a3e8e3-ccd9-4c03-9a69-292bbb09ffd0"),
                            Department = "Computer Science",
                            Name = "Group A"
                        },
                        new
                        {
                            GroupId = new Guid("dc669cea-fd57-4739-97d1-6d12299c9372"),
                            Department = "Electrical Engineering",
                            Name = "Group B"
                        },
                        new
                        {
                            GroupId = new Guid("d4c5c34a-1983-442c-9a6c-f7e90e2eac3d"),
                            Department = "Mechanical Engineering",
                            Name = "Group C"
                        },
                        new
                        {
                            GroupId = new Guid("72e3c55f-1de1-4c51-b29d-c5f4a8dd62bd"),
                            Department = "Physics",
                            Name = "Group D"
                        },
                        new
                        {
                            GroupId = new Guid("a10ed067-9239-44eb-9466-154b0b98aea4"),
                            Department = "Mathematics",
                            Name = "Group E"
                        },
                        new
                        {
                            GroupId = new Guid("02c79aa1-4c08-4714-8cfa-065615bf523a"),
                            Department = "Chemistry",
                            Name = "Group F"
                        },
                        new
                        {
                            GroupId = new Guid("c0179c37-618f-491a-b275-16d19cd85a15"),
                            Department = "Biology",
                            Name = "Group G"
                        },
                        new
                        {
                            GroupId = new Guid("ac918518-8ff0-40c4-8e61-8819101f1c29"),
                            Department = "Civil Engineering",
                            Name = "Group H"
                        },
                        new
                        {
                            GroupId = new Guid("8e198820-4aa5-4285-a858-ccb48c4a7ccf"),
                            Department = "Environmental Science",
                            Name = "Group I"
                        },
                        new
                        {
                            GroupId = new Guid("8a595e7e-7a77-4cb3-93d0-d00a5551176c"),
                            Department = "Information Technology",
                            Name = "Group J"
                        },
                        new
                        {
                            GroupId = new Guid("306815ec-19f0-435a-9d78-221459590ac7"),
                            Department = "Aerospace Engineering",
                            Name = "Group K"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Lecturer", b =>
                {
                    b.Property<Guid>("LecturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Degree")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LecturerId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("StudentId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(319)
                        .HasColumnType("character varying(319)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Lecturer", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne("Lecturer")
                        .HasForeignKey("Domain.Entities.Lecturer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Student", b =>
                {
                    b.HasOne("Domain.Entities.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("Domain.Entities.Student", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Group", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Lecturer");

                    b.Navigation("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
