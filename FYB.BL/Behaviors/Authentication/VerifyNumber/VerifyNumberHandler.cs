using FYB.BL.Exceptions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.VerifyNumber;

public class VerifyNumberHandler : IRequestHandler<VerifyNumberCommand, bool>
{
    private readonly DataContext _context;

    public VerifyNumberHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(VerifyNumberCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(t => t.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (user.PhoneNumberConfirmed) 
            throw new Exception(ErrorMessages.PhoneNumberAlreadyConfirmed);

        if (user.TemporaryCode != request.Code) return false;

        user.PhoneNumberConfirmed = true;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
