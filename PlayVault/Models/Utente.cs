using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayVault.Models
{
    public class Utente
    {
        public int Id { get; set; }

        [Display(Name = "Username")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Il campo Username è obbligatorio.")]
        public string? UserName { get; set; }

        [Display(Name = "Gioco Preferito")]
        [StringLength(60)]
        [Required(ErrorMessage = "Il campo Game è obbligatorio.")]
        public string? Game { get; set; }

        [Display(Name = "Piattaforma")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Piattaforma { get; set; }

        [Display(Name = "Posizionamento")]
        [Range(0, 9999)]
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal Positioning { get; set; }

        [Display(Name = "Data Traguardo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
    }
}