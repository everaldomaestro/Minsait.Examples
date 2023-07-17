using System.ComponentModel.DataAnnotations;

namespace Minsait.Examples.Api.ViewModels
{
    public class MinsaitTestViewModel
    {
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public bool? Active { get; set; }

        [Required]
        public decimal? Value { get; set; }
    }
}
