using System;
using System.ComponentModel.DataAnnotations;

namespace PlayVault.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string OriginalFileName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ContentType { get; set; } = string.Empty;

        [Required]
        public long Size { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Path { get; set; } = string.Empty;
    }
}
