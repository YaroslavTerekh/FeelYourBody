using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class AppFileDTO
{
    public Guid Id { get; set; } 

    public string FileName { get; set; }

    public string FileExtension { get; set; }

    public string FilePath { get; set; }
}
