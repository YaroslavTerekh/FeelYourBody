using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class CoachingVideoDTO
{
    public Guid Id { get; set; }
    public string FileName { get; set; }

    public string ContentFileType { get; set; }

    public string FilePath { get; set; }

    public bool IsPreview { get; set; }

    public Guid CoachingId { get; set; }
}
