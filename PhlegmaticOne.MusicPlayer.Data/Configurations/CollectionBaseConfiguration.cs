using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.Configurations;

public class CollectionBaseConfiguration : IEntityTypeConfiguration<CollectionBase>
{
    public void Configure(EntityTypeBuilder<CollectionBase> builder)
    {
        builder.ToTable("Collections");
        builder.HasKey(x => x.Id);
        builder.Property(p => p.IsFavorite).HasDefaultValue(false);
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.DateAdded).IsRequired();
        builder.HasMany(s => s.Songs).WithMany(a => a.AlbumAppearances);
        builder.HasMany(g => g.Genres).WithMany(a => a.Albums);
        builder.HasOne(c => c.AlbumCover).WithOne(a => a.Album)
            .HasForeignKey<AlbumCover>(p => p.AlbumId);
    }
}