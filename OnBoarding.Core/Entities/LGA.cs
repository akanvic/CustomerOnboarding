using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Core.Entities
{
    public class LGA
    {
        [Key]
        public int LgaId { get; set; }
        public int StateId { get; set; }
        public string LgaName { get; set; }
    }
}
