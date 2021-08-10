using QuartzService.HttpService;
using QuartzService.Quartz;
using Quartz;
using QuartzServiceClient;
using System.Collections.Generic;
using System.Text.Json;

namespace QuartzService.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class RemoteJob : BaseJob
    {
        private readonly IHttpService _httpService;

        public RemoteJob(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public override void Run(IJobExecutionContext context)
        {
            logger.Info($"Job {context.JobDetail.Key.Group}/{context.JobDetail.Key.Name} Running");

            if (context.JobDetail.JobDataMap.Get("currentJob") is QuartzJob currentJob)
            {
                var result = _httpService.GetRequestResult<string>(currentJob.QuartzParameters.ExecuteServiceURL, currentJob.SerializedJobParameters);
                logger.Info(result);

                logger.Info($"Job {context.JobDetail.Key.Group}/{context.JobDetail.Key.Name} Finished");
            }
        }
    }
}