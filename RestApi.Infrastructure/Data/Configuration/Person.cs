using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApi.Core.Entities;

namespace RestApi.Infrastructure.Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.ToTable("Person");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.FirstName).HasColumnType("text");

            entity.Property(x => x.LastName).HasColumnType("text");
        }
    }

}
