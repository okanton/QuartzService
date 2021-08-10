using Quartz;
using QuartzServiceClient;
using System;

namespace QuartzService.Quartz.Triggers
{
    public static class TriggerWithCron
    {
        public static ITrigger GetTriggerWithCron(QuartzJobParameters quartzJobParameters, IJobDetail job)
        {
            var cronExpression = new CronExpressionBuilder().StartDate(quartzJobParameters.BiginDateTask)
                                                            .RepeatCount(quartzJobParameters.RepeatCount)
                                                            .RepeatPeriod(quartzJobParameters.PeriodTimeType)
                                                            .Build();

            var trigger = TriggerBuilder.Create()
                                        .ForJob(job)
                                        .WithCronSchedule(cronExpression, p => p.WithMisfireHandlingInstructionDoNothing())
                                        .StartAt(quartzJobParameters.BiginDateTask ?? DateTime.Now)
                                        .EndAt(quartzJobParameters.EndDateTask)
                                        .WithIdentity($"{quartzJobParameters.JobName}/{job.Key.Group}", job.Key.Group)
                                        .Build();
            return trigger;
        }
    }
}