using Quartz;

namespace QuartzService.Quartz.Triggers
{
    public interface ITriggerCreator
    {
        public ITrigger CreateTrigger(IJobDetail job);
    }
}