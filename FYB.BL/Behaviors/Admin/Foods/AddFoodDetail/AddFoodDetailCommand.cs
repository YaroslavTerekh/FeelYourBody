using MediatR;
using System.Text.Json.Serialization;

namespace FYB.BL.Behaviors.Admin.Foods.AddFoodDetail;

public class AddFoodDetailCommand : IRequest
{
    [JsonIgnore]
    public Guid FoodId { get; set; }

    public string Title { get; set; }

    public string Detail { get; set; }
}
