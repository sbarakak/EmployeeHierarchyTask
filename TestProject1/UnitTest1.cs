using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EmployeeHierarchy;

namespace TestEmployees
{
	[TestClass]

	public class UnitTest1
	{
        [TestMethod]
        public void TestBlankCSV()
        {
            Assert.ThrowsException<Exception>(() => new Employee(""));

        }

        [TestMethod]
        public void TestEmployeeReportingOneManager()
        {
            Assert.ThrowsException<Exception>(() => new Employee("Employee4,Employee2,500" +
                "\r\n" + "Employee3,Employee1,800\r\nEmployee3,Employee2,1900" +
                "\r\n" + "Employee1,,1000\r\nEmployee5,Employee1,500\r\nEmployee2,EMployee1,500"));

        }
        [TestMethod]
        public void TestCEOsinCompany()
        {
            Assert.ThrowsException<Exception>(() => new Employee("Employee4,Employee2,500" +
                "\r\n" + "Employee3,Employee1,800\r\nEmployee6,Employee2,1900" +
                "\r\n" + "Employee1,,1000\r\nEmployee5,,500\r\nEmployee2,EMployee1,500"));

        }

        [TestMethod]
        public void TestCircularReference()
        {
            Assert.ThrowsException<Exception>(() => new Employee("Employee4,Employee1,500" +
                "\r\n" + "Employee3,Employee1,800\r\nEmployee6,Employee2,1900" +
                "\r\n" + "Employee1,Employee1,1000\r\nEmployee5,,500\r\nEmployee2,Employee1,500"));

        }


        [TestMethod]
        public void TestManagersNotEmployee()
        {
            Assert.ThrowsException<Exception>(() => new Employee("Employee4,Employee1,500" +
                "\r\n" + "Employee3,Employee1,800\r\nEmployee6,Employee2,1900" +
                "\r\n" + "Employee7,Employee1,1000\r\nEmployee5,,500\r\nEmployee2,Employee1,500"));

        }


        [TestMethod]
        public void TestGetManagerBudgetCorrectly()
        {
            Employee test = new Employee("Emplyee4,Employee2,500" +
                "\r\n" + "Employee3,Employee1,800\r\nEmployee1,,1000" +
                "\r\n" + "Employee5,Employee1,500\r\nEmployee2,Employee1,500");
            Assert.AreEqual(2800, test.SalaryBudgetforManager("Employee1"));
        }
       
    }
}