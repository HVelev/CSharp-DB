﻿// <auto-generated />
using System;
using Footballers.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Footballers.Migrations
{
    [DbContext(typeof(FootballersContext))]
    [Migration("20220806111407_SetUp")]
    partial class SetUp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Footballers.Data.Models.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("Footballers.Data.Models.Footballer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BestSkillType")
                        .HasColumnType("int");

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ContractEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ContractStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<int>("PositionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.ToTable("Footballers");
                });

            modelBuilder.Entity("Footballers.Data.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<int>("Trophies")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Footballers.Data.Models.TeamFootballer", b =>
                {
                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("FootballerId")
                        .HasColumnType("int");

                    b.HasKey("TeamId", "FootballerId");

                    b.HasIndex("FootballerId");

                    b.ToTable("TeamsFootballers");
                });

            modelBuilder.Entity("Footballers.Data.Models.Footballer", b =>
                {
                    b.HasOne("Footballers.Data.Models.Coach", "Coach")
                        .WithMany("Footballers")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Footballers.Data.Models.TeamFootballer", b =>
                {
                    b.HasOne("Footballers.Data.Models.Footballer", "Footballer")
                        .WithMany("TeamFootballers")
                        .HasForeignKey("FootballerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Footballers.Data.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
