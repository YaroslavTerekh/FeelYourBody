using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class Coach : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Description { get; set; }

    public string InstagramLink { get; set; }

    public DateTime BirthDate { get; set; }

    public AppFile Avatar { get; set; }

    public List<Coaching> Coachings { get; set; } = new();
}
