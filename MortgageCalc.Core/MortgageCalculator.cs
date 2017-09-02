using MortgageCalc.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalc.Core
{
    public static class MortgageCalculator
    {
        public static MortgageCalcResponse Calculate(MortgageCalcRequest request)
        {
            var response = new MortgageCalcResponse
            {
                RemainingPrincipal = request.HomePrice - request.DownPayment,
                PrincipalAndInterest = CalculateMonthlyMinimumPrincipalAndInterest(request)
            };

            var pmiPrincipalThreshold = .8m * request.HomePrice;
            var termsToIterate = request.StopAfterMonths < 1 ? request.TermMonths : request.StopAfterMonths;
            for (var termMonth = 0; termMonth < termsToIterate; termMonth++)
            {
                // Calculate this month's payment and add it to the list of payments in the amortization schedule (banker's rounding)
                var interest = Math.Round(response.RemainingPrincipal * request.MonthlyInterestRate, 2, MidpointRounding.ToEven);
                var thisMonthPayment = new MortgagePaymentBreakdown
                {
                    Escrow = request.Escrow,
                    PMI = response.RemainingPrincipal > pmiPrincipalThreshold ? request.PMI : 0,
                    Interest = interest,
                    Principal = Math.Min(response.PrincipalAndInterest - interest, response.RemainingPrincipal),
                };

                // Decrement the remining principal by the paid principal
                response.RemainingPrincipal -= thisMonthPayment.Principal;

                // If the last month, finish off
                if (termMonth == request.TermMonths - 1)
                {
                    thisMonthPayment.Principal += response.RemainingPrincipal;
                    response.RemainingPrincipal = 0;
                }

                // Add a new record for the monthly payment
                response.AmortizationSchedule.MonthlyPayments.Add(thisMonthPayment);

                // Add a new record for the monthly cumulative totals
                response.AmortizationSchedule.MonthlyCumulativePaymentAmounts.Add(thisMonthPayment.Copy());
                if (termMonth > 0)
                {
                    response.AmortizationSchedule.MonthlyCumulativePaymentAmounts[termMonth] += response.AmortizationSchedule.MonthlyCumulativePaymentAmounts[termMonth - 1];
                }

                // Increment the year summary for this payment
                var termYear = termMonth / 12;
                if (termMonth % 12 == 0)
                {
                    response.AmortizationSchedule.YearlySummaries.Add(new MortgagePaymentBreakdown());
                }
                response.AmortizationSchedule.YearlySummaries[termYear] += thisMonthPayment;
            }

            return response;
        }

        private static decimal CalculateMonthlyMinimumPrincipalAndInterest(MortgageCalcRequest request)
        {
            return Math.Round((request.InitialPrincipal * request.MonthlyInterestRate)
                / (1 - (decimal)Math.Pow((double)(1 + request.MonthlyInterestRate), (double)(-request.TermMonths))), 2, MidpointRounding.ToEven);
        }
    }
}
