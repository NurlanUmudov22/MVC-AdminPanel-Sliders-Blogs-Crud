using System.ComponentModel.DataAnnotations;

namespace Fiorello_PB101.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
    