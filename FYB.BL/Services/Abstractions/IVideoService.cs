using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Abstractions;

public interface IVideoService
{
    public Task UploadVideoAsync(IFormFile file, Guid coachingId, bool isPreview = false, string? name = null,
        CancellationToken cancellationToken = default);

    public Task DeleteVideoAsync(Guid id, CancellationToken cancellationToken = default);
}
