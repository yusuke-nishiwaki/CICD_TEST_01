using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Controllers.ViewModel
{
    public class TestRequestModel
    {
        [Required]
        [MaxLength(20)]
        public string AccessID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^([1-9]|[1-2][0-9]|30)$")]
        public int Length { get; set; }
    }
}