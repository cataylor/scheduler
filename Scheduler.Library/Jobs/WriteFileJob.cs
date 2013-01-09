
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Quartz;


namespace Scheduler.Library.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class WriteFileJob : BaseJob
    {
        private Timer timer;
        private string howManyWrenches;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// 
        public override void RunJob(JobExecutionContext context)
        {
            howManyWrenches = context.JobDetail.JobDataMap.Get("HowManyWrenches").ToString();
            this.SetupTimer();
        }


        private void Exec(object sender, ElapsedEventArgs e)
        {
             var message = String.Format("The time of day is {0} {1}. From Job Data Map:{2}",
                                            DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(),
                                            howManyWrenches);
             EventLog.WriteEntry("BananaSplitScheduler", "Inside Write File Job:" + message, EventLogEntryType.Information);

            if (this.isJobInterrupted)
            {
                File.AppendAllText(@"C:\Downloads\QuartzService.txt", "The freakin' job was interrupted!!");
                EventLog.WriteEntry("BananaSplitScheduler", "Inside Write File Job: Shutting job down.", EventLogEntryType.Information);
                this.timer.Stop();
            }
            else
            {
                File.AppendAllText(@"C:\Downloads\QuartzService.txt", message);
            }

            Log.Info(message);
            
        }

        private void SetupTimer()
        {
            if (this.timer == null)
            {
                EventLog.WriteEntry("CitySourcedScheduler", "Starting Timer.", EventLogEntryType.Information);
                this.timer = new Timer(120000);

            }
            this.timer.Elapsed += Exec;
            this.timer.Enabled = true; // Enable it
            this.timer.Start();
        }
    }
}
