using Domain.Movies.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.Commands
{
    public class AddNewMovieCommand : MovieCommand
    {
        public AddNewMovieCommand(string title, string description, string thumbnailUrl, string author)
        {
            Title = title;
            Description = description;
            ThumbnailURL = thumbnailUrl;
            Author = author;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddNewMovieCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
