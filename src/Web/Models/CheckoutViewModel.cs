using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CheckoutViewModel
    {
        public BasketViewModel? Basket { get; set; } = null!;

        [Required, MaxLength(180)]
        public string Street { get; set; } = null!;

        [Required, MaxLength(180)]
        public string City { get; set; } = null!;

        [MaxLength(60)]
        public string? State { get; set; }

        [Required, MaxLength(180)]
        public string Country { get; set; } = null!;

        [Required, MaxLength(180)]
        [DisplayName("Zip")]
        public string ZipCode { get; set; } = null!;

        [Required]
        [DisplayName("Name on Card")]
        public string CCHolder { get; set; } = null!;

        [Required, CreditCard]
        [DisplayName("CreditCartNumber")]
        public string CCNumber { get; set; } = null!;

        [Required]
        [DisplayName("Expiration")]
        [RegularExpression(@"^[0-9]{2}\/[0-9]{2}$", ErrorMessage = "Invalid {0}.")]
        public string CCExpiration { get; set; } = null!;

        [Required]
        [DisplayName("CVV")]
        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "Invalid {0}.")]
        public string CCCvv { get; set; } = null!;


    }
}
