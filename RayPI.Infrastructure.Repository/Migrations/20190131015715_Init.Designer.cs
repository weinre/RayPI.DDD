﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RayPI.Infrastructure.Repository;

namespace RayPI.Infrastructure.Repository.Migrations
{
    [DbContext(typeof(BizUnitOfWork))]
    [Migration("20190131015715_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RayPI.Domain.Entities.StudentEntity", b =>
                {
                    b.Property<long>("Id");

                    b.Property<int>("Age");

                    b.Property<long?>("CreateId");

                    b.Property<string>("CreateName")
                        .HasMaxLength(128);

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long?>("UpdateId");

                    b.Property<string>("UpdateName")
                        .HasMaxLength(128);

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("RayPI.Domain.Entities.TeacherEntity", b =>
                {
                    b.Property<long>("Id");

                    b.Property<int>("Age");

                    b.Property<long?>("CreateId");

                    b.Property<string>("CreateName")
                        .HasMaxLength(128);

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long?>("UpdateId");

                    b.Property<string>("UpdateName")
                        .HasMaxLength(128);

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}
