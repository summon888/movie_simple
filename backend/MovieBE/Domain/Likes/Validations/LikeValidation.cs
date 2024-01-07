using Domain.Likes.Commands;
using Domain.Movies.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Likes.Validations
{
    public abstract class LikeValidation<T> : AbstractValidator<T>
    where T : LikeCommand
    {

    }
}
