using System;

// log model struct
namespace TrafficWizard.model
{
    public class LoggerModel
    {
        private string logDate;
        private string logIpAddress;
        private string logIpAddressStr;
        private string logVisitUrl;

        public LoggerModel(string logDate, string logIpAddress, string logIpAddressStr, string logVisitUrl)
        {
            this.logDate = logDate;
            this.logIpAddress = logIpAddress;
            this.logIpAddressStr = logIpAddressStr;
            this.logVisitUrl = logVisitUrl;
        }

        public string LogDate { get => logDate; set => logDate = value; }
        public string LogIpAddress { get => logIpAddress; set => logIpAddress = value; }
        public string LogIpAddressStr { get => logIpAddressStr; set => logIpAddressStr = value; }
        public string LogVisitUrl { get => logVisitUrl; set => logVisitUrl = value; }

        public override bool Equals(object obj)
        {
            return obj is LoggerModel model &&
                   logDate == model.logDate &&
                   logIpAddress == model.logIpAddress &&
                   logIpAddressStr == model.logIpAddressStr &&
                   logVisitUrl == model.logVisitUrl;
        }
    }
}
