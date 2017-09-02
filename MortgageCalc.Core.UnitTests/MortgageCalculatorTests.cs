using MortgageCalc.Core.Models;
using System;
using Xunit;

namespace MortgageCalc.Core.UnitTests
{
    // Modeled from http://www.bretwhissel.net/cgi-bin/amortize
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

            // Spot check first month
            var firstMonthPayment = result.AmortizationSchedule.MonthlyPayments[0];
            Assert.Equal(381.93m, firstMonthPayment.PrincipalAndInterest);
            Assert.Equal(266.67m, firstMonthPayment.Interest);
            Assert.Equal(115.26m, firstMonthPayment.Principal);

            // Spot check first year
            var firstYearSummary = result.AmortizationSchedule.YearlySummaries[0];
            Assert.Equal(1408.8m, firstYearSummary.Principal);
            Assert.Equal(3174.36m, firstYearSummary.Interest);

            // Spot check final month
            var lastMonthPayment = result.AmortizationSchedule.MonthlyPayments[359];
            Assert.Equal(382.20m, lastMonthPayment.Principal);
            Assert.Equal(1.27m, lastMonthPayment.Interest);

            // Make sure the balances at the end are right
            var finalCumulativePaymentInfo = result.AmortizationSchedule.MonthlyCumulativePaymentAmounts[359];
            Assert.Equal(80000m, finalCumulativePaymentInfo.Principal);
            Assert.Equal(57496.34m, finalCumulativePaymentInfo.Interest);
            Assert.Equal(36000m, finalCumulativePaymentInfo.Escrow);
            Assert.Equal(0, result.RemainingPrincipal);
        }
    }
}
