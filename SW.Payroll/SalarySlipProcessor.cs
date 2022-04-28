using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Payroll
{
    public class SalarySlipProcessor
    {
        private readonly IZoneService zoneService;

        public SalarySlipProcessor(IZoneService zoneService)
        {
            this.zoneService = zoneService;
        }
         
        public decimal CalculateBasicSalary(Employee employee)
        { 
            if(employee == null)
                throw new ArgumentNullException(nameof(employee));

            return employee.Wage * employee.WorkingDays;
        }
      
        public decimal CalculateSpouseAllowance(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            return employee.IsMarried ? Constants.SpouseAllowanceAmount : 0m;
        }
      
        public decimal CalculateDependancyAllowance(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (employee.TotalDependancies < 0)
                throw new ArgumentOutOfRangeException($"invalid {nameof(employee.TotalDependancies)}");


            if (employee.TotalDependancies > Constants.MaxDependantsFactor)
                return Constants.MaxDependancyAllowanceAmount;

            if (employee.TotalDependancies == 0)
            {
                return 0m;
            }
            else
            {
                return employee.TotalDependancies * Constants.DependancyAllowancePerChildAmount;
            }
        }
     
        public decimal CalculatePension(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (!employee.HasPensionPlan)
                return 0m;

            return Constants.PensionRate * CalculateBasicSalary(employee);
        }
     
        public decimal CalculateTax(Employee employee)
        {
            var basicSalary = CalculateBasicSalary(employee);
            if (basicSalary >= Constants.MediumSalaryThreshold)
                return basicSalary * Constants.HighSalaryTaxFactor;
            else if (basicSalary >= Constants.LowSalaryThreshold)
                return basicSalary * Constants.MediumSalaryTaxFactor;
            else
                return basicSalary * Constants.LowSalaryTaxFactor;
        }
    
        public decimal CalculateDangerPay(Employee employee)
        {

            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (employee.IsDanger)
                return Constants.DangerPayAmount; 
            
            var isDangerZone = zoneService.IsDangerZone(employee.DutyStation);

            if (isDangerZone)
                return Constants.DangerPayAmount;

            return 0m;
        }
    
        public decimal CalculateHealthInsurance(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (!employee.HealthInsurancePackage.HasValue)
                return 0m;

            switch (employee.HealthInsurancePackage.Value)
            { 
                case HealthInsurancePackage.Basic:
                    return Constants.BasicHealthCareAmount;
                case HealthInsurancePackage.Fair:
                    return Constants.FairHealthCareAmount;
                case HealthInsurancePackage.Premium:
                    return Constants.PremiumHealthCareAmount;
                default:
                    return 0m;
            }
        }
      
        public decimal CalculateTransportationAllowece(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (employee.WorkPlatform == WorkPlatform.Office)
                return Constants.TransportationAllowanceAmount;

            if (employee.WorkPlatform == WorkPlatform.Remote)
                return 0m;

            return Constants.TransportationAllowanceAmount / 2;
        }

        public decimal calculateNetSalary(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            var dangerPay = CalculateDangerPay(employee);
            var transportationAllowance = CalculateTransportationAllowece(employee);
            var dependantsAllowance = CalculateDependancyAllowance(employee);
            var spouseAllowance = CalculateSpouseAllowance(employee);
            var basicSalary = CalculateBasicSalary(employee);

            var healthInduranceDeduction = CalculateHealthInsurance(employee);
            var pensionDeductions = CalculatePension(employee);
            var tax = CalculateTax(employee);

           

            var netSalary =
                (basicSalary + transportationAllowance + spouseAllowance + dependantsAllowance + dangerPay)
                - (pensionDeductions + healthInduranceDeduction + tax);

            return netSalary;
        }

        public string Process(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            var dangerPay = CalculateDangerPay(employee);
            var transportationAllowance = CalculateTransportationAllowece(employee);
            var dependantsAllowance = CalculateDependancyAllowance(employee);
            var spouseAllowance = CalculateSpouseAllowance(employee);

            var healthInduranceDeduction = CalculateHealthInsurance(employee);
            var pensionDeductions = CalculatePension(employee);
            var tax = CalculateTax(employee);

            var basicSalary = CalculateBasicSalary(employee);
            var netSalary = calculateNetSalary(employee);
 

            return
                   $" \n" +
                   $" \n --- Employee Information ---" +
                   $" \n" +
                   $" \n   Name: {employee.Name}" +
                   $" \n   DutyStation: {employee.DutyStation}" +
                   $" \n   Wage: ${employee.Wage:n0}" +
                   $" \n   Days: {employee.WorkingDays} days" +
                   $" \n   Basic Salary:  ${basicSalary:n0} " +
                   $" \n" +
                   $" \n --- Benifits ---" +
                   $" \n" +
                   $" \n   Dependants Allowance: ${dependantsAllowance:n0} " +
                   $" \n   Spouse Allowance: ${spouseAllowance:n0} " +
                   $" \n   Danger Pay: ${dangerPay:n0} " +
                   $" \n   Transporation Allowance: ${transportationAllowance:n0} " +
                   $" \n" +
                   $" \n --- Deductions ---" +
                   $" \n" +
                   $" \n   Health Insurance Deductions: ${healthInduranceDeduction:n0} " +
                   $" \n   Pension Deductions: ${pensionDeductions:n0} " +
                   $" \n   Tax: ${tax:n0} " +
                   $" \n" +
                   $" \n --- Net Salary ---" +
                   $" \n                    ${netSalary:n0}";

        }
    }
}
