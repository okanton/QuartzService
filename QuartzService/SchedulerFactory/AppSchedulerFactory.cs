using QuartzService.Listeners.JobListeners;
using Quartz;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzService.SchedulerFactory
{
    public class AppSchedulerFactory : IAppSchedulerFactory
    {
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IJobListenerResolver jobListenerResolver;

        public AppSchedulerFactory(ISchedulerFactory schedulerFactory, IJobListenerResolver jobListenerResolver)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobListenerResolver = jobListenerResolver;
        }

        public Task<IReadOnlyList<IScheduler>> GetAllSchedulers(CancellationToken cancellationToken = default)
        {
            return schedulerFactory.GetAllSchedulers(cancellationToken);
        }

        public Task<IScheduler> GetScheduler(CancellationToken cancellationToken = default)
        {
            return schedulerFactory.GetScheduler(cancellationToken);
        }

        public Task<IScheduler> GetScheduler(string schedName, CancellationToken cancellationToken = default)
        {
            return schedulerFactory.GetScheduler(schedName, cancellationToken);
        }

        public Task<IScheduler> GetScheduler(JobTypes jobType, CancellationToken cancellationToken = default)
        {
            var scheduler = GetScheduler(cancellationToken).Result;
            scheduler.ListenerManager.AddJobListener(jobListenerResolver.Resolve(jobType));
            return Task.FromResult(scheduler);
        }
    }
}