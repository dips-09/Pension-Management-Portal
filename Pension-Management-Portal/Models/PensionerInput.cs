using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pension_Management_Portal.Models
{
    public class PensionerInput
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PAN { get; set; }
        public long AadhaarNumber { get; set; }
        public PensionTypeValue PensionType { get; set; }
    }

    public enum PensionTypeValue
    {
        Self = 1,
        Family = 2
    }
}
