using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.DAL.Initializer
{
    public static class Seeder
    {
        public static async Task SeedAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.MigrateAsync();

            // Genres
            var genres = new List<GenreEntity>();
            if (!dbContext.Genres.Any())
            {
                genres = new List<GenreEntity>
                {
                    new GenreEntity {Name = "Спорт"},
                    new GenreEntity {Name = "Стратегії"},
                    new GenreEntity {Name = "Жахи"},
                    new GenreEntity {Name = "Казуальні"},
                    new GenreEntity {Name = "Перегони"},
                    new GenreEntity {Name = "Симулятори"},
                    new GenreEntity {Name = "Виживання"},
                    new GenreEntity {Name = "Космос"},
                    new GenreEntity {Name = "Аніме"},
                    new GenreEntity {Name = "Шутери"}
                };

                await dbContext.Genres.AddRangeAsync(genres);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                genres = await dbContext.Genres
                    .AsNoTracking()
                    .ToListAsync();
            }

            // Developers and Games
            if (!dbContext.Developers.Any())
            {
                var developers = new List<DeveloperEntity>
                {
                    new DeveloperEntity
                    {
                        Name = "Rockstar Games",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "Grand Theft Auto V", Price = 599, Genres = new List<GenreEntity> { genres[9], genres[4] } },
                            new GameEntity { Name = "Red Dead Redemption 2", Price = 899, Genres = new List<GenreEntity> { genres[9], genres[5] } },
                            new GameEntity { Name = "Max Payne 3", Price = 299, Genres = new List<GenreEntity> { genres[9] } },
                            new GameEntity { Name = "Bully", Price = 149, Genres = new List<GenreEntity> { genres[3] } },
                            new GameEntity { Name = "L.A. Noire", Price = 399, Genres = new List<GenreEntity> { genres[5] } }
                        }
                    },
                    new DeveloperEntity
                    {
                        Name = "Ubisoft",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "Assassin's Creed", Price = 450, Genres = new List<GenreEntity> { genres[1] } },
                            new GameEntity { Name = "Far Cry 6", Price = 1200, Genres = new List<GenreEntity> { genres[9] } },
                            new GameEntity { Name = "The Crew", Price = 600, Genres = new List<GenreEntity> { genres[4] } },
                            new GameEntity { Name = "Rainbow Six Siege", Price = 350, Genres = new List<GenreEntity> { genres[9], genres[1] } },
                            new GameEntity { Name = "Steep", Price = 250, Genres = new List<GenreEntity> { genres[0], genres[5] } }
                        }
                    },
                    new DeveloperEntity
                    {
                        Name = "Electronic Arts",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "FIFA 24", Price = 1500, Genres = new List<GenreEntity> { genres[0] } },
                            new GameEntity { Name = "Need for Speed", Price = 800, Genres = new List<GenreEntity> { genres[4] } },
                            new GameEntity { Name = "The Sims 4", Price = 0, Genres = new List<GenreEntity> { genres[5], genres[3] } },
                            new GameEntity { Name = "Dead Space", Price = 1100, Genres = new List<GenreEntity> { genres[2], genres[7] } },
                            new GameEntity { Name = "Battlefield 2042", Price = 1300, Genres = new List<GenreEntity> { genres[9] } }
                        }
                    },
                    new DeveloperEntity
                    {
                        Name = "CD Projekt RED",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "The Witcher 3", Price = 600, Genres = new List<GenreEntity> { genres[1] } },
                            new GameEntity { Name = "Cyberpunk 2077", Price = 900, Genres = new List<GenreEntity> { genres[9], genres[7] } },
                            new GameEntity { Name = "Gwent", Price = 0, Genres = new List<GenreEntity> { genres[1], genres[3] } },
                            new GameEntity { Name = "Thronebreaker", Price = 300, Genres = new List<GenreEntity> { genres[1] } },
                            new GameEntity { Name = "The Witcher 2", Price = 200, Genres = new List<GenreEntity> { genres[1] } }
                        }
                    },
                    new DeveloperEntity
                    {
                        Name = "Capcom",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "Resident Evil Village", Price = 850, Genres = new List<GenreEntity> { genres[2] } },
                            new GameEntity { Name = "Street Fighter 6", Price = 1100, Genres = new List<GenreEntity> { genres[0] } },
                            new GameEntity { Name = "Monster Hunter: World", Price = 700, Genres = new List<GenreEntity> { genres[5], genres[8] } },
                            new GameEntity { Name = "Devil May Cry 5", Price = 500, Genres = new List<GenreEntity> { genres[8] } },
                            new GameEntity { Name = "Exoprimal", Price = 950, Genres = new List<GenreEntity> { genres[9], genres[7] } }
                        }
                    },
                    new DeveloperEntity
                    {
                        Name = "Bethesda",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "Starfield", Price = 1400, Genres = new List<GenreEntity> { genres[7], genres[5] } },
                            new GameEntity { Name = "Skyrim", Price = 550, Genres = new List<GenreEntity> { genres[1] } },
                            new GameEntity { Name = "DOOM Eternal", Price = 750, Genres = new List<GenreEntity> { genres[9], genres[2] } },
                            new GameEntity { Name = "Fallout 4", Price = 400, Genres = new List<GenreEntity> { genres[6], genres[9] } },
                            new GameEntity { Name = "Prey", Price = 350, Genres = new List<GenreEntity> { genres[7], genres[2] } }
                        }
                    },
                    new DeveloperEntity
                    {
                        Name = "Sega",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "Sonic Frontiers", Price = 800, Genres = new List<GenreEntity> { genres[3], genres[4] } },
                            new GameEntity { Name = "Total War: Warhammer III", Price = 1200, Genres = new List<GenreEntity> { genres[1] } },
                            new GameEntity { Name = "Yakuza: Like a Dragon", Price = 700, Genres = new List<GenreEntity> { genres[8], genres[1] } },
                            new GameEntity { Name = "Persona 5 Royal", Price = 1000, Genres = new List<GenreEntity> { genres[8] } },
                            new GameEntity { Name = "Two Point Hospital", Price = 400, Genres = new List<GenreEntity> { genres[5], genres[3] } }
                        }
                    },
                    new DeveloperEntity
                    {
                        Name = "FromSoftware",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "Elden Ring", Price = 1300, Genres = new List<GenreEntity> { genres[1], genres[2] } },
                            new GameEntity { Name = "Sekiro", Price = 800, Genres = new List<GenreEntity> { genres[1] } },
                            new GameEntity { Name = "Dark Souls III", Price = 600, Genres = new List<GenreEntity> { genres[2], genres[1] } },
                            new GameEntity { Name = "Armored Core VI", Price = 1100, Genres = new List<GenreEntity> { genres[5], genres[7] } },
                            new GameEntity { Name = "Bloodborne", Price = 500, Genres = new List<GenreEntity> { genres[2] } }
                        }
                    },
                    new DeveloperEntity
                    {
                        Name = "Square Enix",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "Final Fantasy XVI", Price = 1500, Genres = new List<GenreEntity> { genres[8], genres[1] } },
                            new GameEntity { Name = "NieR: Automata", Price = 650, Genres = new List<GenreEntity> { genres[8], genres[7] } },
                            new GameEntity { Name = "Outriders", Price = 500, Genres = new List<GenreEntity> { genres[9], genres[7] } },
                            new GameEntity { Name = "Just Cause 4", Price = 300, Genres = new List<GenreEntity> { genres[9] } },
                            new GameEntity { Name = "Life is Strange", Price = 250, Genres = new List<GenreEntity> { genres[3], genres[5] } }
                        }
                    },
                    new DeveloperEntity
                    {
                        Name = "Valve",
                        Games = new List<GameEntity>
                        {
                            new GameEntity { Name = "Half-Life: Alyx", Price = 800, Genres = new List<GenreEntity> { genres[9], genres[2] } },
                            new GameEntity { Name = "Counter-Strike 2", Price = 0, Genres = new List<GenreEntity> { genres[9] } },
                            new GameEntity { Name = "Dota 2", Price = 0, Genres = new List<GenreEntity> { genres[1] } },
                            new GameEntity { Name = "Portal 2", Price = 200, Genres = new List<GenreEntity> { genres[1], genres[3] } },
                            new GameEntity { Name = "Left 4 Dead 2", Price = 200, Genres = new List<GenreEntity> { genres[2], genres[9], genres[6] } }
                        }
                    }
                };

                await dbContext.Developers.AddRangeAsync(developers);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
