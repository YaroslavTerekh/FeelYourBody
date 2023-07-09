using FYB.Data.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public Role Role { get; set; }

    public List<Food> Foods { get; set; } = new();

    public List<Coaching> Coachings { get; set; } = new();

    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

    public List<Purchase<Coaching>> CoachingPurchases { get; set; } = new();

    public List<Purchase<Food>> FoodPurchases { get; set; } = new();
}
