using System;

namespace TourismBot.Models
{
    public class LoggingInfo
    {
        public long ChatId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime Date { get; set; }
    }
}