using System;
using Microsoft.EntityFrameworkCore;
using RazorCRM.Models;

namespace RazorCRM.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions opt) : base(opt)
    {

    }

    public DbSet<User> Users;

}
