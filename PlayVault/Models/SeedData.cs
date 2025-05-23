using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlayVault.Data;
using PlayVault.Models;
using System;
using System.Linq;

namespace PlayVault.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new Game(
            serviceProvider.GetRequiredService<
                DbContextOptions<PlayVaultContext>>()))
        {
            // Look for any movies.
            if (context.Title.Any())
            {
                return;   // DB has been seeded
            }
            context.Id.AddRange(
                new Game
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Rating = 10,
                    Price = 7.99M
                },
                new Game
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Rating = 10,
                    Price = 8.99M
                },
                new Game
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Rating = 10,
                    Price = 9.99M
                },
                new Game
                {
                    Title = "Fallout 4",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Rating = 100,
                    Price = 3.99M
                }
            );
            context.SaveChanges();
        }
    }
}