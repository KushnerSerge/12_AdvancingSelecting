using _12_AdvancingSelecting.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_AdvancingSelecting.Repository.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData
              (
                 new Review
                 {
                     Id = new Guid("75a62660-d910-4423-948e-db20dcd9cfc6"),
                     VoterName = "Anon",
                     NumStars = 10,
                     Comment = "I like bigdata",
                     BookId = new Guid("de9fd1a4-6646-4187-a8be-7449c68b21ae")

                 },

             new Review
             {
                 Id = new Guid("04c2df0e-0866-4e88-948d-79bcb673398f"),
                 VoterName = "Anon",
                 NumStars = 2,
                 Comment = "it's complicated",
                 BookId = new Guid("642e9d6f-e623-4e28-b459-7b0f79c2c707"),

             });
        }
    }
}
