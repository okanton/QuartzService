using Quartz;
using QuartzServiceClient;
using System;

namespace QuartzService.Quartz.Triggers
{
    public class TriggerCreator : ITriggerCreator
    {
        private readonly ITriggerStrategy triggerStrategy;

        public TriggerCreator(ITriggerStrategy triggerStrategy)
        {
            this.triggerStrategy = triggerStrategy;
        }

        public ITrigger CreateTrigger(IJobDetail job)
        {
            if (job.JobDataMap["currentJob"] is not QuartzJob currentJob) throw new Exception("QuartzParameters not found");

            if (currentJob.QuartzParameters.StartNow)
            {
                return triggerStrategy.Resolve(currentJob.QuartzParameters.PeriodTimeType).GetTrigger(currentJob.QuartzParameters, job);
            }
            else
            {
                return TriggerWithCron.GetTriggerWithCron(currentJob.QuartzParameters, job);
            }
        }
    }
}