using FYB.Data.DbConnection;
using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common;

public class ContextInitializer
{
    private readonly DataContext _context;

    public ContextInitializer(DataContext context)
    {
        _context = context;
    }

    public async Task Initialize()
    {
        var admin = new User
        {
            FirstName = "Admin",
            LastName = "Shron",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            PhoneNumber = "380999999999",
            PasswordHash = "AQAAAAEAACcQAAAAELfSF7/M64t0xSDXVPLHeqNYDYS5OluWqZqN/0aVCa3jv5sz2rDlxHX9rFZ3Lbzlmg==",
            PhoneNumberConfirmed = true,
            Role = Role.Admin,
        };

        if (await _context.Users.AnyAsync(t => t.Role == Role.Admin))
        {
            return;
        }

        await _context.Users.AddAsync(admin);
        await _context.SaveChangesAsync();
    }
}