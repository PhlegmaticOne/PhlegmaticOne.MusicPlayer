using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("Artists");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsFavorite).HasDefaultValue(false);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
    }
}