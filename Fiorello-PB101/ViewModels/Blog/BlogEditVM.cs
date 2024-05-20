using System.ComponentModel.DataAnnotations;

namespace Fiorello_PB101.ViewModels.Blog
{
    public class BlogEditVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(20)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public IFormFile NewImage { get; set; }
    }
}
