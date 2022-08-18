﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhlegmaticOne.MusicPlayer.Data.Context;

#nullable disable

namespace PhlegmaticOne.MusicPlayer.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AlbumArtist", b =>
                {
                    b.Property<Guid>("AlbumsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtistsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AlbumsId", "ArtistsId");

                    b.HasIndex("ArtistsId");

                    b.ToTable("AlbumArtist");
                });

            modelBuilder.Entity("AlbumGenre", b =>
                {
                    b.Property<Guid>("AlbumsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenresId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AlbumsId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("AlbumGenre");
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.AlbumCover", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlbumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Cover")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId")
                        .IsUnique();

                    b.ToTable("AlbumCovers", (string)null);
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.Artist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Artists", (string)null);
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.CollectionBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFavorite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Collections", (string)null);
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres", (string)null);
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.Song", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlbumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Duration")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsFavorite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LocalUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OnlineUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("TimePlayed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("time")
                        .HasDefaultValue(new TimeSpan(0, 0, 0, 0, 0));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Songs", (string)null);
                });

            modelBuilder.Entity("PlaylistSong", b =>
                {
                    b.Property<Guid>("PlaylistsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PlaylistsId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("PlaylistSong");
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.Album", b =>
                {
                    b.HasBaseType("PhlegmaticOne.MusicPlayer.Entities.CollectionBase");

                    b.Property<string>("AlbumType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OnlineUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearReleased")
                        .HasColumnType("int");

                    b.ToTable("Albums", (string)null);
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.Playlist", b =>
                {
                    b.HasBaseType("PhlegmaticOne.MusicPlayer.Entities.CollectionBase");

                    b.ToTable("Playlists", (string)null);
                });

            modelBuilder.Entity("AlbumArtist", b =>
                {
                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AlbumGenre", b =>
                {
                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.AlbumCover", b =>
                {
                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.CollectionBase", "Album")
                        .WithOne("AlbumCover")
                        .HasForeignKey("PhlegmaticOne.MusicPlayer.Entities.AlbumCover", "AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.Song", b =>
                {
                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("PlaylistSong", b =>
                {
                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.Playlist", null)
                        .WithMany()
                        .HasForeignKey("PlaylistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.Album", b =>
                {
                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.CollectionBase", null)
                        .WithOne()
                        .HasForeignKey("PhlegmaticOne.MusicPlayer.Entities.Album", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.Playlist", b =>
                {
                    b.HasOne("PhlegmaticOne.MusicPlayer.Entities.CollectionBase", null)
                        .WithOne()
                        .HasForeignKey("PhlegmaticOne.MusicPlayer.Entities.Playlist", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.CollectionBase", b =>
                {
                    b.Navigation("AlbumCover")
                        .IsRequired();
                });

            modelBuilder.Entity("PhlegmaticOne.MusicPlayer.Entities.Album", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
