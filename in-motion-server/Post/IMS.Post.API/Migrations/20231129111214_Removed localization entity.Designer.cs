﻿// <auto-generated />
using System;
using IMS.Post.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IMS.Post.API.Migrations
{
    [DbContext(typeof(ImsPostDbContext))]
    [Migration("20231129111214_Removed localization entity")]
    partial class Removedlocalizationentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("post")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IMS.Post.Domain.Entities.Other.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<Guid>("ExternalAuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("external_author_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("tags", "post");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("description");

                    b.Property<Guid>("ExternalAuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("external_author_id");

                    b.Property<Guid>("IterationId")
                        .HasColumnType("uuid")
                        .HasColumnName("iteration_id");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("title");

                    b.Property<int>("Visibility")
                        .HasColumnType("integer")
                        .HasColumnName("visibility");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("IterationId");

                    b.ToTable("posts", "post");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<Guid>("ExternalAuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("external_author_id");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_date");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PostId");

                    b.ToTable("post_comments", "post");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostCommentReaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Emoji")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("emoji");

                    b.Property<Guid>("ExternalAuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("external_author_id");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modification_date");

                    b.Property<Guid>("PostCommentId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_comment_id");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PostCommentId");

                    b.ToTable("post_comment_reaction", "post");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostIteration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("IterationName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date_time");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("post_iterations", "post");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostReaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Emoji")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("emoji");

                    b.Property<Guid>("ExternalAuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("external_author_id");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modification_date");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PostId");

                    b.ToTable("post_reactions", "post");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostVideo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

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

                    b.Property<Guid>("ExternalAuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("external_author_id");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("filename");

                    b.Property<DateTime>("LastEditionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_edition_name");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PostId");

                    b.ToTable("post_videos", "post");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid");

                    b.HasKey("PostId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("posts_tags_relations", "post");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.Post", b =>
                {
                    b.HasOne("IMS.Post.Domain.Entities.Post.PostIteration", "Iteration")
                        .WithMany("IterationPosts")
                        .HasForeignKey("IterationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Iteration");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostComment", b =>
                {
                    b.HasOne("IMS.Post.Domain.Entities.Post.Post", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostCommentReaction", b =>
                {
                    b.HasOne("IMS.Post.Domain.Entities.Post.PostComment", "PostComment")
                        .WithMany("Reactions")
                        .HasForeignKey("PostCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostComment");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostReaction", b =>
                {
                    b.HasOne("IMS.Post.Domain.Entities.Post.Post", "Post")
                        .WithMany("PostReactions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostVideo", b =>
                {
                    b.HasOne("IMS.Post.Domain.Entities.Post.Post", "Post")
                        .WithMany("Videos")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("IMS.Post.Domain.Entities.Post.Post", null)
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Post.Domain.Entities.Other.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.Post", b =>
                {
                    b.Navigation("PostComments");

                    b.Navigation("PostReactions");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostComment", b =>
                {
                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("IMS.Post.Domain.Entities.Post.PostIteration", b =>
                {
                    b.Navigation("IterationPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
