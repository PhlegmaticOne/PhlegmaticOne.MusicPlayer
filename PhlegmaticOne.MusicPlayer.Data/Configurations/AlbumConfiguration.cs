using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("Albums");
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.OnlineUrl).IsRequired();
        builder.Property(y => y.YearReleased).IsRequired();
        builder.Property(p => p.AlbumType).HasConversion(from => from.ToString(), to => Enum.Parse<AlbumType>(to));
        builder.HasMany(s => s.Songs).WithMany(a => a.AlbumAppearances);
        builder.HasMany(a => a.Artists).WithMany(a => a.Albums);
        builder.HasMany(g => g.Genres).WithMany(a => a.Albums);
        builder.HasOne(c => c.AlbumCover).WithOne(a => a.Album)
            .HasForeignKey<AlbumCover>(p => p.AlbumId);
    }
}