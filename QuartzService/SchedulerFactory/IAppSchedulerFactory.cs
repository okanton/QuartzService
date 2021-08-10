using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzService.SchedulerFactory
{
    public interface IAppSchedulerFactory : ISchedulerFactory
    {
        Task<IScheduler> GetScheduler(JobTypes jobType, CancellationToken cancellationToken = default);
    }
}