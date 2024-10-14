﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recipes.Infrastructure.Data;

#nullable disable

namespace Recipes.Infrastructure.Migrations
{
    [DbContext(typeof(RecipeDb))]
    [Migration("20241012213153_UpdateRecipeEntity")]
    partial class UpdateRecipeEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.Property<Guid>("AllIngredientsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AllIngredientsId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("IngredientRecipe");
                });

            modelBuilder.Entity("IngredientStep", b =>
                {
                    b.Property<Guid>("IngredientsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StepsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IngredientsId", "StepsId");

                    b.HasIndex("StepsId");

                    b.ToTable("IngredientStep");
                });

            modelBuilder.Entity("Recipes.Domain.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Recipes.Domain.Entities.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<string>("MainTag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "main_tag");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<int>("Servings")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "servings");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "tags");

                    b.Property<string>("TotalTime")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "total_time");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Recipes.Domain.Entities.Step", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HowTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "how_to");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Step");

                    b.HasAnnotation("Relational:JsonPropertyName", "steps");
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.HasOne("Recipes.Domain.Entities.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("AllIngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recipes.Domain.Entities.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IngredientStep", b =>
                {
                    b.HasOne("Recipes.Domain.Entities.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recipes.Domain.Entities.Step", null)
                        .WithMany()
                        .HasForeignKey("StepsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Recipes.Domain.Entities.Ingredient", b =>
                {
                    b.OwnsOne("Recipes.Domain.Entities.Measurement", "Measurement", b1 =>
                        {
                            b1.Property<Guid>("IngredientId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Unit")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasAnnotation("Relational:JsonPropertyName", "unit");

                            b1.Property<double?>("Value")
                                .HasColumnType("float")
                                .HasAnnotation("Relational:JsonPropertyName", "value");

                            b1.HasKey("IngredientId");

                            b1.ToTable("Ingredients");

                            b1.HasAnnotation("Relational:JsonPropertyName", "measurement");

                            b1.WithOwner()
                                .HasForeignKey("IngredientId");
                        });

                    b.Navigation("Measurement");
                });

            modelBuilder.Entity("Recipes.Domain.Entities.Step", b =>
                {
                    b.HasOne("Recipes.Domain.Entities.Recipe", null)
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("Recipes.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("Steps");
                });
#pragma warning restore 612, 618
        }
    }
}
