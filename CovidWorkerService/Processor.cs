using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CovidWorkerService.Pharmacies;
using CovidWorkerService.Model;
using System.Threading;

namespace CovidWorkerService
{
    public static class Processor
    {
        public async static void RunProcessor()
        {
            //You'll need to edit Hyvee class. Lat Lon is hard coded to Sycamore's.
            Hyvee h = new Hyvee();
            CovidSearchDatabase database = new CovidSearchDatabase();

            var users = database.GetAllUsers();
            var sentNotifications = database.GetAllNotifications();
            var emailAccount = "";
            var password = "";

            var mailRepository = new MailManager("imap.gmail.com", 993, true, emailAccount, password);

            foreach (var user in users)
            {
                var locationAvailibilities = h.GetHyveeAppointments(user.Radius);

                foreach (var location in locationAvailibilities.data.searchPharmaciesNearPoint)
                {
                    if (location.distance < user.Radius)
                    {
                        if (location.location.isCovidVaccineAvailable && !sentNotifications.Any(p => p.LocationID == location.location.locationId && p.SentDate != null && DateTime.Parse(p.SentDate) > System.DateTime.Now.AddHours(-1)))
                        {
                            SendNotification(mailRepository, user, location, database);
                        }
                    }
                }
            }

            Thread.Sleep(5000);
        }

        private async static void SendNotification(MailManager mailRepository, User user, SearchPharmaciesNearPoint location, CovidSearchDatabase database)
        {
            var result = await mailRepository.SendVaccineAvailability(location.location, user);

            if (result)
            {
                var sentNotification = new SentNotification()
                {
                    UserID = user.UserID,
                    LocationID = location.location.locationId,
                    SentDate = DateTime.Now.ToString()
                };

                var task = Task.Run(async () => { var test = await database.AsyncInsert(sentNotification); });           
            }

        }
    }
}
