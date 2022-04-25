using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Payroll
{
    public static class Constants
    {
        public static decimal SpouseAllowanceAmount = 620m;
        public static decimal DependancyAllowancePerChildAmount = 400m;
        public static int MaxDependantsFactor = 5;
        public static decimal MaxDependancyAllowanceAmount = 2000m;

        public static decimal DangerPayAmount = 2000m;
        
        public static decimal PensionRate = 0.02m;
        
        public static decimal PremiumHealthCareAmount = 1000m;
        public static decimal FairHealthCareAmount = 750m;
        public static decimal BasicHealthCareAmount = 500m;

        public static decimal LowSalaryThreshold = 10000m;
        public static decimal MediumSalaryThreshold = 20000m;

        public static decimal LowSalaryTaxFactor = 0.00m;
        public static decimal MediumSalaryTaxFactor = 0.02m;
        public static decimal HighSalaryTaxFactor = 0.04m;

        public static decimal TransportationAllowanceAmount = 300m;


    }
}
