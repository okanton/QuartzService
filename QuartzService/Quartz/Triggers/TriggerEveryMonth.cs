using Quartz;
using QuartzServiceClient;

namespace QuartzService.Quartz.Triggers
{
    public class TriggerEveryMonth : IJobTrigger
    {
        public PeriodTimeTypes PeriodTimeTypes => PeriodTimeTypes.Ежемесячно;

        public ITrigger GetTrigger(QuartzJobParameters quartzJobParameters, IJobDetail job)
        {
            var trigger = TriggerBuilder.Create()
                                        .StartNow()
                                        .ForJob(job)
                                        //.StartAt(quartzJobParameters.BiginDateTask ?? System.DateTime.Now)
                                        .EndAt(quartzJobParameters.EndDateTask)
                                        .WithCalendarIntervalSchedule(p => p.WithIntervalInMonths(quartzJobParameters.RepeatCount)
                                                                            .WithMisfireHandlingInstructionFireAndProceed())
                                        .Build();
            return trigger;
        }
    }
}