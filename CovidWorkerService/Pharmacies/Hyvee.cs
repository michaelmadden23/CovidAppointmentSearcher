using CovidWorkerService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CovidWorkerService.Pharmacies
{
    public class Hyvee
    {
        public class HyveeVariables
        {
            public int radius { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
        }
        public class HyveeParams
        {
            public string operationName { get; set; }
            public HyveeVariables variables { get; set; }
            public string query { get; set; }
        }
        public HyveeResponse GetHyveeAppointments(int radius)
        {
            string hyveeRequestString = String.Format("https://www.hy-vee.com/my-pharmacy/api/graphql");

            var request = (HttpWebRequest)HttpWebRequest.Create(hyveeRequestString);
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            //request.Headers.Add(HttpRequestHeader.Connection, "keep-alive");
            //request.Headers.Add(HttpRequestHeader.Accept, "*/*");
            //request.Headers.Add(HttpRequestHeader.UserAgent, "PostmanRuntime/7.26.10");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                var requestHeaders = new HyveeParams()
                {
                    operationName = "SearchPharmaciesNearPointWithCovidVaccineAvailability",
                    variables = new HyveeVariables()
                    {
                        radius = radius,
                        latitude = 41.9889,
                        longitude = -88.6868
                    },
                    query = "query SearchPharmaciesNearPointWithCovidVaccineAvailability($latitude: Float!, $longitude: Float!, $radius: Int! = 10) {\n  searchPharmaciesNearPoint(latitude: $latitude, longitude: $longitude, radius: $radius) {\n    distance\n    location {\n      locationId\n      name\n      nickname\n      phoneNumber\n      businessCode\n      isCovidVaccineAvailable\n      covidVaccineEligibilityTerms\n      address {\n        line1\n        line2\n        city\n        state\n        zip\n        latitude\n        longitude\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n"

                };
                string payload = JsonConvert.SerializeObject(requestHeaders);
                streamWriter.Write(payload);

            }


            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);

                string json = reader.ReadToEnd();

                var results = JsonConvert.DeserializeObject<HyveeResponse>(json);

                return results;
            }

        }

    }
}
