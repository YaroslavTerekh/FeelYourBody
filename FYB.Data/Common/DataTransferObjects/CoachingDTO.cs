using FYB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class CoachingDTO
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public long Price { get; set; }

    public long AccessDays { get; set; }

    public Guid CoachId { get; set; }

    public CoachDTO Coach { get; set; }

    public Guid? FoodId { get; set; }

    public FoodDTO? Food { get; set; }

    public Guid CoachingPhotoId { get; set; }

    public AppFileDTO CoachingPhoto { get; set; }

    public CoachingIcon AdditionalIcon { get; set; }

    public List<CoachingDetailsParentDTO> CoachingDetailParents { get; set; } = new();

    public List<FeedbackDTO> Feedbacks { get; set; } = new();

    public List<AppFileDTO> ExamplePhotos { get; set; } = new();

    public List<CoachingVideoDTO> Videos { get; set; } = new();
}
