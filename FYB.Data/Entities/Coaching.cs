using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class Coaching : BaseProduct
{
    public Guid CoachId { get; set; }

    public Coach Coach { get; set; }

    public Guid? FoodId { get; set; }

    public Food? Food { get; set; }

    public Guid CoachingPhotoId { get; set; }

    public AppFile CoachingPhoto { get; set; }

    public List<CoachingDetailsParent> CoachingDetailParents { get; set; } = new();

    public List<Feedback> Feedbacks { get; set; } = new();

    public List<AppFile> ExamplePhotos { get; set; } = new();

    public List<CoachingVideo> Videos { get; set; } = new();
}
