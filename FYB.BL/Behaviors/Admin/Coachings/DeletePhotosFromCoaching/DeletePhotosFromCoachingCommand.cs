using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.DeletePhotosFromCoaching;

public class DeletePhotosFromCoachingCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public List<Guid> PhotoIds { get; set; }
}
