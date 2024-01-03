﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Product.Infrastructure;

#nullable disable

namespace Product.Infrastructure.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    partial class ProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BaseProductCategory", b =>
                {
                    b.Property<string>("CategoriesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CategoriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ProductCategories", (string)null);
                });

            modelBuilder.Entity("Persistence.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccuredOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductAggregate.BaseProduct", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductAggregate.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductAggregate.LaptopProduct", b =>
                {
                    b.HasBaseType("Product.Domain.Entities.ProductAggregate.BaseProduct");

                    b.ToTable("LaptopProducts", (string)null);
                });

            modelBuilder.Entity("BaseProductCategory", b =>
                {
                    b.HasOne("Product.Domain.Entities.ProductAggregate.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Product.Domain.Entities.ProductAggregate.BaseProduct", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductAggregate.BaseProduct", b =>
                {
                    b.OwnsOne("SharedKernel.ValueObjects.BrandModel", "BrandModel", b1 =>
                        {
                            b1.Property<string>("BaseProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Brand")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Brand");

                            b1.Property<string>("Model")
                                .IsRequired()
                                .HasMaxLength(70)
                                .HasColumnType("nvarchar(70)")
                                .HasColumnName("Model");

                            b1.HasKey("BaseProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("BaseProductId");
                        });

                    b.OwnsOne("Product.Domain.Entities.ProductDetails", "ProductDetails", b1 =>
                        {
                            b1.Property<string>("BaseProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)")
                                .HasColumnName("ProductDescription");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("ProductName");

                            b1.HasKey("BaseProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("BaseProductId");
                        });

                    b.OwnsOne("SharedKernel.ValueObjects.DimensionsInfo", "Dimensions", b1 =>
                        {
                            b1.Property<string>("BaseProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Height")
                                .HasColumnType("int")
                                .HasColumnName("Height");

                            b1.Property<int>("Length")
                                .HasColumnType("int")
                                .HasColumnName("Length");

                            b1.Property<int>("Weight")
                                .HasColumnType("int")
                                .HasColumnName("Weight");

                            b1.Property<int>("Width")
                                .HasColumnType("int")
                                .HasColumnName("Width");

                            b1.HasKey("BaseProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("BaseProductId");
                        });

                    b.OwnsOne("SharedKernel.ValueObjects.Money", "Price", b1 =>
                        {
                            b1.Property<string>("BaseProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(12, 2)
                                .HasColumnType("decimal(12,2)")
                                .HasColumnName("ProductPrice");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("ProductPriceCurrency");

                            b1.HasKey("BaseProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("BaseProductId");
                        });

                    b.Navigation("BrandModel")
                        .IsRequired();

                    b.Navigation("Dimensions")
                        .IsRequired();

                    b.Navigation("Price");

                    b.Navigation("ProductDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductAggregate.LaptopProduct", b =>
                {
                    b.HasOne("Product.Domain.Entities.ProductAggregate.BaseProduct", null)
                        .WithOne()
                        .HasForeignKey("Product.Domain.Entities.ProductAggregate.LaptopProduct", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Product.Domain.Entities.Display", "Display", b1 =>
                        {
                            b1.Property<string>("LaptopProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("RefreshRateGc")
                                .HasColumnType("int")
                                .HasColumnName("RefreshRateGc");

                            b1.Property<decimal>("ScreenDiagonalInch")
                                .HasPrecision(10, 2)
                                .HasColumnType("decimal(10,2)")
                                .HasColumnName("ScreenDiagonalInch");

                            b1.Property<int>("ViewingAngleDeg")
                                .HasColumnType("int")
                                .HasColumnName("ViewingAngleDeg");

                            b1.HasKey("LaptopProductId");

                            b1.ToTable("LaptopProducts");

                            b1.WithOwner()
                                .HasForeignKey("LaptopProductId");

                            b1.OwnsOne("Product.Domain.Entities.ScreenResolution", "ScreenResolution", b2 =>
                                {
                                    b2.Property<string>("DisplayLaptopProductId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<int>("Height")
                                        .HasColumnType("int")
                                        .HasColumnName("ScreenHeightPx");

                                    b2.Property<int>("Width")
                                        .HasColumnType("int")
                                        .HasColumnName("ScreenWidthPx");

                                    b2.HasKey("DisplayLaptopProductId");

                                    b2.ToTable("LaptopProducts");

                                    b2.WithOwner()
                                        .HasForeignKey("DisplayLaptopProductId");
                                });

                            b1.Navigation("ScreenResolution")
                                .IsRequired();
                        });

                    b.OwnsOne("Product.Domain.Entities.Graphics", "Graphics", b1 =>
                        {
                            b1.Property<string>("LaptopProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("GraphicsControllerType")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("GraphicsControllerType");

                            b1.Property<int>("VideoMemoryVolumeMb")
                                .HasColumnType("int")
                                .HasColumnName("VideoMemoryVolumeMb");

                            b1.HasKey("LaptopProductId");

                            b1.ToTable("LaptopProducts");

                            b1.WithOwner()
                                .HasForeignKey("LaptopProductId");

                            b1.OwnsOne("SharedKernel.ValueObjects.BrandModel", "BrandModel", b2 =>
                                {
                                    b2.Property<string>("GraphicsLaptopProductId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<string>("Brand")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("GraphicsBrand");

                                    b2.Property<string>("Model")
                                        .IsRequired()
                                        .HasMaxLength(70)
                                        .HasColumnType("nvarchar(70)")
                                        .HasColumnName("GraphicsModel");

                                    b2.HasKey("GraphicsLaptopProductId");

                                    b2.ToTable("LaptopProducts");

                                    b2.WithOwner()
                                        .HasForeignKey("GraphicsLaptopProductId");
                                });

                            b1.Navigation("BrandModel")
                                .IsRequired();
                        });

                    b.OwnsOne("Product.Domain.Entities.Processor", "Processor", b1 =>
                        {
                            b1.Property<string>("LaptopProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("CoreCount")
                                .HasColumnType("int")
                                .HasColumnName("ProcessorCoreCount");

                            b1.Property<decimal>("FrequencyGgc")
                                .HasPrecision(10, 2)
                                .HasColumnType("decimal(10,2)")
                                .HasColumnName("ProcessorFrequencyGgc");

                            b1.Property<int>("ThreadCount")
                                .HasColumnType("int")
                                .HasColumnName("ProcessorThreadCount");

                            b1.HasKey("LaptopProductId");

                            b1.ToTable("LaptopProducts");

                            b1.WithOwner()
                                .HasForeignKey("LaptopProductId");

                            b1.OwnsOne("SharedKernel.ValueObjects.BrandModel", "BrandModel", b2 =>
                                {
                                    b2.Property<string>("ProcessorLaptopProductId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<string>("Brand")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("ProcessorBrand");

                                    b2.Property<string>("Model")
                                        .IsRequired()
                                        .HasMaxLength(70)
                                        .HasColumnType("nvarchar(70)")
                                        .HasColumnName("ProcessorModel");

                                    b2.HasKey("ProcessorLaptopProductId");

                                    b2.ToTable("LaptopProducts");

                                    b2.WithOwner()
                                        .HasForeignKey("ProcessorLaptopProductId");
                                });

                            b1.Navigation("BrandModel")
                                .IsRequired();
                        });

                    b.OwnsOne("Product.Domain.Entities.Ram", "Ram", b1 =>
                        {
                            b1.Property<string>("LaptopProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<decimal>("FrequencyMgc")
                                .HasPrecision(10, 2)
                                .HasColumnType("decimal(10,2)")
                                .HasColumnName("RAMFrequencyMgc");

                            b1.Property<bool>("IsUpgradable")
                                .HasColumnType("bit")
                                .HasColumnName("RAMIsUpgradable");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("RAMType");

                            b1.Property<int>("VolumeGb")
                                .HasColumnType("int")
                                .HasColumnName("RAMVolumeGb");

                            b1.HasKey("LaptopProductId");

                            b1.ToTable("LaptopProducts");

                            b1.WithOwner()
                                .HasForeignKey("LaptopProductId");
                        });

                    b.OwnsOne("Product.Domain.Entities.StorageDevice", "Storage", b1 =>
                        {
                            b1.Property<string>("LaptopProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<bool>("IsUpgradeable")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bit")
                                .HasDefaultValue(false)
                                .HasColumnName("StorageIsUpgradeable");

                            b1.Property<string>("StorageType")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("StorageType");

                            b1.Property<int>("VolumeGb")
                                .HasColumnType("int")
                                .HasColumnName("StorageVolumeGb");

                            b1.HasKey("LaptopProductId");

                            b1.ToTable("LaptopProducts");

                            b1.WithOwner()
                                .HasForeignKey("LaptopProductId");
                        });

                    b.Navigation("Display")
                        .IsRequired();

                    b.Navigation("Graphics")
                        .IsRequired();

                    b.Navigation("Processor")
                        .IsRequired();

                    b.Navigation("Ram")
                        .IsRequired();

                    b.Navigation("Storage")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
