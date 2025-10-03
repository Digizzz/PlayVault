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

        public GamesController(PlayVaultContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ReleaseDate,Price,Genre,Rating,recensioneTxt,Piattaforma")] Game game, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(imageFile.FileName);
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                        if (!Directory.Exists(uploadPath))
                            Directory.CreateDirectory(uploadPath);

                        var filePath = Path.Combine(uploadPath, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        // Salviamo il percorso relativo da usare nell'HTML
                        game.Image = "wwwroot/uploads/" + fileName;
                    }

                    _context.Add(game);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("UPLOAD ERROR: " + ex.Message);
                    throw;
                }
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
        public async Task<IActionResult> Edit(int id, Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (game.ImageFile != null && game.ImageFile.Length > 0)
                    {
                        var ext = Path.GetExtension(game.ImageFile.FileName).ToLowerInvariant();
                        var allowedExt = new[] { ".jpeg", ".jpg", ".gif", ".png", ".pdf" };
                        if (!allowedExt.Contains(ext))
                        {
                            ModelState.AddModelError("ImageFile", "Estensione non consentita.");
                            return View(game);
                        }

                        if (game.ImageFile.Length > 3 * 1024 * 1024)
                        {
                            ModelState.AddModelError("ImageFile", "Il file supera i 3 MB.");
                            return View(game);
                        }

                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        Directory.CreateDirectory(uploads);

                        var fileName = $"{Guid.NewGuid():N}{ext}";
                        var path = Path.Combine(uploads, fileName);

                        using var fs = new FileStream(path, FileMode.Create);
                        await game.ImageFile.CopyToAsync(fs);

                        game.Image = "/uploads/" + fileName;
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Game.Any(e => e.Id == game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRORE UPLOAD (Edit): " + ex.Message);
                    ModelState.AddModelError("", "Errore durante l'upload: " + ex.Message);
                }
                _context.Update(game);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
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
