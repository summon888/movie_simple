using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class MovieViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Title is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Description is Required")]
        [MinLength(2)]
        [MaxLength(1000)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The ThumbnailURL is Required")]
        [MinLength(2)]
        [MaxLength(255)]
        [DisplayName("ThumbnailURL")]
        public string ThumbnailURL { get; set; }

        [Required(ErrorMessage = "The Author is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Author")]
        public string Author { get; set; }
    }
}
