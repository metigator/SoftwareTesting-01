using Moq;
using System;
using Xunit;

namespace SW.Payroll.Tests
{
    public class SalarySlipProcessorTests
    {
        //[Fact]
        //public void Method_Scenario_Outcome()
        //{

        //}

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

        //public decimal CalculateDangerPay(Employee employee)
        //{


        //    return 0m;
        //}


    }
}
