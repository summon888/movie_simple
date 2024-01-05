using Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.Events
{
    public class MovieAddNewEvent : Event
    {
        public MovieAddNewEvent(Guid id, string title, string description, string thumbnailURL, string author)
        {
            Id = id;
            Title = title;
            Description = description;
            ThumbnailURL = thumbnailURL;
            Author = author;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string ThumbnailURL { get; private set; }

        public string Author { get; private set; }
    }
}
