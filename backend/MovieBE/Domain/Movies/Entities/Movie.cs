using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.Entities
{
    public class Movie : EntityAudit
    {
        public Movie(Guid id, string title, string description, string thumbnailUrl, string author)
        {
            Id = id;
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            Author = author;
        }
        public Movie() { }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
    }
}
