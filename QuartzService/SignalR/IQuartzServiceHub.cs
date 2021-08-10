using QuartzServiceClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzService.SignalR
{
    public interface IQuartzServiceHub
    {
        public Task ReturnStatusJob(QuartzJobStatus taskId);
    }
}