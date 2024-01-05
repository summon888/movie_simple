using Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.Commands
{
    public abstract class MovieCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Title { get; protected set; }

        public string Description { get; protected set; }

        public string ThumbnailURL { get; protected set; }

        public string Author { get; protected set; }
    }
}
