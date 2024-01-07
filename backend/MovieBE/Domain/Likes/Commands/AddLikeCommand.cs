using Domain.Likes.Validations;
using Domain.Movies.Commands;
using Domain.Movies.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Likes.Commands
{
    public class AddLikeCommand : LikeCommand
    {
        public AddLikeCommand(Guid movieId, Guid customerId, bool isLike) 
        { 
            CustomerId = customerId;
            MovieId = movieId;
            IsLike = isLike;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddLikeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
