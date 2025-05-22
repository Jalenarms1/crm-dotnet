using System;
using CRMApi.Data;
using CRMApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMApi.Services;

public class UserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var usr = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

        Console.WriteLine(usr?.Email);

        return usr;
    }

    public async Task<User> CreateUser(User user)
    {
        var usr = _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }
    

}
