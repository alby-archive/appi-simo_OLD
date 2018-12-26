﻿// <auto-generated />
using System;
using AppiSimo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AppiSimo.Api.Migrations
{
    [DbContext(typeof(KingRogerContext))]
    partial class KingRogerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AppiSimo.Shared.Model.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("AppiSimo.Shared.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("EventId");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AppiSimo.Shared.Model.User", b =>
                {
                    b.HasOne("AppiSimo.Shared.Model.Event")
                        .WithMany("Users")
                        .HasForeignKey("EventId");
                });
#pragma warning restore 612, 618
        }
    }
}
