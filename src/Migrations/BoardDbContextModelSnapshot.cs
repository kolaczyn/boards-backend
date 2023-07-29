﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using boards.Infrastructure;

#nullable disable

namespace boards.Migrations
{
    [DbContext(typeof(BoardDbContext))]
    partial class BoardDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("boards.Infrastructure.Models.BoardDb", b =>
                {
                    b.Property<string>("Slug")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Slug");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("boards.Infrastructure.Models.ReplyDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ThreadDbId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("boardSlug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ThreadDbId");

                    b.HasIndex("boardSlug");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("boards.Infrastructure.Models.ThreadDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BoardSlug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BoardSlug");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("boards.Infrastructure.Models.ReplyDb", b =>
                {
                    b.HasOne("boards.Infrastructure.Models.ThreadDb", null)
                        .WithMany("Replies")
                        .HasForeignKey("ThreadDbId");

                    b.HasOne("boards.Infrastructure.Models.BoardDb", "board")
                        .WithMany("Replies")
                        .HasForeignKey("boardSlug")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("board");
                });

            modelBuilder.Entity("boards.Infrastructure.Models.ThreadDb", b =>
                {
                    b.HasOne("boards.Infrastructure.Models.BoardDb", "Board")
                        .WithMany()
                        .HasForeignKey("BoardSlug")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("boards.Infrastructure.Models.BoardDb", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("boards.Infrastructure.Models.ThreadDb", b =>
                {
                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}
