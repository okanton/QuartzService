using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzService.Listeners
{
    public class SchedulerListener : ISchedulerListener
    {
        private readonly ILogger<SchedulerListener> logger;

        public SchedulerListener(ILogger<SchedulerListener> logger)
        {
            this.logger = logger;
        }

        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Job {jobDetail.Key.Name} added {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Job {jobKey.Name} deleted {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Job {jobKey.Name} interrupted {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Job {jobKey.Name} paused {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Job {jobKey.Name} resumed {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Job {trigger.JobKey.Name}/{trigger.Key.Name} scheduled {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"JobsGroup {jobGroup} paused {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"JobsGroup {jobGroup} resumed {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Job {triggerKey.Name} unscheduled {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            logger.LogError($"Scheduler has error: {msg} {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Scheduler has status InStandbyMode {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Scheduler has status SchedulerShutdown {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Scheduler has status SchedulerShuttingdown {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Scheduler has status SchedulerStarted {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Scheduler has status SchedulerStarting {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Scheduler has status SchedulingDataCleared {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Trigger {trigger.JobKey.Name}/{trigger.Key.Name} finalized {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Trigger {triggerKey.Name} paused {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Trigger {triggerKey.Name} resumed {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"TriggersGroup {triggerGroup} paused {DateTime.Now}");
            return Task.FromResult(true);
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"TriggersGroup {triggerGroup} resumed {DateTime.Now}");
            return Task.FromResult(true);
        }
    }
}