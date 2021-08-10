using System.Collections.Generic;
using System.Linq;

namespace QuartzService.Listeners.JobListeners
{
    public class JobListenerResolver : IJobListenerResolver
    {
        private readonly IEnumerable<IAppJobListener> jobListeners;

        public JobListenerResolver(IEnumerable<IAppJobListener> jobListeners)
        {
            this.jobListeners = jobListeners;
        }

        public IAppJobListener Resolve(JobTypes jobType)
        {
            return jobListeners.FirstOrDefault(p => p.JobType == jobType);
        }
    }
}