using FYB.Data.Common.DataTransferObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.GetUser;

public class GetUserQuery : IRequest<UserDTO>
{
    public Guid UserId { get; set; }

    public GetUserQuery(Guid userId) => UserId = userId;
}
