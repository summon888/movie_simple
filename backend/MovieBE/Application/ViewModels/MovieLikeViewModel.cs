using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class MovieLikeViewModel
    {
        public Guid MovieId { get; set; }

        [JsonIgnore]
        public Guid CustomerId { get; set; }
        public bool IsLike { get; set; }
    }
}
