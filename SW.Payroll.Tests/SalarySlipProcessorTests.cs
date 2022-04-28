using Moq;
using System;
using Xunit;

namespace SW.Payroll.Tests
{
    public class SalarySlipProcessorTests
    {
       
        [Fact]
        public void CalculateBasicSalary_EmployeeIsNull_ThrowArgumentNullException()
        {
            // Arrange

            Employee employee = null;

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateBasicSalary(employee);

            // Assert 

            Assert.Throws<ArgumentNullException>(() => func(employee));

        }

        [Fact]
        public void CalculateBasicSalary_ForEmployeeWageAndWorkingDays_ReturnsBasicSalary()
        {
            // Arrange
            
            var employee = new Employee { Wage = 500m, WorkingDays = 20 };

            // Act
            
            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateBasicSalary(employee);
            
            var expected = 10000m;
            
            // Assert

            Assert.Equal(actual, expected);
        }


        [Fact]
        public void CalculateSpouseAllowances_EmployeeIsNull_ThrowArgumentNullException()
        {
            // Arrange

            Employee employee = null;

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateSpouseAllowance(employee);

            // Assert 

            Assert.Throws<ArgumentNullException>(() => func(employee));

        }

        [Fact]
        public void CalculateSpouseAllowance_ForMarriedEmployee_SpouseAllowance()
        {
            // Arrange

            var employee = new Employee { IsMarried = true };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateSpouseAllowance(employee);

            var expected = Constants.SpouseAllowanceAmount;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateSpouseAllowance_ForNonMarriedEmployee_ReturnsZero()
        {
            // Arrange

            var employee = new Employee { IsMarried = false };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateSpouseAllowance(employee);

            var expected = 0m;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateDependancyAllowance_EmployeeIsNull_ThrowArgumentNullException()
        {
            // Arrange

            Employee employee = null;

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateSpouseAllowance(employee);

            // Assert 

            Assert.Throws<ArgumentNullException>(() => func(employee));

        }

        [Fact]
        public void CalculateDependancyAllowance_EmployeeWithOneDependants_ReturnsDependancyAllowance()
        {
            // Arrange
            var totalDependants = 2;
            var employee = new Employee { TotalDependancies = totalDependants };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateDependancyAllowance(employee);

            var expected = Constants.DependancyAllowancePerChildAmount * totalDependants;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateDependancyAllowance_EmployeeWithZeroDependants_ReturnsZero()
        {
            // Arrange

            var employee = new Employee { TotalDependancies = 0 };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateDependancyAllowance(employee);

            var expected = 0m;

            // Assert

            Assert.Equal(actual, expected);
        }
         
        [Fact]
        public void CalculateDependancyAllowance_EmployeeWithDependantsOverFive_ReturnsMaxDependancyAllowance()
        {
            // Arrange

            var employee = new Employee { TotalDependancies = 6 };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateDependancyAllowance(employee);

            var expected = Constants.MaxDependancyAllowanceAmount;

            // Assert

            Assert.Equal(actual, expected);
        }
         

        [Fact]
        public void CalculatePension_EmployeeIsNull_ThrowArgumentNullException()
        {
            // Arrange

            Employee employee = null;

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateSpouseAllowance(employee);

            // Assert 

            Assert.Throws<ArgumentNullException>(() => func(employee));

        }

        [Fact]
        public void CalculatePension_EmployeeHasPensionOn_ReturnsPensionAmount()
        {
            // Arrange 
            var employee = new Employee {  Wage = 500, WorkingDays = 20,  HasPensionPlan = true };
        
            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculatePension(employee);

            var expected = salarySlipProcessor.CalculateBasicSalary(employee) * Constants.PensionRate;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculatePension_EmployeeHasPensionOff_ReturnsZero()
        {
            // Arrange 
            var employee = new Employee { Wage = 500, WorkingDays = 20, HasPensionPlan = false };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculatePension(employee);

            var expected = 0;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateHealthInsurance_EmployeeIsNull_ThrowArgumentNullException()
        {
            // Arrange

            Employee employee = null;

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateHealthInsurance(employee);

            // Assert 

            Assert.Throws<ArgumentNullException>(() => func(employee));

        }

        [Fact]
        public void CalculateHealthInsurance_EmployeeWithBasicCoverage_ReturnsBasicCoverageAmount()
        {
            // Arrange 
            var employee = new Employee { HealthInsurancePackage = HealthInsurancePackage.Basic };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateHealthInsurance(employee);

            var expected = Constants.BasicHealthCareAmount;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateHealthInsurance_EmployeeWithFairCoverage_ReturnsFairCoverageAmount()
        {
            // Arrange 
            var employee = new Employee { HealthInsurancePackage = HealthInsurancePackage.Fair };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateHealthInsurance(employee);

            var expected = Constants.FairHealthCareAmount;

            // Assert

            Assert.Equal(actual, expected);
        }


        [Fact]
        public void CalculateHealthInsurance_EmployeeWithPremiumCoverage_ReturnsPremiumCoverageAmount()
        {
            // Arrange 
            var employee = new Employee { HealthInsurancePackage = HealthInsurancePackage.Premium };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateHealthInsurance(employee);

            var expected = Constants.PremiumHealthCareAmount;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateHealthInsurance_EmployeeHealthInsurancePackageIsNull_ReturnsZero()
        {
            // Arrange 
            var employee = new Employee { HealthInsurancePackage = null };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateHealthInsurance(employee);

            var expected = 0;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateTransportationAllowece_EmployeeIsNull_ThrowArgumentNullException()
        {
            // Arrange

            Employee employee = null;

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateTransportationAllowece(employee);

            // Assert 

            Assert.Throws<ArgumentNullException>(() => func(employee));

        }

        [Fact]
        public void CalculateTransportationAllowece_EmployeeWorkInOffice_ReturnsTransporationAllowance()
        {
            // Arrange

            var employee = new Employee { WorkPlatform = WorkPlatform.Office};

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateTransportationAllowece(employee);

            var expected = Constants.TransportationAllowanceAmount;

            // Assert

            Assert.Equal(actual, expected);
        }


        [Fact]
        public void CalculateTransportationAllowece_EmployeeWorkRemote_ReturnsTransporationAllowance()
        {
            // Arrange

            var employee = new Employee { WorkPlatform = WorkPlatform.Remote };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateTransportationAllowece(employee);

            var expected = 0m;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateTransportationAllowece_EmployeeWorkHybridMode_ReturnsTransporationAllowance()
        {
            // Arrange

            var employee = new Employee { WorkPlatform = WorkPlatform.Hybrid };

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateTransportationAllowece(employee);

            var expected = Constants.TransportationAllowanceAmount/2;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateDangerPay_EmployeeIsNull_ThrowArgumentNullException()
        {
            // Arrange

            Employee employee = null;

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateDangerPay(employee);

            // Assert 

            Assert.Throws<ArgumentNullException>(() => func(employee));

        }

        [Fact]
        public void CalculateDangerPay_EmployeeIsDangerOn_ReturnsDangerPay()
        {
            // Arrange

            var employee = new Employee { IsDanger = true};

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateDangerPay(employee);

            var expected = Constants.DangerPayAmount;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateDangerPay_EmployeeIsDangerOffAndInDangerZone_ReturnsDangerPay()
        {
            // Arrange

            var employee = new Employee { IsDanger = false, DutyStation = "Ukraine" };
            
            var mock = new Mock<IZoneService>();
            
            var setup = mock.Setup(z=>z.IsDangerZone(employee.DutyStation)).Returns(true);
            
            // Act 

            var salarySlipProcessor = new SalarySlipProcessor(mock.Object);

            var actual = salarySlipProcessor.CalculateDangerPay(employee);

            var expected = Constants.DangerPayAmount;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateDangerPay_EmployeeIsDangerOffAndNotInDangerZone_ReturnsZero()
        {
            // Arrange

            var employee = new Employee { IsDanger = false, DutyStation = "Ukraine" };

            var mock = new Mock<IZoneService>();

            var setup = mock.Setup(z => z.IsDangerZone(employee.DutyStation)).Returns(false);

            // Act 

            var salarySlipProcessor = new SalarySlipProcessor(mock.Object);

            var actual = salarySlipProcessor.CalculateDangerPay(employee);

            var expected = 0m;

            // Assert

            Assert.Equal(actual, expected); 
        }

        [Fact]
        public void CalculateTax_EmployeeIsNull_ThrowArgumentNullException()
        {
            // Arrange

            Employee employee = null;

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateSpouseAllowance(employee);

            // Assert 

            Assert.Throws<ArgumentNullException>(() => func(employee));

        }

        [Fact]
        public void CalculateTax_EmployeeWithHighSalaryThreshold_ReturnsHighTaxAmount()
        {
            // Arrange 
            var employee = new Employee { Wage = 1000, WorkingDays = 22 }; // 22,000

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateTax(employee);

            var expected = salarySlipProcessor.CalculateBasicSalary(employee)
                * Constants.HighSalaryTaxFactor;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateTax_EmployeeWithMediumSalaryThreshold_ReturnsMediumTaxAmount()
        {
            // Arrange 
            var employee = new Employee { Wage = 800, WorkingDays = 22 }; // 17,600

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateTax(employee);

            var expected = salarySlipProcessor.CalculateBasicSalary(employee)
                * Constants.MediumSalaryTaxFactor;

            // Assert

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CalculateTax_EmployeeWithLowSalaryThreshold_ReturnsZeroTaxAmount()
        {
            // Arrange 
            var employee = new Employee { Wage = 450, WorkingDays = 22 }; // 9,900

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            var actual = salarySlipProcessor.CalculateTax(employee);

            var expected = 0m;

            // Assert

            Assert.Equal(actual, expected);
        }



        [Fact]
        public void CalculateNetSalary_EmployeeIsNull_ThrowArgumentNullException()
        {
            // Arrange

            Employee employee = null;

            // Act

            var salarySlipProcessor = new SalarySlipProcessor(null);

            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateDangerPay(employee);

            // Assert 

            Assert.Throws<ArgumentNullException>(() => func(employee));
        }

        [Fact]
        public void CalculateNetSalary_ForGivenEmployee_ReturnsNetSalary()
        {
            // Arrange

            var employee = new Employee {
                Wage = 100,
                WorkingDays = 20,
                TotalDependancies = 2,
                IsMarried = true,
                HasPensionPlan = true,
                HealthInsurancePackage = HealthInsurancePackage.Basic,
                IsDanger = true 
            };


            var mock = new Mock<IZoneService>();
            var setup = mock.Setup(z => z.IsDangerZone(employee.DutyStation)).Returns(false);
            var salarySlipProcessor = new SalarySlipProcessor(mock.Object);

            var basicSalary = salarySlipProcessor.CalculateBasicSalary(employee);
            var pension = salarySlipProcessor.CalculatePension(employee);
            var healthInsurance = salarySlipProcessor.CalculateHealthInsurance(employee);
            var dangerPay = salarySlipProcessor.CalculateDangerPay(employee);
            var spouseAllowance = salarySlipProcessor.CalculateSpouseAllowance(employee);
            var dependancyAllowance = salarySlipProcessor.CalculateDependancyAllowance(employee);
            var transportationAllowance = salarySlipProcessor.CalculateTransportationAllowece(employee);
            var tax = salarySlipProcessor.CalculateTax(employee);

            var benifits = 
                basicSalary + dangerPay + spouseAllowance 
                + dependancyAllowance + transportationAllowance;

            var deductions = pension + healthInsurance + tax;

            var expected = benifits - deductions;
            
            var actual = salarySlipProcessor.calculateNetSalary(employee);
            
            // Act 

            Assert.Equal(expected, actual); 
        }
    }
}
