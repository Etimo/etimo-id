﻿// <auto-generated />
using System;
using Etimo.Id.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Etimo.Id.Data.Migrations
{
    [DbContext(typeof(EtimoIdDbContext))]
    partial class EtimoIdDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Etimo.Id.Entities.AccessToken", b =>
                {
                    b.Property<Guid>("AccessTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Disabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Scope")
                        .HasColumnType("text");

                    b.HasKey("AccessTokenId");

                    b.ToTable("AccessTokens");
                });

            modelBuilder.Entity("Etimo.Id.Entities.Application", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("ClientSecret")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("HomepageUri")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("RedirectUri")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ApplicationId");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Etimo.Id.Entities.AuthorizationCode", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<Guid?>("AccessTokenId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RedirectUri")
                        .HasColumnType("text");

                    b.Property<string>("Scope")
                        .HasColumnType("text");

                    b.Property<bool>("Used")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Code");

                    b.HasIndex("AccessTokenId");

                    b.ToTable("AuthorizationCodes");
                });

            modelBuilder.Entity("Etimo.Id.Entities.RefreshToken", b =>
                {
                    b.Property<string>("RefreshTokenId")
                        .HasColumnType("text");

                    b.Property<Guid?>("AccessTokenId")
                        .HasColumnType("uuid");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RedirectUri")
                        .HasColumnType("text");

                    b.Property<string>("Scope")
                        .HasColumnType("text");

                    b.Property<bool>("Used")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("AccessTokenId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("Code");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Etimo.Id.Entities.Scope", b =>
                {
                    b.Property<Guid>("ScopeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ScopeId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("Name");

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("Etimo.Id.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Etimo.Id.Entities.Application", b =>
                {
                    b.HasOne("Etimo.Id.Entities.User", "User")
                        .WithMany("Applications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Etimo.Id.Entities.AuthorizationCode", b =>
                {
                    b.HasOne("Etimo.Id.Entities.AccessToken", "AccessToken")
                        .WithMany()
                        .HasForeignKey("AccessTokenId");

                    b.Navigation("AccessToken");
                });

            modelBuilder.Entity("Etimo.Id.Entities.RefreshToken", b =>
                {
                    b.HasOne("Etimo.Id.Entities.AccessToken", "AccessToken")
                        .WithMany()
                        .HasForeignKey("AccessTokenId");

                    b.HasOne("Etimo.Id.Entities.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Etimo.Id.Entities.AuthorizationCode", "AuthorizationCode")
                        .WithMany()
                        .HasForeignKey("Code");

                    b.HasOne("Etimo.Id.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessToken");

                    b.Navigation("Application");

                    b.Navigation("AuthorizationCode");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Etimo.Id.Entities.Scope", b =>
                {
                    b.HasOne("Etimo.Id.Entities.Application", "Application")
                        .WithMany("Scopes")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Etimo.Id.Entities.Application", b =>
                {
                    b.Navigation("Scopes");
                });

            modelBuilder.Entity("Etimo.Id.Entities.User", b =>
                {
                    b.Navigation("Applications");
                });
#pragma warning restore 612, 618
        }
    }
}
