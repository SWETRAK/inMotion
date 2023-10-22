﻿// <auto-generated />
using System;
using IMS.User.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IMS.User.API.Migrations
{
    [DbContext(typeof(ImsUserDbContext))]
    [Migration("20231021104405_migrationFix3")]
    partial class migrationFix3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("user")
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IMS.User.Domain.Entities.UserMetas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Bio")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("bio");

                    b.Property<Guid>("ProfileVideoId")
                        .HasColumnType("uuid")
                        .HasColumnName("profile_video_id");

                    b.Property<Guid>("UserExternalId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("user_metas", "user");
                });

            modelBuilder.Entity("IMS.User.Domain.Entities.UserProfileVideo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AuthorExternalId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_external_id");

                    b.Property<string>("BucketLocation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bucket_location");

                    b.Property<string>("BucketName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bucket_name");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content_type");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("filename");

                    b.Property<DateTime>("LastEditionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_edition_name");

                    b.Property<Guid>("UserMetasId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserMetasId")
                        .IsUnique();

                    b.ToTable("user_profile_videos", "user");
                });

            modelBuilder.Entity("IMS.User.Domain.Entities.UserProfileVideo", b =>
                {
                    b.HasOne("IMS.User.Domain.Entities.UserMetas", "UserMetas")
                        .WithOne("ProfileVideo")
                        .HasForeignKey("IMS.User.Domain.Entities.UserProfileVideo", "UserMetasId")
                        .HasPrincipalKey("IMS.User.Domain.Entities.UserMetas", "ProfileVideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserMetas");
                });

            modelBuilder.Entity("IMS.User.Domain.Entities.UserMetas", b =>
                {
                    b.Navigation("ProfileVideo");
                });
#pragma warning restore 612, 618
        }
    }
}
