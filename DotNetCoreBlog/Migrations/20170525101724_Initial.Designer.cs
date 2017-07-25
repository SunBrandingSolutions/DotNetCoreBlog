using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DotNetCoreBlog.Models;

namespace DotNetCoreBlog.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20170525101724_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DotNetCoreBlog.Models.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired()
                        .IsUnicode(true);

                    b.Property<DateTimeOffset>("PubDate");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<string>("Tags")
                        .HasMaxLength(500);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.Property<string>("UrlSlug")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("UrlSlug")
                        .IsUnique();

                    b.ToTable("Posts");
                });
        }
    }
}
