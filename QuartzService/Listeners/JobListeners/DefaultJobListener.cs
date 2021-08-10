using NLog;
using Quartz;
using Quartz.Impl.Matchers;
using QuartzServiceClient;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzService.Listeners.JobListeners
{
    public class DefaultJobListener : IAppJobListener
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public JobTypes JobType => JobTypes.Default;

        public string Name => "DefaultJobListener";

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
                if (context.MergedJobDataMap["nextJob"] is QuartzJob nextJob)
                {
                    var nextTrigger = TriggerBuilder.Create()
                                                    .StartNow()
                                                    .ForJob(nextJob.QuartzParameters.JobName, context.JobDetail.Key.Group)
                                                    .Build();
                    context.Scheduler.ScheduleJob(nextTrigger, cancellationToken);
                }

                return Task.CompletedTask;
            }
            else
            {
                var jobs = context.Scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(context.JobDetail.Key.Group), cancellationToken).Result;
                context.Scheduler.DeleteJobs(jobs, cancellationToken);

                return Task.FromResult(jobException);
            }
        }
    }
}