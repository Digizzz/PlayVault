using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayVault.Models
{
    public class Utente
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s.,]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? UserName { get; set; }


        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s.,;()]*$")]
        [Required]
        [StringLength(60)]
        public string? Game { get; set; }

        [Range(1, 150)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal Positioning { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Piattaforma { get; set; }


    }

}
