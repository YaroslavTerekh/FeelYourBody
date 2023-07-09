using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Abstractions;

public interface IHangfireJobsService
{
    public void CreateFileDeletingJob();

    public void CreateInvisibleFilesDeletingJob();
}
