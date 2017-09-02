using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalc.Core.Models
{
    public class MortgageCalcResponse
    {
        public AmortizationSchedule AmortizationSchedule { get; set; } = new AmortizationSchedule();
        public decimal PrincipalAndInterest { get; set; }
        public decimal RemainingPrincipal { get; set; }
    }
}
