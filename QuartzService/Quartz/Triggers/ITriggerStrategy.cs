using QuartzService.Quartz.Triggers;
using QuartzServiceClient;

namespace QuartzService.Quartz.Triggers
{
    public interface ITriggerStrategy
    {
        public IJobTrigger Resolve(PeriodTimeTypes periodTimeType);
    }
}