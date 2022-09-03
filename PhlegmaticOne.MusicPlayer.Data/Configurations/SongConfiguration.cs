using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.Configurations;

public class SongConfiguration : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.ToTable("Songs");
        builder.HasKey(x => x.Id);
        builder.Property(p => p.TimePlayed).HasDefaultValue(TimeSpan.Zero);
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.IsFavorite).HasDefaultValue(false);
        builder.Property(x => x.LocalUrl).IsRequired(false);
        builder.Property(x => x.OnlineUrl).IsRequired(false);
        builder.Property(x => x.Duration).HasConversion(from => from.Ticks, to => TimeSpan.FromTicks(to));

        builder.HasOne(x => x.Album).WithMany(x => x.Songs)
            .HasForeignKey(x => x.AlbumId);

        builder.HasMany(x => x.Playlists).WithMany(x => x.Songs);
        builder.HasMany(x => x.Artists).WithMany(x => x.Songs);
    }
}