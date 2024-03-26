using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Common.Entry
{
    public class EntryResponse
    {
        public bool ConcurrencyViolation { get; set; }
        public bool IsValid { get; set; }
        public string Id { get; set; }
        public ErrorModel ErrorModel { get; set; } = new ErrorModel();
    }

    public class ErrorModel
    {
        public Dictionary<string, List<string>> BusinessErrors { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> PropertyErrors { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, string> Exceptions { get; set; } = new Dictionary<string, string>();
    }
}
