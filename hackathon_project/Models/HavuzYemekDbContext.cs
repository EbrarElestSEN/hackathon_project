using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace hackathon_project.Models;

public partial class HavuzYemekDbContext : DbContext
{
    public HavuzYemekDbContext()
    {
    }

    public HavuzYemekDbContext(DbContextOptions<HavuzYemekDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HavuzKatilimcilari> HavuzKatilimcilaris { get; set; }

    public virtual DbSet<HavuzSayaclari> HavuzSayaclaris { get; set; }

    public virtual DbSet<HavuzSiparisleri> HavuzSiparisleris { get; set; }

    public virtual DbSet<Havuzlar> Havuzlars { get; set; }

    public virtual DbSet<Kategoriler> Kategorilers { get; set; }

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    public virtual DbSet<Restoranlar> Restoranlars { get; set; }

    public virtual DbSet<Sehirler> Sehirlers { get; set; }

    public virtual DbSet<Urunler> Urunlers { get; set; }

    public virtual DbSet<Yurtlar> Yurtlars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite("Data Source=HavuzYemekDB.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HavuzKatilimcilari>(entity =>
        {
            entity.HasKey(e => e.KatilimId).HasName("PK__HavuzKat__7739554B2485AD44");

            entity.ToTable("HavuzKatilimcilari");

            entity.HasIndex(e => new { e.HavuzId, e.KullaniciId }, "UQ_KatilimBenzersiz").IsUnique();

            entity.Property(e => e.KatilimId).HasColumnName("KatilimID");
            entity.Property(e => e.AnonymousAlias).HasMaxLength(50);
            entity.Property(e => e.HavuzId).HasColumnName("HavuzID");
            entity.Property(e => e.KatilimZamani).HasDefaultValueSql("(datetime('now'))");
            entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");
            entity.Property(e => e.ToplamTutar).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Havuz).WithMany(p => p.HavuzKatilimcilaris)
                .HasForeignKey(d => d.HavuzId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Katilim_Havuz");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.HavuzKatilimcilaris)
                .HasForeignKey(d => d.KullaniciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Katilim_Kullanici");
        });

        modelBuilder.Entity<HavuzSayaclari>(entity =>
        {
            entity.HasKey(e => e.SayacId).HasName("PK__HavuzSay__B4524D05B68D713D");

            entity.ToTable("HavuzSayaclari");

            entity.Property(e => e.SayacId).HasColumnName("SayacID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.HavuzId).HasColumnName("HavuzID");

            entity.HasOne(d => d.Havuz).WithMany(p => p.HavuzSayaclaris)
                .HasForeignKey(d => d.HavuzId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sayac_Havuz");
        });

        modelBuilder.Entity<HavuzSiparisleri>(entity =>
        {
            entity.HasKey(e => e.SiparisId).HasName("PK__HavuzSip__C3F03BDDA6DE33FA");

            entity.ToTable("HavuzSiparisleri");

            entity.Property(e => e.SiparisId).HasColumnName("SiparisID");
            entity.Property(e => e.Adet).HasDefaultValue(1);
            entity.Property(e => e.HavuzId).HasColumnName("HavuzID");
            entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");
            entity.Property(e => e.SiparisTarihi).HasDefaultValueSql("(datetime('now'))");
            entity.Property(e => e.Tutar).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UrunId).HasColumnName("UrunID");

            entity.HasOne(d => d.Havuz).WithMany(p => p.HavuzSiparisleris)
                .HasForeignKey(d => d.HavuzId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Siparis_Havuz");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.HavuzSiparisleris)
                .HasForeignKey(d => d.KullaniciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Siparis_Kullanici");

            entity.HasOne(d => d.Urun).WithMany(p => p.HavuzSiparisleris)
                .HasForeignKey(d => d.UrunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Siparis_Urun");
        });

        modelBuilder.Entity<Havuzlar>(entity =>
        {
            entity.HasKey(e => e.HavuzId).HasName("PK__Havuzlar__87E51550CD2113BF");

            entity.ToTable("Havuzlar");

            entity.Property(e => e.HavuzId).HasColumnName("HavuzID");
            entity.Property(e => e.MaksimumKatilimci).HasDefaultValue(10);
            entity.Property(e => e.MevcutTutar).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MinimumTutar).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OlusturanKullaniciId).HasColumnName("OlusturanKullaniciID");
            entity.Property(e => e.OlusturmaTarihi).HasDefaultValueSql("(datetime('now'))");
            entity.Property(e => e.RestoranId).HasColumnName("RestoranID");
            entity.Property(e => e.UrunId).HasColumnName("UrunID");

            entity.HasOne(d => d.OlusturanKullanici).WithMany(p => p.Havuzlars)
                .HasForeignKey(d => d.OlusturanKullaniciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Havuz_Kullanici");

            entity.HasOne(d => d.Restoran).WithMany(p => p.Havuzlars)
                .HasForeignKey(d => d.RestoranId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Havuz_Restoran");

            entity.HasOne(d => d.Urun).WithMany(p => p.Havuzlars)
                .HasForeignKey(d => d.UrunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Havuz_Urun");
        });

        modelBuilder.Entity<Kategoriler>(entity =>
        {
            entity.HasKey(e => e.KategoriId).HasName("PK__Kategori__1782CC9265FCEE5E");

            entity.ToTable("Kategoriler");

            entity.HasIndex(e => e.KategoriAdi, "UQ__Kategori__110FF79EE80D1321").IsUnique();

            entity.Property(e => e.KategoriId).HasColumnName("KategoriID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.IkonUrl)
                .HasMaxLength(500)
                .HasColumnName("IkonURL");
            entity.Property(e => e.KategoriAdi).HasMaxLength(100);
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.KullaniciId).HasName("PK__Kullanic__E011F09BF93E349A");

            entity.ToTable("Kullanicilar");

            entity.HasIndex(e => e.Email, "UQ__Kullanic__A9D1053415DA2FAF").IsUnique();

            entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");
            entity.Property(e => e.AdSoyad).HasMaxLength(150);
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.KayitTarihi).HasDefaultValueSql("(datetime('now'))");
            entity.Property(e => e.SehirId).HasColumnName("SehirID");
            entity.Property(e => e.SifreHash).HasMaxLength(500);
            entity.Property(e => e.Telefon).HasMaxLength(20);
            entity.Property(e => e.YurtId).HasColumnName("YurtID");

            entity.HasOne(d => d.Sehir).WithMany(p => p.Kullanicilars)
                .HasForeignKey(d => d.SehirId)
                .HasConstraintName("FK_Kullanici_Sehir");

            entity.HasOne(d => d.Yurt).WithMany(p => p.Kullanicilars)
                .HasForeignKey(d => d.YurtId)
                .HasConstraintName("FK_Kullanici_Yurt");
        });

        modelBuilder.Entity<Restoranlar>(entity =>
        {
            entity.HasKey(e => e.RestoranId).HasName("PK__Restoran__259AB1A7AA52A535");

            entity.ToTable("Restoranlar");

            entity.Property(e => e.RestoranId).HasColumnName("RestoranID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.MinimumSepetTutar).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RestoranAdi).HasMaxLength(150);
            entity.Property(e => e.SehirId).HasColumnName("SehirID");

            entity.HasOne(d => d.Sehir).WithMany(p => p.Restoranlars)
                .HasForeignKey(d => d.SehirId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Restoran_Sehir");
        });

        modelBuilder.Entity<Sehirler>(entity =>
        {
            entity.HasKey(e => e.SehirId).HasName("PK__Sehirler__D1E8748B874CE440");

            entity.ToTable("Sehirler");

            entity.HasIndex(e => e.SehirAdi, "UQ__Sehirler__7B189908ED192CF9").IsUnique();

            entity.Property(e => e.SehirId).HasColumnName("SehirID");
            entity.Property(e => e.SehirAdi).HasMaxLength(100);
        });

        modelBuilder.Entity<Urunler>(entity =>
        {
            entity.HasKey(e => e.UrunId).HasName("PK__Urunler__623D364B658E7B49");

            entity.ToTable("Urunler");

            entity.Property(e => e.UrunId).HasColumnName("UrunID");
            entity.Property(e => e.Aciklama).HasMaxLength(500);
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.Fiyat).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.KategoriId).HasColumnName("KategoriID");
            entity.Property(e => e.RestoranId).HasColumnName("RestoranID");
            entity.Property(e => e.UrunAdi).HasMaxLength(150);

            entity.HasOne(d => d.Kategori).WithMany(p => p.Urunlers)
                .HasForeignKey(d => d.KategoriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Urun_Kategori");

            entity.HasOne(d => d.Restoran).WithMany(p => p.Urunlers)
                .HasForeignKey(d => d.RestoranId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Urun_Restoran");
        });

        modelBuilder.Entity<Yurtlar>(entity =>
        {
            entity.HasKey(e => e.YurtId).HasName("PK__Yurtlar__C93A0ED51A70B663");

            entity.ToTable("Yurtlar");

            entity.Property(e => e.YurtId).HasColumnName("YurtID");
            entity.Property(e => e.SehirId).HasColumnName("SehirID");
            entity.Property(e => e.YurtAdi).HasMaxLength(150);

            entity.HasOne(d => d.Sehir).WithMany(p => p.Yurtlars)
                .HasForeignKey(d => d.SehirId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Yurt_Sehir");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
