using Microsoft.EntityFrameworkCore;
using PlayVault.Data;

namespace PlayVault.Models
{
    public class SeedDataGames
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
                        Image = "https://image.api.playstation.com/vulcan/ap/rnd/202009/2502/rB3GRFvdPmaALiGt89ysflQ4.jpg",
                        Title = "Fallout 4",
                        Description = "Come unico sopravvissuto del Vault 111 devi affrontare un mondo distrutto dalla guerra nucleare." +
                        "Ogni istante è una lotta per la sopravvivenza e ogni scelta spetta a te." +
                        "Solo tu puoi ricostruire e definire il destino della Zona Contaminata. Benvenuto a casa.",
                        ReleaseDate = DateTime.Parse("10-11-2015"),
                        Price = 10,
                        Genre = "GDR",
                        Rating = 100,
                        recensioneTxt = "Provaaaa",
                        Piattaforma = "PC, PS, XBOX"
                    },
                    new Game
                    {
                        Image = "https://image.api.playstation.com/vulcan/ap/rnd/202010/2800/TSjQ4B8ArVmjPiNL2W1R8ndy.jpg",
                        Title = "The Last of Us Part II",
                        Description = "Cinque anni dopo un viaggio pericoloso, Ellie e Joel si sono stabiliti a Jackson." +
                        "Ma un evento violento interrompe la pace, e Ellie parte in cerca di giustizia." +
                        "Affronta nemici letali ed esplora un mondo devastato dalla pandemia.",
                        ReleaseDate = DateTime.Parse("19-06-2020"),
                        Price = 20,
                        Genre = "Azione/Avventura",
                        Rating = 98,
                        recensioneTxt = "Storia intensa e coinvolgente.",
                        Piattaforma = "PS"
                    },

                    new Game
                    { 
                        Image = "https://cdn1.epicgames.com/offer/fn/EN_02_2560x1440_2560x1440-51d82407c9b5283d01a735b7edc92858.jpg",
                        Title = "Fortnite",
                        Description = "Costruisci, combatti, sopravvivi. Entra in una battaglia multiplayer gratuita sempre aggiornata." +
                        "Crea la tua strategia e sopravvivi fino alla fine contro 99 altri giocatori.",
                        ReleaseDate = DateTime.Parse("25-07-2017"),
                        Price = 0,
                        Genre = "Battle Royale",
                        Rating = 85,
                        recensioneTxt = "Divertente e sempre in evoluzione.",
                        Piattaforma = "PC, PS, XBOX, Switch"
                    },

                    new Game
                    {
                        Image = "https://image.api.playstation.com/vulcan/ap/rnd/202010/0218/YKH0zYruGvYmsbOv9YYMNgqh.jpg",
                        Title = "Ghost of Tsushima",
                        Description = "Nell’anno 1274, l’esercito mongolo invade l’isola di Tsushima. Tu sei Jin Sakai, un samurai sopravvissuto." +
                        "Per proteggere la tua terra, devi infrangere le tradizioni e diventare il Fantasma.",
                        ReleaseDate = DateTime.Parse("17-07-2020"),
                        Price = 30,
                        Genre = "Azione/Avventura",
                        Rating = 95,
                        recensioneTxt = "Spettacolare e coinvolgente.",
                        Piattaforma = "PS"
                    },

                    new Game
                    {
                        Image = "https://cdn1.epicgames.com/offer/62cb0404ec4a48f9a2d40764c4fdf844/EGS_GrandTheftAutoV_RockstarGames_S1_2560x1440-8dd2ce549217bb6fbd8263493d5f29c6",
                        Title = "Grand Theft Auto V",
                        Description = "Vivi la storia di tre criminali in una città inondata dal crimine e dalla corruzione." +
                        "Sperimenta un mondo aperto enorme e dettagliato pieno di missioni, attività e caos.",
                        ReleaseDate = DateTime.Parse("17-09-2013"),
                        Price = 15,
                        Genre = "Azione",
                        Rating = 97,
                        recensioneTxt = "Classico moderno ricco di contenuti.",
                        Piattaforma = "PC, PS, XBOX"
                    },

                    new Game
                    {
                        Image = "https://cdn1.epicgames.com/spt-assets/25aa5bfcfb304dd69c40b8bbd182f7e1/elden-ring-1bhlv.png",
                        Title = "Elden Ring",
                        Description = "Affronta un mondo epico e oscuro creato da Hidetaka Miyazaki e George R. R. Martin." +
                        "Scopri segreti, combatti boss temibili e plasma il tuo destino nell’Interregno.",
                        ReleaseDate = DateTime.Parse("25-02-2022"),
                        Price = 40,
                        Genre = "GDR/Azione",
                        Rating = 96,
                        recensioneTxt = "Profondo, difficile e gratificante.",
                        Piattaforma = "PC, PS, XBOX"
                    }


                );
                context.SaveChanges();
            }
        }
    }
}
