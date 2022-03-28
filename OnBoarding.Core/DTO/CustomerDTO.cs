using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Core.DTO
{
    public class CustomerDTO
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int StateId { get; set; }
        public int LgaId { get; set; }
    }
    public class OnboardedCustomersDto
    {
        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string StateName { get; set; }
        public string LgaName { get; set; }
    }
}
