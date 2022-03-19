using Dukkantek.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dukkantek.Infrastructure.EntityConfigurations
{
    public class UserRefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshTokens");
            builder.HasKey(RT => RT.Id);
            builder.Property(RT => RT.UserId)
                .IsRequired(true)
              .HasColumnName("UserId");
            builder.Property(RT => RT.RefreshToken)
                .IsRequired(true)
                .HasColumnName("RefreshToken")
                .HasColumnType("nvarchar(500)");
        }
    }
}
