using System;
using ASPBoilerplate.Modules.Chat;
using ASPBoilerplate.Modules.File;
using ASPBoilerplate.Modules.User;
using ASPBoilerplate.Modules.User.Entity;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate;

public class AppDbContext(DbContextOptions<AppDbContext> options)  : DbContext(options)
{

    public DbSet<FileEntity> Files { get; set; }

    // Unrestricted users
    public DbSet<UnrestrictedUserEntity> UnrestrictedUsers { get; set; }
    public DbSet<UnrestrictedUserProfileEntity> UnrestrictedUserProfiles { get; set; }


    // Resitrcted Users
    public DbSet<RestrictedUserEntity> RestrictedUsers { get; set; }
    public DbSet<RestrictedUserProfileEntity> RestrictedUserProfiles { get; set; }


    // Common for both users
    public DbSet<UserTokenEntity> UserTokens { get; set; }
    public DbSet<UserOtpEntity> UserOtps { get; set; } 

    // Chat Entities
    public DbSet<ChatHubConnections> ChatHubConnections { get; set; }
    public DbSet<MessageHistory> MessageHistories { get; set; }
    public DbSet<ChatInbox> ChatInboxes { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSeeding((context, _) =>
        {
            BaseUserSeeder.Seed(context);
        });
}
