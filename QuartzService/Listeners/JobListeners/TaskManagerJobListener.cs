using NLog;
using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzService.Listeners.JobListeners
{
    public class TaskManagerJobListener : IAppJobListener
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public JobTypes JobType => JobTypes.TaskManager;

        public string Name => "TaskManagerJobListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            logger.Trace("JobExecutionVetoed");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            logger.Trace($"JobToBeExecuted - Group:{context.JobDetail.Key.Group}/Name: {context.JobDetail.Key.Name}");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            logger.Trace($"JobWasExecuted - Group:{context.JobDetail.Key.Group}/Name: {context.JobDetail.Key.Name}");
            if (jobException is null)
            {
                if (context.MergedJobDataMap["nextJob"] is string nextJob)
                {
                    var nextTrigger = TriggerBuilder.Create().StartNow().ForJob(nextJob, context.JobDetail.Key.Group).Build();
                    context.Scheduler.ScheduleJob(nextTrigger, cancellationToken);
                }
                return Task.CompletedTask;
            }
            return Task.FromResult(jobException);
        }
    }
}