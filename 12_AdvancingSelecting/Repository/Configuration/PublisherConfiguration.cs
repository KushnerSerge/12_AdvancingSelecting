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
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasData
              (
                  new Publisher
                  {
                      Id = new Guid("c0b9b67b-2408-41c1-b1fb-50e204d1820c"),
                      Name = "O'Reilly Media"
                  },
                 new Publisher
                 {
                     Id = new Guid("89f3cf78-c864-4cd4-965f-10072b9986af"),
                     Name = "Packt Publishing"
                 },
                 new Publisher
                 {
                     Id = new Guid("2c42e67e-46e6-42a6-b0b0-6ad516a861d7"),
                     Name = "Apress"
                 },
                 new Publisher
                 {
                     Id = new Guid("8e5e91de-36c1-41e0-b341-c1b356022fa2"),
                     Name = "Manning Publications"
                 },
                 new Publisher
                 {
                     Id = new Guid("f120b006-38a7-4557-a110-da6495e56d55"),
                     Name = "Microsoft Press"
                 }
              );
        }
    }
}
