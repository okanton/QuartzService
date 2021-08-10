using Quartz;
using QuartzServiceClient;

namespace QuartzService.Quartz.Triggers
{
    public class TriggerEveryDay : IJobTrigger
    {
        public PeriodTimeTypes PeriodTimeTypes => PeriodTimeTypes.Ежедневно;

        public ITrigger GetTrigger(QuartzJobParameters quartzJobParameters, IJobDetail job)
        {
            var trigger = TriggerBuilder.Create()
                                        .StartNow()
                                        .ForJob(job)
                                        //.StartAt(quartzJobParameters.BiginDateTask ?? System.DateTime.Now)
                                        .EndAt(quartzJobParameters.EndDateTask)
                                        .WithCalendarIntervalSchedule(p => p.WithIntervalInDays(quartzJobParameters.RepeatCount)
                                                                            .WithMisfireHandlingInstructionFireAndProceed())
                                        .Build();
            return trigger;
        }
    }
}