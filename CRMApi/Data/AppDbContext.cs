using System;
using CarelinkModels;
using Microsoft.EntityFrameworkCore;

namespace CRMApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions opts) : base(opts)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
}
