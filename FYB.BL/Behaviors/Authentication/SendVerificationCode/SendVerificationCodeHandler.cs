using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.BL.Settings.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FYB.BL.Behaviors.Authentication.SendVerificationCode;

public class SendVerificationCodeHandler : IRequestHandler<SendVerificationCodeCommand>
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly ITwilioSettings _twilioSettings;
    private readonly IPhoneNumberService _phoneNumberService;

    public SendVerificationCodeHandler
    (
        DataContext context, 
        UserManager<User> userManager, 
        ITwilioSettings twilioSettings,
        IPhoneNumberService phoneNumberService
    )
    {
        _context = context;
        _userManager = userManager;
        _twilioSettings = twilioSettings;
        _phoneNumberService = phoneNumberService;
    }

    public async Task<Unit> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(t => t.PhoneNumber == _phoneNumberService.FormatPhoneNumber(request.PhoneNumber), cancellationToken);

        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (user.PhoneNumberConfirmed)
            throw new Exception(ErrorMessages.PhoneNumberAlreadyConfirmed);

        var code = new Random().Next(100000, 999999);
        user.TemporaryCode = code;

        await _context.SaveChangesAsync(cancellationToken);

        BackgroundJob.Schedule(() => RemoveCode(user.Id, cancellationToken), TimeSpan.FromMinutes(10));

        TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

        var message = MessageResource.Create(
            body: ValidationMessages.VerificationCodeInfo(user.TemporaryCode),
            from: new PhoneNumber(_twilioSettings.FromPhoneNumber),
            to: new PhoneNumber(user.PhoneNumber)
        );

        return Unit.Value;
    }

    public async Task RemoveCode(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (user.TemporaryCode is not null)
        {
            user.TemporaryCode = null;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
