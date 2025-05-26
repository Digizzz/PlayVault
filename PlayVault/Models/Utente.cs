using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayVault.Models
{
    public class Utente
    {
        public int Id { get; set; }

        [Display(Name = "Username")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9\s.,]*$", ErrorMessage = "Il nome utente deve iniziare con una lettera maiuscola.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Il nome utente deve avere tra 3 e 60 caratteri.")]
        [Required(ErrorMessage = "Il campo Username è obbligatorio.")]
        public string? UserName { get; set; }

        [Display(Name = "Gioco Preferito")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9\s.,;()]*$", ErrorMessage = "Il campo Game deve iniziare con una lettera maiuscola.")]
        [Required(ErrorMessage = "Il campo Game è obbligatorio.")]
        [StringLength(60)]
        public string? Game { get; set; }

        [Display(Name = "Piattaforma")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s]*$", ErrorMessage = "Il campo Piattaforma deve iniziare con una lettera maiuscola.")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Piattaforma { get; set; }

        [Display(Name = "Posizionamento")]
        [Range(1, 150)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal Positioning { get; set; }

        [Display(Name = "Data Traguardo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
    }
}