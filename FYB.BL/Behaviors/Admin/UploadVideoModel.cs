using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin;

public class UploadVideoModel
{
    public IFormFile File { get; set; }
    public string? FileName { get; set; }
    public bool IsPreview { get; set; }
}
