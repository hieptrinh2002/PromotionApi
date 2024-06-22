using System.ComponentModel.DataAnnotations;

namespace PromotionApi.Models.Dtos
{
    public class CreatePromotionRequestDto
    {
        [Required]
        [StringLength(255)]
        public string Code { get; set; }

        [Required]
        [CustomValidation(typeof(Promotion), nameof(ValidateDateStart))]
        public DateTime DateStart { get; set; }

        [Required]
        [CustomValidation(typeof(Promotion), nameof(ValidateDateExpire))]
        public DateTime DateExpire { get; set; }

        [CustomValidation(typeof(Promotion), nameof(ValidateQuantityAvailable))]
        public int QuantityAvailable { get; set; }

        [Required]
        public double Condition { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Discount must be between 1 and 100.")]
        public float Discount { get; set; } // % discount

        [Required]
        public string MerchantId { get; set; }

        public static ValidationResult ValidateDateStart(DateTime dateStart, ValidationContext context)
        {
            if (dateStart < DateTime.Today)
            {
                return new ValidationResult("DateStart must be greater than or equal to today.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateDateExpire(DateTime dateExpire, ValidationContext context)
        {
            var instance = context.ObjectInstance as Promotion;
            if (instance != null)
            {
                if (dateExpire <= instance.DateStart)
                {
                    return new ValidationResult("DateExpire must be greater than DateStart.");
                }
                if (dateExpire < DateTime.Today)
                {
                    return new ValidationResult("DateExpire must be greater than or equal to today.");
                }
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateQuantityAvailable(int quantityAvailable, ValidationContext context)
        {
            if (quantityAvailable >= 50000)
            {
                return new ValidationResult("QuantityAvailable must be less than 50000.");
            }
            return ValidationResult.Success;
        }
    }
}
