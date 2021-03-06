﻿// <auto-generated />
using HallOfFameAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HallOfFameAPI.Migrations
{
    [DbContext(typeof(HallOfFameContext))]
    [Migration("20190204181148_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HallOfFameAPI.Models.Person", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("HallOfFameAPI.Models.Skill", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Level");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long>("PersonID");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("HallOfFameAPI.Models.Skill", b =>
                {
                    b.HasOne("HallOfFameAPI.Models.Person")
                        .WithMany("Skills")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
