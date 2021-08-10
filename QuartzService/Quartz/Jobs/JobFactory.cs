using Quartz;
using QuartzServiceClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuartzService.Quartz.Jobs
{
    public static class JobFactory
    {
        public static IEnumerable<IJobDetail> CreateJobList(IEnumerable<QuartzJob> jobParameters)
        {
            for (int i = 0; i < jobParameters.Count(); i++)
            {
                var job = CreateJob(jobParameters.ElementAtOrDefault(i));
                if (job is not null)
                {
                    var nextJob = jobParameters.ElementAtOrDefault(i + 1);
                    if (nextJob is not null)
                    {
                        job.JobDataMap.Put("nextJob", nextJob);
                    }
                    yield return job;
                }
            }
        }

        private static IJobDetail CreateJob(QuartzJob job)
        {
            if (!string.IsNullOrEmpty(job.QuartzParameters.ExecuteServiceURL))
            {
                var newJob = JobBuilder.Create<RemoteJob>()
                                    .WithIdentity(job.QuartzParameters.JobName, job.QuartzParameters.TaskName)
                                    .StoreDurably(!job.QuartzParameters.StartNow)
                                    .RequestRecovery(job.QuartzParameters.RequestRecovery)
                                    .Build();
                newJob.JobDataMap.Put("currentJob", job);
                return newJob;
            }
            throw new Exception("ExecuteServiceURL is empty");
        }
    }
}