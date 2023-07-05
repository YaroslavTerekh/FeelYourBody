using FYB.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Files.GetFile;

public class GetFileQuery : IRequest<AppFile>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public GetFileQuery(Guid id) => Id = id;
}
