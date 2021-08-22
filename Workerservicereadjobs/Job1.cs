using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Workerservicereadjobs
{
    public class Job1 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {

            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string jobmsg = dataMap.GetString("msg");

            await File.AppendAllLinesAsync(@"c:\Achraf\jobs.txt", new[] { DateTime.Now.ToLongTimeString(), jobmsg });
        }
    }
}
