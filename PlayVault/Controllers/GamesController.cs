using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayVault.Data;
using PlayVault.Models;

namespace PlayVault.Controllers
{
    public class GamesController : Controller
    {
        private readonly PlayVaultContext _context;
        private readonly ILogger<GamesController> _logger;

        public GamesController(PlayVaultContext context, ILogger<GamesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Game.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ReleaseDate,Price,Genre,Rating,recensioneTxt,Piattaforma")] Game game, IFormFile? imageFile)
        {
            try
            {
                _logger.LogInformation("Inizio Create - ImageFile ricevuto: {HasFile}", imageFile != null);

                if (ModelState.IsValid)
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        _logger.LogInformation("Dimensione file: {Size} bytes", imageFile.Length);

                        // Validazione estensione
                        var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                        var allowedExt = new[] { ".jpeg", ".jpg", ".gif", ".png" };

                        if (!allowedExt.Contains(ext))
                        {
                            ModelState.AddModelError("imageFile", "Estensione non consentita. Usa .jpeg, .jpg, .gif, o .png");
                            return View(game);
                        }

                        // Validazione dimensione (3 MB)
                        if (imageFile.Length > 3 * 1024 * 1024)
                        {
                            ModelState.AddModelError("imageFile", "Il file supera i 3 MB.");
                            return View(game);
                        }

                        // Crea directory se non esiste
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                            _logger.LogInformation("Cartella uploads creata: {Path}", uploadPath);
                        }

                        // Nome file univoco
                        var fileName = $"{Guid.NewGuid():N}{ext}";
                        var filePath = Path.Combine(uploadPath, fileName);

                        _logger.LogInformation("Salvataggio file in: {Path}", filePath);

                        // Salva il file
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        // CORRETTO: Percorso relativo per il browser
                        game.Image = "/uploads/" + fileName;
                        _logger.LogInformation("File salvato con successo. Path: {ImagePath}", game.Image);
                    }

                    _context.Add(game);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Game salvato nel database con ID: {Id}", game.Id);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("ModelState non valido");
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        _logger.LogWarning("Errore validazione: {Error}", error.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERRORE durante la creazione del gioco");
                ModelState.AddModelError("", $"Errore durante il salvataggio: {ex.Message}");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ReleaseDate,Price,Genre,Rating,recensioneTxt,Piattaforma,Image")] Game game, IFormFile? imageFile)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Inizio Edit - ID: {Id}, ImageFile ricevuto: {HasFile}", id, imageFile != null);

                    // Gestione upload immagine
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        _logger.LogInformation("Dimensione file: {Size} bytes", imageFile.Length);

                        // Validazione estensione
                        var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                        var allowedExt = new[] { ".jpeg", ".jpg", ".gif", ".png" };

                        if (!allowedExt.Contains(ext))
                        {
                            ModelState.AddModelError("imageFile", "Estensione non consentita. Usa .jpeg, .jpg, .gif, o .png");
                            return View(game);
                        }

                        // Validazione dimensione
                        if (imageFile.Length > 3 * 1024 * 1024)
                        {
                            ModelState.AddModelError("imageFile", "Il file supera i 3 MB.");
                            return View(game);
                        }

                        // Crea directory
                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        Directory.CreateDirectory(uploads);

                        // Elimina vecchia immagine se esiste
                        if (!string.IsNullOrEmpty(game.Image))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", game.Image.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                                _logger.LogInformation("Vecchia immagine eliminata: {Path}", oldImagePath);
                            }
                        }

                        // Salva nuova immagine
                        var fileName = $"{Guid.NewGuid():N}{ext}";
                        var path = Path.Combine(uploads, fileName);

                        using (var fs = new FileStream(path, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fs);
                        }

                        game.Image = "/uploads/" + fileName;
                        _logger.LogInformation("Nuova immagine salvata: {ImagePath}", game.Image);
                    }

                    _context.Update(game);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Game aggiornato con successo");

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex, "Errore di concorrenza durante l'update");
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERRORE durante l'edit del gioco");
                    ModelState.AddModelError("", "Errore durante l'upload: " + ex.Message);
                }
            }
            else
            {
                _logger.LogWarning("ModelState non valido in Edit");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning("Errore validazione: {Error}", error.ErrorMessage);
                }
            }

            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game != null)
            {
                // Elimina anche il file immagine se esiste
                if (!string.IsNullOrEmpty(game.Image))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", game.Image.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                        _logger.LogInformation("Immagine eliminata: {Path}", imagePath);
                    }
                }

                _context.Game.Remove(game);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
