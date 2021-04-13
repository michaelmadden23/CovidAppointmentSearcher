using CovidAppointmentSearcher.Pharmacies;
using System;

namespace CovidAppointmentSearcher
{
    class Program
    {
        public static void Main(string[] args)
        {
            Hyvee h = new Hyvee();
            var users = CovidSearchDatabase.GetAllUsers();
            var mailRepository = new MailManager("imap.gmail.com", 993, true, "email", "password");

            foreach (var user in users)
            {
                var locationAvailibilities = h.GetHyveeAppointments(user.Radius);

                foreach (var location in locationAvailibilities.data.searchPharmaciesNearPoint)
                {
                    if (location.distance < user.Radius)
                    {
                        if (location.location.isCovidVaccineAvailable)
                        {
                            mailRepository.SendVaccineAvailability(location.location, user);
                            Console.WriteLine("Available at " + location.location.name + " in " + location.location.address.city + ", " + location.location.address.state + ". " + location.distance + " miles away.");
                        }
                    }
                }
            }
        }
    }
}
