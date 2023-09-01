using FYB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class UserDTO
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Role { get; set; }

    public List<FoodDTO> Foods { get; set; } = new();

    public List<CoachingDTO> Coachings { get; set; } = new();

    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

    public List<Purchase<CoachingDTO>> CoachingPurchases { get; set; } = new();

    public List<Purchase<FoodDTO>> FoodPurchases { get; set; } = new();
}
