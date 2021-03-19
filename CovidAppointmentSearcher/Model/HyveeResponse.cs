using System;
using System.Collections.Generic;
using System.Text;

namespace CovidAppointmentSearcher.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Address
    {
        public string line1 { get; set; }
        public object line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string __typename { get; set; }
    }

    public class Location
    {
        public string locationId { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public string phoneNumber { get; set; }
        public string businessCode { get; set; }
        public bool isCovidVaccineAvailable { get; set; }
        public string covidVaccineEligibilityTerms { get; set; }
        public Address address { get; set; }
        public string __typename { get; set; }
    }

    public class SearchPharmaciesNearPoint
    {
        public double distance { get; set; }
        public Location location { get; set; }
        public string __typename { get; set; }
    }

    public class Data
    {
        public List<SearchPharmaciesNearPoint> searchPharmaciesNearPoint { get; set; }
    }

    public class Execution
    {
        public List<object> resolvers { get; set; }
    }

    public class Tracing
    {
        public int version { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public long duration { get; set; }
        public Execution execution { get; set; }
    }

    public class Extensions
    {
        public Tracing tracing { get; set; }
    }

    public class HyveeResponse
    {
        public Data data { get; set; }
        public Extensions extensions { get; set; }
    }

}
