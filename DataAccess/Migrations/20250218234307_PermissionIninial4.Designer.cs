﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(LearningCoursesDbContext))]
    [Migration("20250218234307_PermissionIninial4")]
    partial class PermissionIninial4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.PermissionRoleEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("PermissionRoleEntity");
                });

            modelBuilder.Entity("Core.Entities.PermissionsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PermissionsEntity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Create"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Read"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Update"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Delete"
                        });
                });

            modelBuilder.Entity("Core.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("Core.Entities.UserRoleEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoleEntity");
                });

            modelBuilder.Entity("CourseEntityStudentEntity", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentsId")
                        .HasColumnType("integer");

                    b.HasKey("CoursesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("CourseEntityStudentEntity");
                });

            modelBuilder.Entity("DataAccess.Entities.AuthorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("DataAccess.Entities.CourseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId")
                        .IsUnique();

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("DataAccess.Entities.ImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NewsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("DataAccess.Entities.LessonEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LessonText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("DataAccess.Entities.NewsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TextData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TitleImageId")
                        .HasColumnType("integer");

                    b.Property<int>("Views")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TitleImageId")
                        .IsUnique();

                    b.ToTable("News");
                });

            modelBuilder.Entity("DataAccess.Entities.StudentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Entities.PermissionRoleEntity", b =>
                {
                    b.HasOne("Core.Entities.PermissionsEntity", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("Core.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseEntityStudentEntity", b =>
                {
                    b.HasOne("DataAccess.Entities.CourseEntity", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.StudentEntity", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Entities.CourseEntity", b =>
                {
                    b.HasOne("DataAccess.Entities.AuthorEntity", "Author")
                        .WithOne("Course")
                        .HasForeignKey("DataAccess.Entities.CourseEntity", "AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("DataAccess.Entities.LessonEntity", b =>
                {
                    b.HasOne("DataAccess.Entities.CourseEntity", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DataAccess.Entities.NewsEntity", b =>
                {
                    b.HasOne("DataAccess.Entities.ImageEntity", "TitleImage")
                        .WithOne("News")
                        .HasForeignKey("DataAccess.Entities.NewsEntity", "TitleImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TitleImage");
                });

            modelBuilder.Entity("DataAccess.Entities.AuthorEntity", b =>
                {
                    b.Navigation("Course");
                });

            modelBuilder.Entity("DataAccess.Entities.CourseEntity", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("DataAccess.Entities.ImageEntity", b =>
                {
                    b.Navigation("News");
                });
#pragma warning restore 612, 618
        }
    }
}
