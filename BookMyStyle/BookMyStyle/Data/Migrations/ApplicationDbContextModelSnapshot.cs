﻿// <auto-generated />
using System;
using BookMyStyle.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookMyStyle.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookMyStyle.Models.Korisnik", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("SalonID")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("terminID")
                        .HasColumnType("int");

                    b.Property<int>("tipFrizera")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SalonID");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BookMyStyle.Models.KorisnikSalon", b =>
                {
                    b.Property<int>("korisniksalonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("korisniksalonId"));

                    b.Property<int>("korisnikID")
                        .HasColumnType("int");

                    b.Property<int>("salonID")
                        .HasColumnType("int");

                    b.HasKey("korisniksalonId");

                    b.ToTable("KorisnikSalon", (string)null);
                });

            modelBuilder.Entity("BookMyStyle.Models.Obavijest", b =>
                {
                    b.Property<int>("obavijestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("obavijestID"));

                    b.Property<DateTime>("DatumIVrijeme")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tekst")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("terminID")
                        .HasColumnType("int");

                    b.HasKey("obavijestID");

                    b.ToTable("Obavijest", (string)null);
                });

            modelBuilder.Entity("BookMyStyle.Models.QRCodeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("QRCodeText")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("QRCodeModels");
                });

            modelBuilder.Entity("BookMyStyle.Models.Recenzija", b =>
                {
                    b.Property<int>("recenzijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("recenzijaID"));

                    b.Property<DateTime>("DatumObjave")
                        .HasColumnType("datetime2");

                    b.Property<string>("Komentar")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("KorisnikID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Ocjena")
                        .HasColumnType("int");

                    b.Property<int?>("SalonID")
                        .HasColumnType("int");

                    b.HasKey("recenzijaID");

                    b.HasIndex("KorisnikID");

                    b.HasIndex("SalonID");

                    b.ToTable("Recenzija", (string)null);
                });

            modelBuilder.Entity("BookMyStyle.Models.Salon", b =>
                {
                    b.Property<int>("salonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("salonID"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RadnoVrijeme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("salonID");

                    b.ToTable("Salon", (string)null);
                });

            modelBuilder.Entity("BookMyStyle.Models.Termin", b =>
                {
                    b.Property<int>("terminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("terminID"));

                    b.Property<string>("AdresaSalona")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DatumIVrijeme")
                        .HasColumnType("datetime2");

                    b.Property<string>("FrizerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("KorisnikID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NazivFrizera")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NazivSalona")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("salonID")
                        .HasColumnType("int");

                    b.Property<int>("uslugaID")
                        .HasColumnType("int");

                    b.HasKey("terminID");

                    b.HasIndex("FrizerID");

                    b.HasIndex("KorisnikID");

                    b.HasIndex("salonID");

                    b.ToTable("Termin", (string)null);
                });

            modelBuilder.Entity("BookMyStyle.Models.TerminUsluga", b =>
                {
                    b.Property<int>("terminuslugaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("terminuslugaId"));

                    b.Property<int>("terminID")
                        .HasColumnType("int");

                    b.Property<int>("uslugaID")
                        .HasColumnType("int");

                    b.HasKey("terminuslugaId");

                    b.ToTable("TerminUsluga", (string)null);
                });

            modelBuilder.Entity("BookMyStyle.Models.Usluga", b =>
                {
                    b.Property<int>("uslugaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("uslugaID"));

                    b.Property<double>("Cijena")
                        .HasColumnType("float");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("Popust")
                        .HasColumnType("float");

                    b.Property<int>("Tip")
                        .HasColumnType("int");

                    b.Property<int>("Trajanje")
                        .HasColumnType("int");

                    b.Property<int>("salonID")
                        .HasColumnType("int");

                    b.HasKey("uslugaID");

                    b.HasIndex("salonID");

                    b.ToTable("Usluga", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SmtpSettings", b =>
                {
                    b.Property<int>("SmtpSettingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SmtpSettingsId"));

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EnableSsl")
                        .HasColumnType("bit");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SmtpSettingsId");

                    b.ToTable("SmtpSettings");
                });

            modelBuilder.Entity("BookMyStyle.Models.Korisnik", b =>
                {
                    b.HasOne("BookMyStyle.Models.Salon", "Salon")
                        .WithMany("Frizeri")
                        .HasForeignKey("SalonID");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("BookMyStyle.Models.Recenzija", b =>
                {
                    b.HasOne("BookMyStyle.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikID");

                    b.HasOne("BookMyStyle.Models.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonID");

                    b.Navigation("Korisnik");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("BookMyStyle.Models.Termin", b =>
                {
                    b.HasOne("BookMyStyle.Models.Korisnik", "Frizer")
                        .WithMany()
                        .HasForeignKey("FrizerID");

                    b.HasOne("BookMyStyle.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikID");

                    b.HasOne("BookMyStyle.Models.Salon", null)
                        .WithMany("Termin")
                        .HasForeignKey("salonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Frizer");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("BookMyStyle.Models.Usluga", b =>
                {
                    b.HasOne("BookMyStyle.Models.Salon", "Salon")
                        .WithMany("Usluga")
                        .HasForeignKey("salonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BookMyStyle.Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BookMyStyle.Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookMyStyle.Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BookMyStyle.Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookMyStyle.Models.Salon", b =>
                {
                    b.Navigation("Frizeri");

                    b.Navigation("Termin");

                    b.Navigation("Usluga");
                });
#pragma warning restore 612, 618
        }
    }
}
