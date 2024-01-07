using Domain.Likes.Commands;
using Domain.Movies.Commands;
using Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Likes.Validations
{
    public class AddLikeCommandValidation : LikeValidation<AddLikeCommand>
    {
        public AddLikeCommandValidation() { }
    }
}
