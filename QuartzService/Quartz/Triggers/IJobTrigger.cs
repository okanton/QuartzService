using Quartz;
using QuartzServiceClient;

namespace QuartzService.Quartz.Triggers
{
    public interface IJobTrigger
    {
        public PeriodTimeTypes PeriodTimeTypes { get; }

        public ITrigger GetTrigger(QuartzJobParameters quartzJobParameters, IJobDetail job);
    }
}