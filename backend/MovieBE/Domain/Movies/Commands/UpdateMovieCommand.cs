using Domain.Movies.Validations;

namespace Domain.Movies.Commands
{
    public class UpdateMovieCommand : MovieCommand
    {
        public UpdateMovieCommand(Guid id, string title, string description, string thumbnailURL, string author)
        {
            Id = id;
            Title = title;
            Description = description;
            ThumbnailURL = thumbnailURL;
            Author = author;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateMovieCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
