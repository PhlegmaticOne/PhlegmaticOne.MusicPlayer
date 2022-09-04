using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("Artists");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsFavorite).HasDefaultValue(false);
        builder.Property(p => p.Title).IsRequired().HasMaxLength(256);
    }
}