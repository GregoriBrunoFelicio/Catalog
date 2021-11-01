using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Inputs
{
    public class CreateProductInput
    {
        public Guid Id = Guid.NewGuid();

        [Required(ErrorMessage = "The {0} is required.")]
        [StringLength(50, ErrorMessage = "The {0} must contain between {2} and {1} characters", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        public decimal Price { get; set; }
    }

    public class UpdateProductInput
    {
        [Required(ErrorMessage = "The {0} is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [StringLength(50, ErrorMessage = "The {0} must contain between {2} and {1} characters", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        public decimal Price { get; set; }
    }
}
