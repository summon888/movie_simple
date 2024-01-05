using Domain.Movies.Commands;
using Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.Validations
{
    public class AddNewMovieCommandValidation : MovieValidation<AddNewMovieCommand>
    {
        public AddNewMovieCommandValidation()
        {
            ValidateTitle();
            ValidateDescription();
            ValidateThumbnailURL();
            ValidateAuthor();
        }
    }
}
