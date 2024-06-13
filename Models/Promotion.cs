using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PromotionApi.Models
{
    public class Promotion
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(255)]
        public string Code { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateExpire { get; set; }

        [Required]
        public double Condition { get; set; }

        [Required]
        public float Discount { get; set; }

        public string IdMer { get; set; }
    }
}
