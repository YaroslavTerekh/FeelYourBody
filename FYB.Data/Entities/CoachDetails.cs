using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class CoachDetails : BaseEntity
{
    public string Detail { get; set; }

    public Guid CoachId { get; set; }

    public Coach Coach { get; set; }
}
