using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayVault.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Display(Name = "URL Immagine")]
        [StringLength(100, MinimumLength = 3)]
        [Url(ErrorMessage = "Inserisci un URL valido.")]
        [RegularExpression(@"^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|bmp))$", ErrorMessage = "L'immagine deve essere un URL che termina con .png, .jpg, .jpeg, .gif o .bmp.")]
        public string Image { get; set; }

        [Display(Name = "Titolo")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9\s.,]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Title { get; set; }

        [Display(Name = "Descrizione")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9\s.,;()]*$")]
        [StringLength(500, MinimumLength = 5)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Data di Rilascio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Prezzo")]
        [Range(1, 150)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Genere")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9\s.,;()]*$")]
        [StringLength(30)]
        [Required]
        public string? Genre { get; set; }

        [Display(Name = "Valutazione")]
        [Range(0, 110)]
        [Required]
        public int Rating { get; set; }

        [Display(Name = "Recensione")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9\s.,;()]*$")]
        [StringLength(500, MinimumLength = 5)]
        [Required]
        public string recensioneTxt { get; set; }

        [Display(Name = "Piattaforma")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Piattaforma { get; set; }
    }
}
