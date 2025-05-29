using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayVault.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Display(Name = "URL Immagine")]
        [StringLength(300)]
        [Url(ErrorMessage = "Inserisci un URL valido.")]
        public string Image { get; set; }

        [Display(Name = "Titolo")]
        [StringLength(100, MinimumLength = 2)]
        [Required]
        public string? Title { get; set; }

        [Display(Name = "Descrizione")]
        [StringLength(1000, MinimumLength = 5)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Data di Rilascio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Prezzo")]
        [Range(0, 150)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Genere")]
        [StringLength(50)]
        [Required]
        public string? Genre { get; set; }

        [Display(Name = "Valutazione")]
        [Range(0, 110)]
        [Required]
        public int Rating { get; set; }

        [Display(Name = "Recensione")]
        [StringLength(1000, MinimumLength = 5)]
        [Required]
        public string recensioneTxt { get; set; }

        [Display(Name = "Piattaforma")]
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Piattaforma { get; set; }
    }

}
