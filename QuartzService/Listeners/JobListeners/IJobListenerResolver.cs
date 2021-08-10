namespace QuartzService.Listeners.JobListeners
{
    public interface IJobListenerResolver
    {
        public IAppJobListener Resolve(JobTypes jobType);
    }
}