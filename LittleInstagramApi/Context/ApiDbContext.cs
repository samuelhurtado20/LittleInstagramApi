using LittleInstagramApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleInstagramApi.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PictureEntity>()
                .HasOne(p => p.User)
                .WithMany(b => b.Pictures);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PictureEntity> Pictures { get; set; }
    }
}
