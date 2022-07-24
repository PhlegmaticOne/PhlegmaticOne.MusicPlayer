using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.Configurations;

public class AlbumCoverConfiguration : IEntityTypeConfiguration<AlbumCover>
{
    public void Configure(EntityTypeBuilder<AlbumCover> builder)
    {
        builder.ToTable("AlbumCovers");
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Cover).HasConversion(
            from => (byte[])new ImageConverter().ConvertTo(from, typeof(byte[]))!,
            to => (Bitmap)Image.FromStream(new MemoryStream(to))!).IsRequired();
    }
}
