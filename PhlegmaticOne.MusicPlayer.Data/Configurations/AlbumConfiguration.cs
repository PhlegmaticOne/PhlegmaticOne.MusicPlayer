using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("Albums");
        builder.HasBaseType<CollectionBase>();
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.DateAdded).IsRequired();
        builder.Property(y => y.YearReleased).IsRequired();
        builder.Property(p => p.AlbumType).HasConversion(from => from.ToString(), to => Enum.Parse<AlbumType>(to));
        builder.HasMany(a => a.Artists).WithMany(a => a.Albums);
        builder.HasMany(x => x.Genres).WithMany(x => x.Albums);
    }
}