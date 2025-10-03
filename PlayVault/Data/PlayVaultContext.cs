using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayVault.Models;

namespace PlayVault.Data
{
    public class PlayVaultContext : DbContext
    {
        public PlayVaultContext (DbContextOptions<PlayVaultContext> options)
            : base(options){}

        public DbSet<PlayVault.Models.Game> Game { get; set; } = default!;
        public DbSet<PlayVault.Models.Utente> Utente { get; set; } = default!;
        public DbSet<PlayVault.Models.Image> Images { get; set; }
    }
}
