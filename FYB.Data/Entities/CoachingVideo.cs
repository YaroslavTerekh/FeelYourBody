using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class CoachingVideo : BaseEntity
{
    public string FileName { get; set; }

    public string FileExtension { get; set; }

    public Guid CoachingId { get; set; }
}
