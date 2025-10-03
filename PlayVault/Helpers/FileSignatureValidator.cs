using Microsoft.AspNetCore.Http;
using System.Text;

namespace PlayVault.Helpers
{
    public static class FileSignatureValidator
    {
        private static readonly Dictionary<string, List<byte[]>> _fileSignatures = new()
        {
            { ".jpeg", new List<byte[]>{ new byte[]{ 0xFF,0xD8,0xFF } } },
            { ".jpg", new List<byte[]>{ new byte[]{ 0xFF,0xD8,0xFF } } },
            { ".gif", new List<byte[]>{ Encoding.ASCII.GetBytes("GIF87a"), Encoding.ASCII.GetBytes("GIF89a") } },
            { ".pdf", new List<byte[]>{ new byte[]{ 0x25,0x50,0x44,0x46 } } }
        };

        public static async Task<bool> IsValidFile(IFormFile file, string extension)
        {
            if (file == null) return false;
            extension = extension.ToLowerInvariant();
            if (!_fileSignatures.ContainsKey(extension)) return false;

            using var stream = file.OpenReadStream();
            var maxLen = _fileSignatures[extension].Max(s => s.Length);
            var header = new byte[maxLen];
            var read = await stream.ReadAsync(header, 0, maxLen);
            foreach (var sig in _fileSignatures[extension])
            {
                if (read >= sig.Length)
                {
                    bool ok = true;
                    for (int i = 0; i < sig.Length; i++)
                    {
                        if (header[i] != sig[i]) { ok = false; break; }
                    }
                    if (ok) return true;
                }
            }
            return false;
        }
    }
}
