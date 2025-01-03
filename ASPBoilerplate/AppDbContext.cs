using System;
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
    public DbSet<UnrestrictedUserUserOtpEntity> UnrestrictedUserOtps { get; set; }
    public DbSet<UnrestrictedTokenEntity> UnrestrictedUserTokens { get; set; }


    // Resitrcted Users
    public DbSet<RestrictedUserEntity> RestrictedUsers { get; set; }
    public DbSet<RestrictedUserProfileEntity> RestrictedUserProfiles { get; set; }
    public DbSet<RestrictedUserUserOtpEntity> RestrictedUserOtps { get; set; }
    public DbSet<RestrictedUserTokenEntity> RestrictedUserTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSeeding((context, _) =>
        {
            BaseUserSeeder.Seed(context);
        });
}
