using Domain.Likes.Entities;
using Domain.Movies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class LikeMap : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.Property(c => c.Id)
               .HasColumnName("Id");
            builder.Property(c => c.CustomerId)
                .IsRequired();
            builder.Property(c => c.MovieId)
                .IsRequired();
            builder.Property(c => c.IsLike)
                .IsRequired();

            //builder.HasOne<Movie>(m => m.Movie)
            //.WithMany(l => l.Likes)
            //.HasForeignKey(m => m.MovieId);
        }
    }
}
