﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentApi.Models;

namespace StudentApi.Migrations
{
    [DbContext(typeof(StudentContext))]
    [Migration("20200708101137_firstStudentMigration")]
    partial class firstStudentMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StudentApi.Models.Sports", b =>
                {
                    b.Property<int>("SportsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("SportsName")
                        .HasColumnType("text");

                    b.Property<long>("StudID")
                        .HasColumnType("bigint");

                    b.HasKey("SportsID");

                    b.HasIndex("StudID");

                    b.ToTable("SportsTable");
                });

            modelBuilder.Entity("StudentApi.Models.Student", b =>
                {
                    b.Property<long>("SID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("SName")
                        .HasColumnType("text");

                    b.HasKey("SID");

                    b.ToTable("StudentTable");
                });

            modelBuilder.Entity("StudentApi.Subject", b =>
                {
                    b.Property<long>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("StudId")
                        .HasColumnType("bigint");

                    b.Property<string>("SubjectTitle")
                        .HasColumnType("text");

                    b.HasKey("SubjectID");

                    b.HasIndex("StudId");

                    b.ToTable("SubjectTable");
                });

            modelBuilder.Entity("StudentApi.Models.Sports", b =>
                {
                    b.HasOne("StudentApi.Models.Student", "student")
                        .WithMany("StudentSports")
                        .HasForeignKey("StudID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentApi.Subject", b =>
                {
                    b.HasOne("StudentApi.Models.Student", "student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
