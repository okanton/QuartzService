using Microsoft.AspNetCore.Mvc;
using Quartz;
using QuartzService.Quartz;
using QuartzServiceClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuartzService.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class QuartzController : ControllerBase
    {
        private readonly IQuartzManager quartz;

        public QuartzController(IQuartzManager quartz)
        {
            this.quartz = quartz;
        }

        [HttpPost]
        public async Task<ActionResult> StartJobsChain(IEnumerable<QuartzJob> parameters)
        {
            await quartz.StartJobsChain(parameters);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> UnscheduleJob()
        {
            await quartz.UnScheduleJob(new TriggerKey("trigger1", "group1"));
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuartzJob>>> GetJobs()
        {
            return Ok(await quartz.GetJobs());
        }

        [HttpGet]
        public async Task<ActionResult> PauseJob()
        {
            await quartz.PauseJob(new JobKey("job1", "group1"));
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> PauseTrigger()
        {
            await quartz.PauseTrigger(new TriggerKey("trigger1", "group1"));
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ResumeJob()
        {
            await quartz.ResumeJob(new JobKey("job1", "group1"));
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ResumeTrigger()
        {
            await quartz.ResumeTrigger(new TriggerKey("trigger1", "group1"));
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> InterruptJob()
        {
            await quartz.InterruptJob(new JobKey("job1", "group1"));
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> StartJobsChainTaskManager()
        {
            await quartz.StartJobsTaskManager();
            return Ok();
        }
    }
}