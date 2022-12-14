// <auto-generated />
using System;
using ApiTgBot.Models.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiTgBot.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220811204558_RemoveAccountOwnerInTransfer")]
    partial class RemoveAccountOwnerInTransfer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GetCurrency")
                        .HasColumnType("int");

                    b.Property<decimal>("Rate")
                        .HasColumnType("money");

                    b.Property<int>("SetCurrency")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.Transfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AmountId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AmountId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.TransferArgument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FileId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TransferId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransferId");

                    b.ToTable("arguments");
                });

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.Currency", b =>
                {
                    b.HasOne("ApiTgBot.Models.EF.Tables.Account", null)
                        .WithMany("Wallet")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.Transfer", b =>
                {
                    b.HasOne("ApiTgBot.Models.EF.Tables.Currency", "Amount")
                        .WithMany()
                        .HasForeignKey("AmountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Amount");
                });

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.TransferArgument", b =>
                {
                    b.HasOne("ApiTgBot.Models.EF.Tables.Transfer", null)
                        .WithMany("arguments")
                        .HasForeignKey("TransferId");
                });

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.Account", b =>
                {
                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("ApiTgBot.Models.EF.Tables.Transfer", b =>
                {
                    b.Navigation("arguments");
                });
#pragma warning restore 612, 618
        }
    }
}
