using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.Configurations;

public class CollectionBaseConfiguration : IEntityTypeConfiguration<CollectionBase>
{
    public void Configure(EntityTypeBuilder<CollectionBase> builder)
    {
        builder.ToTable("Collections");
        builder.HasKey(x => x.Id);
        builder.Property(p => p.IsFavorite).HasDefaultValue(false);
        builder.Property(p => p.Title).IsRequired();
        builder.Property(x => x.TimePlayed).HasDefaultValue(TimeSpan.Zero);
        builder.Property(p => p.DateAdded).IsRequired();
        builder.HasOne(c => c.AlbumCover).WithOne(a => a.Album)
            .HasForeignKey<AlbumCover>(p => p.AlbumId);
    }
}