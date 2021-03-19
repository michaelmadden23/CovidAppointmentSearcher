using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using CovidWorkerService.Pharmacies;
using System.Timers;

namespace CovidWorkerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            //MainAsync().Wait();

            Processor.RunProcessor();

            Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
            aTimer.Enabled = true;
            aTimer.Start();
        }

        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Processor.RunProcessor();

            }
            catch (Exception ex)
            {
            }
        }
    }
}
