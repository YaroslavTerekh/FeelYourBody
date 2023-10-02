using FYB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class BaseFileDTO : BaseFile
{
    public int? OrderId { get; set; }
}
