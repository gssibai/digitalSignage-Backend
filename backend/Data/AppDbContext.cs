using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalSignageApi.Data
{
    public class AppDbContext : IdentityDbContext<User>
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
        //public DbSet<Schedule> Schedules { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //    // optionsBuilder.UseMySQL("server=localhost;database=digitial_signage;user=admin;password=0");
        //
        // }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //
        //     // Users Table
        //     modelBuilder.Entity<User>()
        //         .HasKey(u => u.UserId);
        //
        //     modelBuilder.Entity<User>()
        //         .Property(u => u.Role)
        //         .HasConversion<string>();
        //
        //     // Content Table
        //     modelBuilder.Entity<Content>()
        //         .HasKey(c => c.ContentId);
        //
        //     modelBuilder.Entity<Content>()
        //         .Property(c => c.Type)
        //         .HasConversion<string>();
        //
        //     modelBuilder.Entity<Content>()
        //         .HasOne(c => c.User)
        //         .WithMany(u => u.Contents)
        //         .HasForeignKey(c => c.UserId)
        //         .OnDelete(DeleteBehavior.Cascade);
        //
        //     // Screens Table
        //     modelBuilder.Entity<Screen>()
        //         .HasKey(s => s.ScreenId);
        //
        //     // modelBuilder.Entity<Screen>()
        //     //     .Property(s => s.Status)
        //     //     .HasConversion<string>();
        //
        //     modelBuilder.Entity<Screen>()
        //         .HasOne(s => s.User)
        //         .WithMany(u => u.Screens)
        //         .HasForeignKey(s => s.UserId)
        //         .OnDelete(DeleteBehavior.SetNull);
        //
        //     // Screen Tags Table
        //     modelBuilder.Entity<ScreenTag>()
        //         .HasKey(st => st.TagId);
        //
        //     modelBuilder.Entity<ScreenTag>()
        //         .HasOne(st => st.Screen)
        //         .WithMany(s => s.Tags)
        //         .HasForeignKey(st => st.ScreenId)
        //         .OnDelete(DeleteBehavior.Cascade);
        //
        //     // Screen Lists Table
        //     modelBuilder.Entity<ScreenList>()
        //         .HasKey(sl => sl.ListId);
        //
        //     modelBuilder.Entity<ScreenList>()
        //         .HasOne(sl => sl.User)
        //         .WithMany(u => u.ScreenLists)
        //         .HasForeignKey(sl => sl.UserId)
        //         .OnDelete(DeleteBehavior.Cascade);
        //
        //     // Screen List Screens Table
        //     modelBuilder.Entity<ScreenListScreen>()
        //         .HasKey(sls => sls.ListScreenId);
        //
        //     modelBuilder.Entity<ScreenListScreen>()
        //         .HasOne(sls => sls.ScreenList)
        //         .WithMany(sl => sl.ScreenListScreens)
        //         .HasForeignKey(sls => sls.ListId)
        //         .OnDelete(DeleteBehavior.Cascade);
        //
        //     modelBuilder.Entity<ScreenListScreen>()
        //         .HasOne(sls => sls.Screen)
        //         .WithMany(s => s.ScreenListScreens)
        //         .HasForeignKey(sls => sls.ScreenId)
        //         .OnDelete(DeleteBehavior.Cascade);
        //
        //     // Schedules Table
        //     modelBuilder.Entity<Schedule>()
        //         .HasKey(s => s.ScheduleId);
        //
        //     modelBuilder.Entity<Schedule>()
        //         .HasOne(s => s.Content)
        //         .WithMany(c => c.Schedules)
        //         .HasForeignKey(s => s.ContentId)
        //         .OnDelete(DeleteBehavior.Cascade);
        //
        //     modelBuilder.Entity<Schedule>()
        //         .HasOne(s => s.Screen)
        //         .WithMany(sc => sc.Schedules)
        //         .HasForeignKey(s => s.ScreenId)
        //         .OnDelete(DeleteBehavior.Cascade);
        //
        //
        // }

    }
}