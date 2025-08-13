using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockTraceSystem.Persistence.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasQueryFilter(u => u.Status == true);
            builder.HasData(_seeds);
        }

        private IEnumerable<User> _seeds
        {
            get
            {
                User User = new User()
                {
                    FirstName="Mesut",
                    LastName="Kızılay",
                    Email = "1",
                    Password="1",
                    Status = true,
                    Id=1
                };

                yield return User;
            }
        }
    }
}