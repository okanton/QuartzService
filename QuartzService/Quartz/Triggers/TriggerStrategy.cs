using QuartzServiceClient;
using System.Collections.Generic;
using System.Linq;

namespace QuartzService.Quartz.Triggers
{
    public class TriggerStrategy : ITriggerStrategy
    {
        private readonly IEnumerable<IJobTrigger> jobTriggers;

        public TriggerStrategy(IEnumerable<IJobTrigger> jobTriggers) => this.jobTriggers = jobTriggers;

        public IJobTrigger Resolve(PeriodTimeTypes periodTimeType) => jobTriggers.FirstOrDefault(p => p.PeriodTimeTypes == periodTimeType);
    }
}