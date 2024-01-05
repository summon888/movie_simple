using Domain.Movies.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public abstract class MovieValidation<T> : AbstractValidator<T>
    where T : MovieCommand
    {
        protected void ValidateTitle()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Please ensure you have entered the Title")
                .Length(2, 255).WithMessage("The Title must have between 2 and 255 characters");
        }

        protected void ValidateDescription()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Please ensure you have entered the Description")
                .Length(2, 1000).WithMessage("The Description must have between 2 and 1000 characters");
        }


        protected void ValidateThumbnailURL()
        {
            RuleFor(c => c.ThumbnailURL)
                .NotEmpty().WithMessage("Please ensure you have entered the ThumbnailURL")
                .Length(2, 255).WithMessage("The ThumbnailURL must have between 2 and 255 characters");
        }

        protected void ValidateAuthor()
        {
            RuleFor(c => c.Author)
                .NotEmpty().WithMessage("Please ensure you have entered the Author")
                .Length(2, 100).WithMessage("The Author must have between 2 and 100 characters");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
