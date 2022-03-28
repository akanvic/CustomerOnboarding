using System;

namespace OnBoarding.Core.Responses
{
    public class BankResponse
    {
        public BankDetails[] result { get; set; }
        public object errorMessage { get; set; }
        public object errorMessages { get; set; }
        public bool hasError { get; set; }
        public DateTime timeGenerated { get; set; }
    }

    public class BankDetails
    {
        public string bankName { get; set; }
        public string bankCode { get; set; }
    }
}
