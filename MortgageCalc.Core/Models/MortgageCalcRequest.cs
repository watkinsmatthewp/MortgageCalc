using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalc.Core.Models
{
    public class MortgageCalcRequest
    {
        public decimal Escrow { get; set; }
        public decimal PMI { get; set; }
        public decimal HomePrice { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public decimal DownPayment { get; set; }
        public int TermMonths { get; set; }
        public int StopAfterMonths { get; set; }
        public decimal InitialPrincipal => HomePrice - DownPayment;
        public decimal MonthlyInterestRate => AnnualInterestRate / 12;
    }
}
