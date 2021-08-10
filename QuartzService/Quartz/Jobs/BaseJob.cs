using NLog;
using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzService.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public abstract class BaseJob : IJob
    {
        protected static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public abstract void Run(IJobExecutionContext context);

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                logger.Trace("{0}*** {1} start executing", Environment.NewLine, context.JobDetail.Key.Name);
                Run(context);
                logger.Trace("{0}*** {1} end executing", Environment.NewLine, context.JobDetail.Key.Name);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                logger.Error("{0}!!! {1} exception:{2}{3}{4}{5}", Environment.NewLine, context.JobDetail.Key.Name, Environment.NewLine, ex.ToString(), Environment.NewLine, ex.InnerException != null ? ex.InnerException.ToString() : "");
                return Task.FromException(ex);
            }
        }

        public static void LogException(string where, Exception ex)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
                logger.Error(ex, string.Format("Exception in {0}:{1}\ninner:{2}", where, ex.ToString(), ex.InnerException != null ? ex.InnerException.ToString() : string.Empty));
        }
    }
}