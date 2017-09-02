using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalc.Core.Models
{
    public class MortgageCalcActionResult
    {
        public MortgageCalcRequest Request { get; set; }
        public MortgageCalcResponse Response { get; set; }
    }
}
