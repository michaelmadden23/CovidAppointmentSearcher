using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CovidWorkerService.Model
{
    public class User
    {
        [PrimaryKey]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int ZipCode { get; set; }
        public int Radius { get; set; }

    }
}
