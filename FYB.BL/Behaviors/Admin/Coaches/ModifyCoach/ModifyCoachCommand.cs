﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coaches.ModifyCoach;

public class ModifyCoachCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public Guid AvatarId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Description { get; set; }

    public string InstagramLink { get; set; }

    public DateTime BirthDate { get; set; }

    public List<Guid> CoachingIds { get; set; }
}
