using Quartz;
using QuartzServiceClient;

namespace QuartzService.Quartz.Triggers
{
    public class TriggerSingle : IJobTrigger
    {
        public PeriodTimeTypes PeriodTimeTypes => PeriodTimeTypes.Однократно;

        public ITrigger GetTrigger(QuartzJobParameters quartzJobParameters, IJobDetail job)
        {
            var trigger = TriggerBuilder.Create()
                                        .StartNow()
                                        .ForJob(job)
                                        .WithSimpleSchedule()
                                        .Build();
            return trigger;
        }
    }
}