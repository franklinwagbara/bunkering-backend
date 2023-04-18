using System.Collections.Generic;

namespace Bunkering.Core.Data
{
    public class RemitaResponse
    {
        public object statusMessage { get; set; }
        public string appId { get; set; }
        public string status { get; set; }
        public string rrr { get; set; }
        public object transactiontime { get; set; }
        public string transactionId { get; set; }
        public List<object> requiredDocs { get; set; }

    }
}