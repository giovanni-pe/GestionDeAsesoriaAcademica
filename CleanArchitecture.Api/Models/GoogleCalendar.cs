using System;

namespace CleanArchitecture.Api.Models
{
    public class GoogleCalendar
    {
        public string Sumary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public  DateTime Start {get;set; }
        public DateTime End { get;set; }
    }
}
