using Domain.Movies.Validations;

namespace Domain.Movies.Commands
{
    public class RemoveMovieCommand : MovieCommand
    {
        public RemoveMovieCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveMovieCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
