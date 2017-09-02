using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalc.Core.Models
{
    public class AmortizationSchedule
    {
        public List<MortgagePaymentBreakdown> MonthlyPayments { get; set; } = new List<MortgagePaymentBreakdown>();
        public List<MortgagePaymentBreakdown> MonthlyCumulativePaymentAmounts { get; set; } = new List<MortgagePaymentBreakdown>();
        public List<MortgagePaymentBreakdown> YearlySummaries { get; set; } = new List<MortgagePaymentBreakdown>();
    }
}
