using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApi.Core.Entities;

namespace RestApi.Infrastructure.Data.Configuration
{
    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> entity)
        {
            entity.ToTable("Quote");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Description).HasColumnType("text");

            entity.Property(x => x.PersonId).HasColumnType("integer");

            entity.HasOne(x => x.Person)
                .WithMany(x => x.Quotes)
                .HasForeignKey(x => x.PersonId);

        }
    }

}
