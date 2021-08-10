using Quartz;

namespace QuartzService.Listeners.JobListeners
{
    public interface IAppJobListener : IJobListener
    {
        public JobTypes JobType { get; }
    }
}