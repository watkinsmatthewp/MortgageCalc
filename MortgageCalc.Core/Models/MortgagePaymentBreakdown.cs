using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalc.Core.Models
{
    public class MortgagePaymentBreakdown
    {
        public decimal Escrow { get; set; }
        public decimal PMI { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }

        public decimal PrincipalAndInterest => Principal + Interest;
        public decimal Total => PrincipalAndInterest + Escrow + PMI;

        public MortgagePaymentBreakdown Copy()
        {
            return new MortgagePaymentBreakdown
            {
                Escrow = Escrow,
                PMI = PMI,
                Principal = Principal,
                Interest = Interest
            };
        }

        public static MortgagePaymentBreakdown operator +(MortgagePaymentBreakdown p1, MortgagePaymentBreakdown p2)
        {
            return new MortgagePaymentBreakdown
            {
                Escrow = p1.Escrow + p2.Escrow,
                PMI = p1.PMI + p2.PMI,
                Interest = p1.Interest + p2.Interest,
                Principal = p1.Principal + p2.Principal
            };
        }
    }
}
