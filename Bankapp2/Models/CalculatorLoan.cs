using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class CalculateLoan
    {
        public decimal BalanceAmount { get; set; }
        public decimal EMI { get; set; }
        public decimal RateOfInterest { get; set; }
        public int LoanDuration { get; set; }
        public decimal LoanAmount { get; set; }
        public CalculateLoan() { }
        public CalculateLoan(decimal RateOfInterest, decimal LoanAmount, int LoanDuration)
        {
            this.LoanDuration = LoanDuration;
            this.LoanAmount = LoanAmount;
            this.RateOfInterest = RateOfInterest;
            LoanDuration = LoanDuration / 12;
            RateOfInterest = RateOfInterest * LoanDuration;
            this.BalanceAmount = Math.Round(LoanAmount + (LoanAmount * (RateOfInterest / 100)), 2);
            this.EMI = Math.Round((this.BalanceAmount / this.LoanDuration), 2);
        }
    }
}