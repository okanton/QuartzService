using Quartz;
using QuartzServiceClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuartzService.Quartz
{
    public interface IQuartzManager
    {
        public Task ScheduleJob(IJobDetail job, ITrigger trigger);

        public Task UnScheduleJob(TriggerKey triggerKey);

        public Task PauseJob(JobKey jobKey);

        public Task ResumeJob(JobKey jobKey);

        public Task InterruptJob(JobKey jobKey);

        public Task PauseTrigger(TriggerKey triggerKey);

        public Task ResumeTrigger(TriggerKey triggerKey);

        public Task StartJobsChain(IEnumerable<QuartzJob> parameters);

        public Task StartJobsTaskManager();

        public Task<IEnumerable<QuartzJob>> GetJobs();
    }
}