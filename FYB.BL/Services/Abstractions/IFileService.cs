using FYB.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Abstractions;

public interface IFileService
{
    public Task<AppFile> UploadFileAsync(AppFile fileModel, IFormFile file, CancellationToken cancellationToken);
    public Task DeleteFileAsync(Guid id, CancellationToken cancellationToken);
}
