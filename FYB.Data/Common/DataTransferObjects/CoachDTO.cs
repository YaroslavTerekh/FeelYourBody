using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class CoachDTO
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Description { get; set; }

    public string InstagramLink { get; set; }

    public DateTime BirthDate { get; set; }

    public List<CoachDetailDTO> Details { get; set; }

    public List<AppFileDTO> Photos { get; set; }
}
