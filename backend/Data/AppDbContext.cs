using backend.Data.Models;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalSignageApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<ScreenTag> ScreenTags { get; set; }
        public DbSet<ScreenList> ScreenLists { get; set; }
        public DbSet<ScreenListScreen> ScreenListScreens { get; set; }
        public DbSet<AssetAssignment> AssetAssignments { get; set; }

        public DbSet<UserScreen> UserScreens { get; set; }
       

    }
}