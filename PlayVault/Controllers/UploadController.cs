using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayVault.Data;
using PlayVault.Models;

namespace PlayVault.Controllers
{
    public class UploadController : Controller
    {
        private readonly PlayVaultContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UploadController> _logger;

        // 3 MB limit
        private const long MAX_FILE_BYTES = 3 * 1024 * 1024;

        private static readonly HashSet<string> AllowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".jpeg", ".gif", ".pdf"
        };

        public UploadController(PlayVaultContext context, IWebHostEnvironment env, ILogger<UploadController> logger)
        {
            _context = context;
            _env = env;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            // Show the upload form
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewBag.Message = "Nessun file selezionato.";
                return View();
            }

            int uploadedCount = 0;
            long totalBytes = 0;
            var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            foreach (var file in files)
            {
                try
                {
                    var ext = Path.GetExtension(file.FileName);
                    if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
                    {
                        _logger.LogWarning("File with disallowed extension attempted: {FileName}", file.FileName);
                        continue;
                    }

                    if (file.Length == 0 || file.Length > MAX_FILE_BYTES)
                    {
                        _logger.LogWarning("File size invalid for {FileName}: {Size}", file.FileName, file.Length);
                        continue;
                    }

                    // Validate signature
                    using var stream = file.OpenReadStream();
                    if (!IsValidFileSignature(stream, ext))
                    {
                        _logger.LogWarning("File signature mismatch for {FileName}", file.FileName);
                        continue;
                    }

                    // Save file with a unique name (GUID)
                    var savedName = Guid.NewGuid().ToString("N") + ext;
                    var savePath = Path.Combine(uploadsFolder, savedName);

                    // Reset stream position and save
                    stream.Position = 0;
                    using (var fs = System.IO.File.Create(savePath))
                    {
                        await stream.CopyToAsync(fs);
                    }

                    // Persist in DB
                    var img = new Image
                    {
                        FileName = savedName,
                        OriginalFileName = Path.GetFileName(file.FileName),
                        ContentType = file.ContentType,
                        Size = file.Length,
                        UploadDate = DateTime.UtcNow,
                        Path = "/uploads/" + savedName
                    };
                    _context.Images.Add(img);
                    await _context.SaveChangesAsync();

                    uploadedCount++;
                    totalBytes += file.Length;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while processing file {FileName}", file?.FileName);
                }
            }

            // Use session to store number of files uploaded in this session
            int sessionTotal = HttpContext.Session.GetInt32("SessionUploadTotal") ?? 0;
            sessionTotal += uploadedCount;
            HttpContext.Session.SetInt32("SessionUploadTotal", sessionTotal);

            ViewBag.UploadedCount = uploadedCount;
            ViewBag.TotalBytes = totalBytes;
            ViewBag.SessionTotal = sessionTotal;
            return View();
        }

        private bool IsValidFileSignature(Stream stream, string extension)
        {
            if (stream == null || !stream.CanRead) return false;
            // read first 8 bytes
            var header = new byte[8];
            stream.Position = 0;
            int read = stream.Read(header, 0, header.Length);

            // JPEG: FF D8 FF
            if (extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                if (read >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF) return true;
                // some jpegs use .jpg; check too (defensive)
                if (read >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF) return true;
                return false;
            }

            // GIF: "GIF87a" or "GIF89a"
            if (extension.Equals(".gif", StringComparison.OrdinalIgnoreCase))
            {
                var sig = System.Text.Encoding.ASCII.GetString(header, 0, Math.Min(read, 6));
                if (sig.StartsWith("GIF87a") || sig.StartsWith("GIF89a")) return true;
                return false;
            }

            // PDF: "%PDF-"
            if (extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                var sig = System.Text.Encoding.ASCII.GetString(header, 0, Math.Min(read, 5));
                if (sig.StartsWith("%PDF-")) return true;
                return false;
            }

            return false;
        }
    }
}
