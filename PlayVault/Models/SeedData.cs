using Microsoft.EntityFrameworkCore;
using PlayVault.Data;

namespace PlayVault.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PlayVaultContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PlayVaultContext>>()))
            {
                // Look for any movies.
                if (context.Game.Any())
                {
                    return;   // DB has been seeded
                }
                context.Game.AddRange(
                    new Game
                    {
                        Title = "Fallout 4",
                        Description = "Come unico sopravvissuto del Vault 111 devi affrontare un mondo distrutto dalla guerra nucleare." +
                        "Ogni istante è una lotta per la sopravvivenza e ogni scelta spetta a te." +
                        "Solo tu puoi ricostruire e definire il destino della Zona Contaminata. Benvenuto a casa.",
                        ReleaseDate = DateTime.Parse("10-11-2015"),
                        Genre = "Apocalittico, Azione, Avventura",
                        Rating = 100,
                        Price = 799                    
                    }






                );
                context.SaveChanges();
            }
        }
    }
}
