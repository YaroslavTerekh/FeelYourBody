using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class CoachingVideo : BaseEntity
{
    public string FileName { get; set; }

    public string ContentFileType { get; set; }

    public string Path { get; set; }

    public Guid CoachingId { get; set; }

    [JsonIgnore]
    public Coaching Coaching { get; set; }
}
