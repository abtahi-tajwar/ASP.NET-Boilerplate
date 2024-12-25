using System;
using ASPBoilerplate.Modules.File;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate;

public class AppDbContext(DbContextOptions<AppDbContext> options)  : DbContext(options)
{
    public DbSet<FileEntity> Files { get; set; }

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
}
