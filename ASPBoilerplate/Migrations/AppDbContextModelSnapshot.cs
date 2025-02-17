﻿// <auto-generated />
using System;
using ASPBoilerplate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASPBoilerplate.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("ASPBoilerplate.Modules.Chat.ChatHubConnections", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ChatHubConnections");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.Chat.ChatInbox", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("MessagedUserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ChatInboxes");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.Chat.MessageHistory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MessageHistories");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.File.FileEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginalName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Storage")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.Entity.RestrictedUserEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPasswordSet")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OtpId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("OtpId");

                    b.ToTable("RestrictedUsers");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.Entity.RestrictedUserProfileEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("DOB")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RestrictedUserProfiles");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.Entity.UserOtpEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ExpireAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Otp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserOtps");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.Entity.UserTokenEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceSignature")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("TEXT");

                    b.Property<string>("RestrictedUserEntityId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UnrestrictedUserEntityId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RestrictedUserEntityId");

                    b.HasIndex("UnrestrictedUserEntityId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.UnrestrictedUserEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OtpId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OtpId");

                    b.HasIndex("ProfileId");

                    b.ToTable("UnrestrictedUsers");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.UnrestrictedUserProfileEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("DOB")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UnrestrictedUserProfiles");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.Entity.RestrictedUserEntity", b =>
                {
                    b.HasOne("ASPBoilerplate.Modules.User.Entity.UserOtpEntity", "Otp")
                        .WithMany()
                        .HasForeignKey("OtpId");

                    b.Navigation("Otp");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.Entity.RestrictedUserProfileEntity", b =>
                {
                    b.HasOne("ASPBoilerplate.Modules.User.Entity.RestrictedUserEntity", "UserEntity")
                        .WithOne("Profile")
                        .HasForeignKey("ASPBoilerplate.Modules.User.Entity.RestrictedUserProfileEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.Entity.UserTokenEntity", b =>
                {
                    b.HasOne("ASPBoilerplate.Modules.User.Entity.RestrictedUserEntity", null)
                        .WithMany("Tokens")
                        .HasForeignKey("RestrictedUserEntityId");

                    b.HasOne("ASPBoilerplate.Modules.User.UnrestrictedUserEntity", null)
                        .WithMany("Tokens")
                        .HasForeignKey("UnrestrictedUserEntityId");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.UnrestrictedUserEntity", b =>
                {
                    b.HasOne("ASPBoilerplate.Modules.User.Entity.UserOtpEntity", "Otp")
                        .WithMany()
                        .HasForeignKey("OtpId");

                    b.HasOne("ASPBoilerplate.Modules.User.UnrestrictedUserEntity", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.Navigation("Otp");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.UnrestrictedUserProfileEntity", b =>
                {
                    b.HasOne("ASPBoilerplate.Modules.User.UnrestrictedUserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.Entity.RestrictedUserEntity", b =>
                {
                    b.Navigation("Profile");

                    b.Navigation("Tokens");
                });

            modelBuilder.Entity("ASPBoilerplate.Modules.User.UnrestrictedUserEntity", b =>
                {
                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
