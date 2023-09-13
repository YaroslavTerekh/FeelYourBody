using FYB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class CoachingDetailsParentDTO : BaseEntity
{
    public string Title { get; set; }

    public List<CoachingDetailDTO> Details { get; set; }
}
