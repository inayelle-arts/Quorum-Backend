﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Quorum.DataAccess.EfDataProvider;

namespace Quorum.DataAccess.EfDataProvider.Migrations
{
    [DbContext(typeof(EfDataContext))]
    [Migration("20190425135819_AddMissingRelations")]
    partial class AddMissingRelations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Quorum.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<bool>("IsCorrect");

                    b.Property<int>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Quorum.Entities.ChallengedAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChallengedQuestionId");

                    b.Property<bool>("IsUserCorrect");

                    b.Property<int>("SourceAnswerId");

                    b.HasKey("Id");

                    b.HasIndex("ChallengedQuestionId");

                    b.HasIndex("SourceAnswerId");

                    b.ToTable("ChallengedAnswers");
                });

            modelBuilder.Entity("Quorum.Entities.ChallengedQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChallengedTestId");

                    b.Property<int>("SourceQuestionId");

                    b.Property<int>("TotalScore");

                    b.Property<int>("UserScore");

                    b.HasKey("Id");

                    b.HasIndex("ChallengedTestId");

                    b.HasIndex("SourceQuestionId");

                    b.ToTable("ChallengedQuestions");
                });

            modelBuilder.Entity("Quorum.Entities.ChallengedTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaximumScore");

                    b.Property<int>("SourceTestId");

                    b.Property<int>("UserScore");

                    b.HasKey("Id");

                    b.HasIndex("SourceTestId");

                    b.ToTable("ChallengedTests");
                });

            modelBuilder.Entity("Quorum.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Quorum.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Quorum.Entities.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Quorum.Entities.Answer", b =>
                {
                    b.HasOne("Quorum.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quorum.Entities.ChallengedAnswer", b =>
                {
                    b.HasOne("Quorum.Entities.ChallengedQuestion", "ChallengedQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("ChallengedQuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Quorum.Entities.Answer", "SourceAnswer")
                        .WithMany()
                        .HasForeignKey("SourceAnswerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quorum.Entities.ChallengedQuestion", b =>
                {
                    b.HasOne("Quorum.Entities.ChallengedTest", "ChallengedTest")
                        .WithMany("Questions")
                        .HasForeignKey("ChallengedTestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Quorum.Entities.Question", "SourceQuestion")
                        .WithMany()
                        .HasForeignKey("SourceQuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quorum.Entities.ChallengedTest", b =>
                {
                    b.HasOne("Quorum.Entities.Test", "SourceTest")
                        .WithMany()
                        .HasForeignKey("SourceTestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quorum.Entities.Question", b =>
                {
                    b.HasOne("Quorum.Entities.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quorum.Entities.Tag", b =>
                {
                    b.HasOne("Quorum.Entities.Test", "Test")
                        .WithMany("Tags")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
