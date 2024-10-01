using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Domain.Models
{
    public class MpesaCallbackResponse
    {
        public CallbackBody Body { get; set; }

        public class CallbackBody
        {
            public StkCallback StkCallback { get; set; }
        }

        public class StkCallback
        {
            public string MerchantRequestID { get; set; }
            public string CheckoutRequestID { get; set; }
            public int ResultCode { get; set; }  // 0 = Success
            public string ResultDesc { get; set; }
            public CallbackMetadata CallbackMetadata { get; set; }
        }

        public class CallbackMetadata
        {
            public List<CallbackItem> Item { get; set; }
        }

        public class CallbackItem
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}

