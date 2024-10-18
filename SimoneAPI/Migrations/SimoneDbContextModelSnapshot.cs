﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimoneAPI.DbContexts;

#nullable disable

namespace SimoneAPI.Migrations
{
    [DbContext(typeof(SimoneDbContext))]
    partial class SimoneDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("SimoneAPI.DataModels.Attendance", b =>
                {
                    b.Property<Guid>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TeamDancerRelationId")
                        .HasColumnType("TEXT");

                    b.HasKey("AttendanceId");

                    b.HasIndex("TeamDancerRelationId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("SimoneAPI.DataModels.DancerDataModel", b =>
                {
                    b.Property<Guid>("DancerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("TimeOfBirth")
                        .HasColumnType("TEXT");

                    b.HasKey("DancerId");

                    b.ToTable("DancerDataModels");

                    b.HasData(
                        new
                        {
                            DancerId = new Guid("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                            Name = "Petra",
                            TimeOfBirth = new DateOnly(1, 1, 1)
                        },
                        new
                        {
                            DancerId = new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                            Name = "Silje",
                            TimeOfBirth = new DateOnly(1, 1, 1)
                        });
                });

            modelBuilder.Entity("SimoneAPI.DataModels.Staff", b =>
                {
                    b.Property<Guid>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeOfBirth")
                        .HasColumnType("TEXT");

                    b.HasKey("StaffId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("SimoneAPI.DataModels.TeamDancerRelation", b =>
                {
                    b.Property<Guid?>("TeamDancerRelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DancerId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsTrialLesson")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("TEXT");

                    b.HasKey("TeamDancerRelationId");

                    b.HasIndex("DancerId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamDancerRelations");

                    b.HasData(
                        new
                        {
                            TeamDancerRelationId = new Guid("825d6913-a9c8-427f-97fa-eb73976f6e3c"),
                            DancerId = new Guid("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                            IsTrialLesson = false,
                            TeamId = new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48")
                        },
                        new
                        {
                            TeamDancerRelationId = new Guid("1c18938b-fa94-416d-bb02-ce2f201f4861"),
                            DancerId = new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                            IsTrialLesson = false,
                            TeamId = new Guid("b5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48")
                        },
                        new
                        {
                            TeamDancerRelationId = new Guid("48bedce6-f56e-4abb-8749-b9973e471d5e"),
                            DancerId = new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                            IsTrialLesson = false,
                            TeamId = new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48")
                        });
                });

            modelBuilder.Entity("SimoneAPI.DataModels.TeamDataModel", b =>
                {
                    b.Property<Guid>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SceduledTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TeamId");

                    b.ToTable("TeamDataModels");

                    b.HasData(
                        new
                        {
                            TeamId = new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                            Name = "Hiphop1",
                            Number = 1,
                            SceduledTime = "Mandag 16:00 - 16:45"
                        },
                        new
                        {
                            TeamId = new Guid("b5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                            Name = "MGP",
                            Number = 2,
                            SceduledTime = "Tirsdag 15:15 - 16:00"
                        });
                });

            modelBuilder.Entity("SimoneAPI.DataModels.WorkingHours", b =>
                {
                    b.Property<Guid>("WorkingHoursId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ChosenValueOfWorkingHours")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("TEXT");

                    b.HasKey("WorkingHoursId");

                    b.HasIndex("StaffId");

                    b.ToTable("WorkingHours");
                });

            modelBuilder.Entity("SimoneAPI.DataModels.Attendance", b =>
                {
                    b.HasOne("SimoneAPI.DataModels.TeamDancerRelation", "TeamDancerRelation")
                        .WithMany("Attendances")
                        .HasForeignKey("TeamDancerRelationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TeamDancerRelation");
                });

            modelBuilder.Entity("SimoneAPI.DataModels.TeamDancerRelation", b =>
                {
                    b.HasOne("SimoneAPI.DataModels.DancerDataModel", "DancerDataModel")
                        .WithMany("TeamDancerRelations")
                        .HasForeignKey("DancerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimoneAPI.DataModels.TeamDataModel", "TeamDataModel")
                        .WithMany("TeamDancerRelations")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DancerDataModel");

                    b.Navigation("TeamDataModel");
                });

            modelBuilder.Entity("SimoneAPI.DataModels.WorkingHours", b =>
                {
                    b.HasOne("SimoneAPI.DataModels.Staff", "Staff")
                        .WithMany("RegisteredWorkingHours")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("SimoneAPI.DataModels.DancerDataModel", b =>
                {
                    b.Navigation("TeamDancerRelations");
                });

            modelBuilder.Entity("SimoneAPI.DataModels.Staff", b =>
                {
                    b.Navigation("RegisteredWorkingHours");
                });

            modelBuilder.Entity("SimoneAPI.DataModels.TeamDancerRelation", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("SimoneAPI.DataModels.TeamDataModel", b =>
                {
                    b.Navigation("TeamDancerRelations");
                });
#pragma warning restore 612, 618
        }
    }
}
