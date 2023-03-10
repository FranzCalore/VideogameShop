// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideogameShop.Database;

#nullable disable

namespace VideogameShop.Migrations
{
    [DbContext(typeof(VideogameContext))]
    [Migration("20230127150815_AggiuntiRifornimenti")]
    partial class AggiuntiRifornimenti
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VideogameShop.Models.Acquisto", b =>
                {
                    b.Property<int>("AcquistoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AcquistoId"));

                    b.Property<DateTime>("DataAcquisto")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.Property<int>("VideogiocoId")
                        .HasColumnType("int");

                    b.HasKey("AcquistoId");

                    b.HasIndex("VideogiocoId");

                    b.ToTable("Acquisto");
                });

            modelBuilder.Entity("VideogameShop.Models.Rifornimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeFornitore")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Prezzo")
                        .HasColumnType("float");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.Property<int>("VideogiocoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VideogiocoId");

                    b.ToTable("Rifornimenti");
                });

            modelBuilder.Entity("VideogameShop.Models.Tipologia", b =>
                {
                    b.Property<int>("TipologiaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipologiaId"));

                    b.Property<string>("TipologiaNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipologiaId");

                    b.ToTable("Tipologia");
                });

            modelBuilder.Entity("VideogameShop.Models.Videogioco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("text");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(150)");

                    b.Property<double>("Prezzo")
                        .HasColumnType("float");

                    b.Property<int?>("QuantitaDisponibile")
                        .HasColumnType("int");

                    b.Property<int>("TipologiaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipologiaId");

                    b.ToTable("Videogiochi");
                });

            modelBuilder.Entity("VideogameShop.Models.Acquisto", b =>
                {
                    b.HasOne("VideogameShop.Models.Videogioco", null)
                        .WithMany("Acquisti")
                        .HasForeignKey("VideogiocoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VideogameShop.Models.Rifornimento", b =>
                {
                    b.HasOne("VideogameShop.Models.Videogioco", "Videogioco")
                        .WithMany("ListaRifornimenti")
                        .HasForeignKey("VideogiocoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Videogioco");
                });

            modelBuilder.Entity("VideogameShop.Models.Videogioco", b =>
                {
                    b.HasOne("VideogameShop.Models.Tipologia", "Tipologia")
                        .WithMany("Videogiochi")
                        .HasForeignKey("TipologiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tipologia");
                });

            modelBuilder.Entity("VideogameShop.Models.Tipologia", b =>
                {
                    b.Navigation("Videogiochi");
                });

            modelBuilder.Entity("VideogameShop.Models.Videogioco", b =>
                {
                    b.Navigation("Acquisti");

                    b.Navigation("ListaRifornimenti");
                });
#pragma warning restore 612, 618
        }
    }
}
