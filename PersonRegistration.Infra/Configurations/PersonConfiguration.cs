using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistration.Domain.Entities;

namespace PersonRegistration.Infra.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasKey(x => x.Email);
            builder.Property(x => x.Address);
            builder.Property(x => x.City);
            builder.Property(x => x.State);
            builder.Property(x => x.Phone);
            builder.Property(x => x.CreateAt);
            builder.Property(x => x.Status);
            builder.Property(x => x.Password).IsRequired();
        }
    }
}
