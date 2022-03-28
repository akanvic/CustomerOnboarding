using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Core.Entities
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
