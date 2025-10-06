using Microsoft.EntityFrameworkCore;
using PlayVault.Data;

namespace PlayVault.Models
{
    public static class SeedDataGames
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PlayVaultContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PlayVaultContext>>()))
            {
                // Se il database contiene già giochi, esci
                if (context.Game.Any())
                {
                    return; // DB già inizializzato
                }

                context.Game.AddRange(
                    new Game
                    {
                        Image = "/uploads/placeholder.jpg",
                        Title = "Fallout 4",
                        Description = "Come unico sopravvissuto del Vault 111, devi affrontare un mondo distrutto dalla guerra nucleare. Ogni istante è una lotta per la sopravvivenza e ogni scelta spetta a te.",
                        ReleaseDate = new DateTime(2015, 11, 10),
                        Price = 10,
                        Genre = "GDR",
                        Rating = 5,
                        recensioneTxt = "Un vasto mondo post-apocalittico con una storia profonda e tante possibilità di esplorazione.",
                        Piattaforma = "PC, PS4, XBOX One"
                    },

                    new Game
                    {
                        Image = "/uploads/placeholder.jpg",
                        Title = "The Last of Us Part II",
                        Description = "Cinque anni dopo il loro viaggio pericoloso attraverso gli Stati Uniti, Ellie e Joel si sono stabiliti a Jackson. Un evento violento infrange la pace e spinge Ellie in una spietata ricerca di giustizia.",
                        ReleaseDate = new DateTime(2020, 6, 19),
                        Price = 25,
                        Genre = "Azione / Avventura",
                        Rating = 5,
                        recensioneTxt = "Una narrazione intensa e cinematografica, tra le migliori esperienze single-player.",
                        Piattaforma = "PS4, PS5"
                    },

                    new Game
                    {
                        Image = "/uploads/placeholder.jpg",
                        Title = "Fortnite",
                        Description = "Costruisci, combatti e sopravvivi in questo frenetico battle royale sempre in evoluzione. 100 giocatori si affrontano finché ne resta solo uno.",
                        ReleaseDate = new DateTime(2017, 7, 25),
                        Price = 0,
                        Genre = "Battle Royale",
                        Rating = 5,
                        recensioneTxt = "Divertente e costantemente aggiornato, perfetto per partite veloci con gli amici.",
                        Piattaforma = "PC, PS, XBOX, Switch, Mobile"
                    },

                    new Game
                    {
                        Image = "/uploads/placeholder.jpg",
                        Title = "Ghost of Tsushima",
                        Description = "Nel 1274, l’esercito mongolo invade l’isola di Tsushima. Jin Sakai, un samurai sopravvissuto, deve abbandonare le tradizioni per salvare la sua terra e diventare il Fantasma.",
                        ReleaseDate = new DateTime(2020, 7, 17),
                        Price = 35,
                        Genre = "Azione / Avventura",
                        Rating = 5,
                        recensioneTxt = "Un’esperienza visiva e narrativa eccezionale ambientata nel Giappone feudale.",
                        Piattaforma = "PS4, PS5"
                    },

                    new Game
                    {
                        Image = "/uploads/placeholder.jpg",
                        Title = "Grand Theft Auto V",
                        Description = "Vivi la storia di tre criminali in una città brulicante di vita, corruzione e caos. Un open world enorme con missioni, attività e follia senza fine.",
                        ReleaseDate = new DateTime(2013, 9, 17),
                        Price = 15,
                        Genre = "Azione / Open World",
                        Rating = 5,
                        recensioneTxt = "Un capolavoro che continua a dominare le classifiche anche anni dopo l’uscita.",
                        Piattaforma = "PC, PS, XBOX"
                    },

                    new Game
                    {
                        Image = "/uploads/placeholder.jpg",
                        Title = "Elden Ring",
                        Description = "Un vasto mondo fantasy creato da Hidetaka Miyazaki e George R. R. Martin. Esplora, combatti boss temibili e scopri i segreti dell’Interregno.",
                        ReleaseDate = new DateTime(2022, 2, 25),
                        Price = 40,
                        Genre = "GDR / Azione",
                        Rating = 5,
                        recensioneTxt = "Difficile ma appagante: un capolavoro di design, libertà e atmosfera.",
                        Piattaforma = "PC, PS, XBOX"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
