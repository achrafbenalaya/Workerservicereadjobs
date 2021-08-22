
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.EventArgs;

namespace Workerservicereadjobs.Models
{
  public  class sqladapter
    {

        //create the jobs
        public static  void Changed(object sender, RecordChangedEventArgs<JobsTable> e)
        {
            var changedEntity = e.Entity;
            Console.WriteLine("DML operation: " + e.ChangeType);
            var jobDetail = JobBuilder.Create<Job1>().WithIdentity(changedEntity.Idtache.ToString(), "achraf")
                .UsingJobData("msg", changedEntity.themessage)
                .Build();


            //trigger when the job will start
            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("triger"+changedEntity.Idtache, "achraf")
                .StartAt(changedEntity.startat)
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(changedEntity.frequence)
                    .RepeatForever()).EndAt(changedEntity.endat)
                    .Build();

             _scheduler.ScheduleJob(job1, trigger1, _stopppingToken);






            //Console.WriteLine("ID: " + changedEntity.Id);
            //Console.WriteLine("Name: " + changedEntity.Name);
            //Console.WriteLine("Surname: " + changedEntity.Surname);
        }


        //detect chanegs happening in the db of the jobs
        public void  detectchanges( )
        {
            string _con = "Server=tcp:achrafdprdemo.database.windows.net,1433;Initial Catalog=achrafdebfordapr;Persist Security Info=False;User ID=achraf;Password=KhH7Xql5;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";            var mapper = new ModelToTableMapper<JobsTable>();
            mapper.AddMapping(c => c.Idtache, "Idtache");
            mapper.AddMapping(c => c.themessage, "themessage");
            mapper.AddMapping(c => c.frequence, "frequence");
            mapper.AddMapping(c => c.startat, "startat");
            mapper.AddMapping(c => c.endat, "endat");

            using (var dep = new SqlTableDependency<JobsTable>(_con, "JobsTable", mapper: mapper)) 
            {
                dep.OnChanged += Changed;
                dep.Start();

                Console.WriteLine("Press a key to exit");
                Console.ReadKey();

                dep.Stop();
            }
        }
    }


    }

