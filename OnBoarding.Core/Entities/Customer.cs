using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Core.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Otp { get; set; }
        public bool Status { get; set; }
        public int StateId { get; set; }
        public int LgaId { get; set; }
    }
}
