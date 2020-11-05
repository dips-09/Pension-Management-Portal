using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pension_Management_Portal.Models
{
    public class ProcessPensionInput
    {
        public long AadhaarNumber { get; set; }
        public double PensionAmount { get; set; }
        public int BankCharge { get; set; }
    }
}
