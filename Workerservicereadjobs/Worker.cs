using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Workerservicereadjobs
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private StdSchedulerFactory _schedulerFactory;
        private CancellationToken _stopppingToken;
        private IScheduler _scheduler;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartJobs();
            _stopppingToken = stoppingToken;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
            await _scheduler.Shutdown();
        }


        protected async Task StartJobs()
        {
            await NewMethod();

            IJobDetail job1 = JobBuilder.Create<Job1>()
                .WithIdentity("job1", "gtoup")
                .Build();

            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("trigger_10_sec", "group")
                .StartAt(System.DateTimeOffset.UtcNow)
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever()).EndAt(new DateTimeOffset(2021, 8, 31, 9, 0, 0, new TimeSpan(1, 0, 0)))
            .Build();

           

        }

        private async Task NewMethod(IJobDetail job1, ITrigger trigger1)
        {
            _schedulerFactory = new StdSchedulerFactory();

            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Start();
            await _scheduler.ScheduleJob(job1, trigger1, _stopppingToken);
        }
        //comunicate db lister

        //cronjob

        // Ecriture


    }
}
