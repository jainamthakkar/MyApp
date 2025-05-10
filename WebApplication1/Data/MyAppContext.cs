using System;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

public class MyAppContext : DbContext
{
    public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
    {
    }
    public DbSet<Item> Items { get; set; }
}
