using MortgageCalc.Core.Models;
using System;
using Xunit;

namespace MortgageCalc.Core.UnitTests
{
    public class MortgageCalculatorTests
    {
        [Fact]
        public void CalculatorTest1()
        {
            var request = new MortgageCalcRequest
            {
                AnnualInterestRate = .04m,
                DownPayment = 20000m,
                Escrow = 100m,
                HomePrice = 100000m,
                TermMonths = 360
            };

            var result = MortgageCalculator.Calculate(request);

            Assert.Equal(381.93m, result.PrincipalAndInterest);
            Assert.Equal(30, result.AmortizationSchedule.YearlySummaries.Count);
            Assert.Equal(360, result.AmortizationSchedule.MonthlyPayments.Count);

            Assert.Equal(0, result.RemainingPrincipal);
            
        }
    }
}
