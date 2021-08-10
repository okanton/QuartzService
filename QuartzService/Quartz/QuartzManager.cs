using Quartz;
using Quartz.Impl.Matchers;
using QuartzService.Quartz.Jobs;
using QuartzService.Quartz.Triggers;
using QuartzService.SchedulerFactory;
using QuartzServiceClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzService.Quartz
{
    public class QuartzManager : IQuartzManager
    {
        private readonly IAppSchedulerFactory _schedulerFactory;
        private readonly IScheduler _scheduler;
        private readonly ITriggerCreator _triggerCreator;

        public QuartzManager(IAppSchedulerFactory schedulerFactory, ITriggerCreator triggerCreator)
        {
            _schedulerFactory = schedulerFactory;
            _triggerCreator = triggerCreator;
            _scheduler = _schedulerFactory.GetScheduler().Result;
        }

        public Task StartJobsChain(IEnumerable<QuartzJob> parameters)
        {
            try
            {
                var jobList = JobFactory.CreateJobList(parameters).ToList();

                var sched = _schedulerFactory.GetScheduler(JobTypes.Default).Result;

                for (int i = 0; i < jobList.Count; i++)
                {
                    if (i == 0)
                    {
                        var firstJobTrigger = _triggerCreator.CreateTrigger(jobList[i]);
                        sched.ScheduleJob(jobList[i], firstJobTrigger);
                    }
                    else sched.AddJob(jobList[i], true, true);
                }

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task StartJobsTaskManager()
        {
            try
            {
                //var taskId = Guid.NewGuid();

                //var parametersList = Mocks.parametersList1;

                //var jobList = JobCreator.CreateJobList(parametersList).ToList();

                //var sched = schedulerFactory.GetScheduler(JobTypes.TaskManager).Result;

                //for (int i = 0; i < jobList.Count; i++)
                //{
                //    if (i == 0)
                //    {
                //        var firstJobTrigger = JobCreator.GetFirstChainTrigger(jobList[i]);
                //        sched.ScheduleJob(jobList[i], firstJobTrigger);
                //    }
                //    else sched.AddJob(jobList[i], true, false);
                //}

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InterruptJob(JobKey jobKey)
        {
            try
            {
                var ct = new CancellationToken(true);

                await _scheduler.Interrupt(jobKey, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PauseJob(JobKey jobKey4r3)
        {
            try
            {
                var triggers = await _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
                var listJob = new List<QuartzJob>();
                foreach (var item in triggers)
                {
                    var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(item.Group));

                    foreach (var jobKey in jobKeys)
                    {
                        var j = await _scheduler.GetJobDetail(jobKey);
                        var currentJob = j.JobDataMap["currentJob"] as QuartzJob;
                        var parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(currentJob.SerializedJobParameters);
                        listJob.Add(currentJob);
                    }
                }

                var ttt = listJob.GroupBy(p => p.QuartzParameters.TaskType);

                await _scheduler.PauseJob(jobKey4r3);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PauseTrigger(TriggerKey triggerKey)
        {
            try
            {
                await _scheduler.PauseTrigger(triggerKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ResumeJob(JobKey jobKey)
        {
            try
            {
                await _scheduler.ResumeJob(jobKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ResumeTrigger(TriggerKey triggerKey)
        {
            try
            {
                await _scheduler.ResumeTrigger(triggerKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ScheduleJob(IJobDetail job, ITrigger trigger)
        {
            try
            {
                await _scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UnScheduleJob(TriggerKey triggerKey)
        {
            try
            {
                await _scheduler.UnscheduleJob(triggerKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<QuartzJob>> GetJobs()
        {
            try
            {
                var triggers = await _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
                var listJob = new List<QuartzJob>();
                foreach (var item in triggers)
                {
                    var trigger = await _scheduler.GetTrigger(item);
                    var previousTime = trigger.GetPreviousFireTimeUtc();
                    var nextTime = trigger.GetNextFireTimeUtc();
                    var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(item.Group));

                    foreach (var jobKey in jobKeys)
                    {
                        var j = await _scheduler.GetJobDetail(jobKey);
                        var currentJob = j.JobDataMap["currentJob"] as QuartzJob;

                        currentJob.QuartzParameters.NextExecutionDate = nextTime.HasValue ? nextTime.Value.LocalDateTime : null;
                        currentJob.QuartzParameters.PreviousExecutionDate = previousTime.HasValue ? previousTime.Value.LocalDateTime : null;

                        var parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(currentJob.SerializedJobParameters);
                        listJob.Add(currentJob);
                    }
                }
                return listJob;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}