using System;
using ASPBoilerplate.Modules.File;
using ASPBoilerplate.Modules.User;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate;

public class AppDbContext(DbContextOptions<AppDbContext> options)  : DbContext(options)
{
    public DbSet<FileEntity> Files { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserProfileEntity> UserProfiles { get; set; }
    public DbSet<UserTokenEntity> UserTokens { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<FileEntity>().HasData([
    //         new FileEntity
    //         {
    //             Id = "1",
    //             Name = "File 1",
    //             Location = "file1.txt",
    //             Storage = FILE_STORAGE_TYPES.LOCAL,
    //             IsUsed = true,
    //             CreatedAt = DateTime.Now,
    //             UpdatedAt = DateTime.Now
    //         },
    //         new FileEntity
    //         {
    //             Id = "2",
    //             Name = "File 2",
    //             Location = "file2.txt",
    //             Storage = FILE_STORAGE_TYPES.LOCAL,
    //             IsUsed = true,
    //             CreatedAt = DateTime.Now,
    //             UpdatedAt = DateTime.Now
    //         }
    //     ]);
    // }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSeeding((context, _) =>
        {
            BaseUserSeeder.Seed(context);
        });
}
