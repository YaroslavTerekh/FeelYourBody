using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class AppFile : BaseEntity
{
    public string FileName { get; set; }

    public string FileExtension { get; set; }

    public string FilePath { get; set; }
}
