using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalc.Core.Models
{
    public interface IPaidAmount
    {
        decimal Escrow { get; }
        decimal PMI { get; set; }
        decimal Principal { get; }
        decimal Interest { get; }
    }
}
