using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.DAL
{
    public class AppDbContext 
        : IdentityDbContext<UserEntity, RoleEntity, string,
            IdentityUserClaim<string>, UserRoleEntity, IdentityUserLogin<string>,
            IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<GameEntity> Games { get; set; }
        public DbSet<DeveloperEntity> Developers { get; set; }
        public DbSet<GameImageEntity> GameImages { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Game
            builder.Entity<GameEntity>(e =>
            {
                e.HasKey(g => g.Id);

                e.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);

                e.Property(g => g.Description)
                .HasColumnType("text");
            });

            // GameImage
            builder.Entity<GameImageEntity>(e =>
            {
                e.HasKey(gi => gi.Id);

                e.Property(gi => gi.Name)
                .IsRequired()
                .HasMaxLength(100);
            });

            // Developer
            builder.Entity<DeveloperEntity>(e =>
            {
                e.HasKey(d => d.Id);

                e.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(150);

                e.Property(d => d.Image)
                .HasMaxLength(50);
            });

            // Genre
            builder.Entity<GenreEntity>(e =>
            {
                e.HasKey(g => g.Id);

                e.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);
            });

            // Relationships
            builder.Entity<GameEntity>()
                .HasMany(g => g.Images)
                .WithOne(i => i.Game)
                .HasForeignKey(i => i.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<GameEntity>()
                .HasOne(g => g.Developer)
                .WithMany(d => d.Games)
                .HasForeignKey(g => g.DeveloperId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<GameEntity>()
                .HasMany(g => g.Genres)
                .WithMany(g => g.Games)
                .UsingEntity("GameGenres");

            // Identity
            builder.Entity<UserEntity>(e =>
            {
                e.Property(u => u.FirstName)
                .HasMaxLength(100);

                e.Property(u => u.LastName)
                .HasMaxLength(100);

                e.Property(u => u.Image)
                .HasMaxLength(50);
            });

            builder.Entity<UserEntity>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<RoleEntity>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
