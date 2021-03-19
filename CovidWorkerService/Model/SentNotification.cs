using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CovidWorkerService.Model
{
    public class SentNotification
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string LocationID { get; set; }
        public string SentDate { get; set; }
    }
}
