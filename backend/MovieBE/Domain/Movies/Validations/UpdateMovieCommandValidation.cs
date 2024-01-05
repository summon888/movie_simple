using Domain.Movies.Commands;
using Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.Validations
{
    internal class UpdateMovieCommandValidation : MovieValidation<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidation()
        {
            ValidateId();
            ValidateTitle();
            ValidateDescription();
            ValidateThumbnailURL();
            ValidateAuthor();
        }
    }
}
