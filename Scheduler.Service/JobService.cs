
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using Scheduler.Library;

namespace Scheduler.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public partial class JobService : ServiceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public JobService()
        {
            InitializeComponent();
        }
        

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("CitySourcedScheduler", "Starting CitySourced.Scheduler Service", EventLogEntryType.Information);
            JobScheduler.Start();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("CitySourcedScheduler", "Stopping CitySourced.Scheduler Service", EventLogEntryType.Information);
            JobScheduler.Stop();
        }
    }
}
