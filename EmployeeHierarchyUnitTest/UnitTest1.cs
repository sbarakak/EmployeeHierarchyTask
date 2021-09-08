using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EmployeeHierarchy;

namespace EmployeeHierarchyUnitTest
{
    [TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestBlankCSV()
		{
			Assert.ThrowsException<Exception>(() => new Emp(""));

		}

		[TestMethod]
		public void TestExceptionisThrownWhenOneEmployessReportsToMoreThanoneManger()
		{
			Assert.ThrowsException<Exception>(() => new Employees("John,manager1,100" +
				"\n" +
				"Joyce,manager1," + //error
				"1900\nJames,manager2,100" +
				"\n" +
				"Joyce,manager3,1900" + //error
				"\nCEO,,1000 \n " +
				"manager1,CEO,1900" +
				"\n" +
				"manager3,CEO,1900\nsalasia,manager1,1900\nmanager2,CEO,1900"));

		}

		[TestMethod]
		public void TestExceptionisThrownWhenWehaveMoreThanOneCEO()
		{
			Assert.ThrowsException<Exception>(() => new Employees("John,manager1,100" +
				"\n" +
				"Joyce,manager1," +
				"1900\nJames,manager2,100" +
				"\n" +
				"Eve,,1900" + // error
				"\nCEO,,1000 \n " + // error
				"manager1,CEO,1900" +
				"\n" +
				"manager3,CEO,1900\nsalasia,manager1,1900\nmanager2,CEO,1900"));

		}

		[TestMethod]
		public void TestExceptionisThrownWhenWehaveCircularReference()
		{
			Assert.ThrowsException<Exception>(() => new Employees("John,manager1,100" +
				"\n" +
				"Joyce,manager1," +
				"1900\nJames,manager2,100" +
				"\n" +
				"Eve,manager1,1900" +
				"\nCEO,,1000 \n " +
				"manager1,CEO,1900" +
				"\n" +
				"manager2,manager1,1900\n" + // error
	"salasia,manager1,1900\nmanager2,CEO,1900"));

		}

		[TestMethod]
		public void TestExceptionisThrownWhenAllManagersAreNotListedInEmployessCell()
		{
			Assert.ThrowsException<Exception>(() => new Employees("John,manager1,100" +
				"\n" +
				"Joyce,manager1," +
				"1900\nJames,manager2,100" +
				"\n" +
				"Eve,manager5,1900" + //error
				"\nCEO,,1000 \n " +
				"manager1,CEO,1900" +
				"\n" +
				"employess,manager1,1900\nsalasia,manager1,1900\nmanager2,CEO,1900"));

		}

		[TestMethod]
		public void TestManagerBurgetsReturnsCorrect()
		{

			Employees testEmployee = new Employees("John,manager1,100" +
				"\n" +
				"Joyce,manager1," +
				"1900\nJames,manager2,100" +
				"\n" +
				"Eve,manager1,1900" +
				"\nCEO,,1000 \n " +
				"manager1,CEO,1900" +
				"\n" +
				"employess,manager1,1900\nsalasia,manager1,1900\nmanager2,CEO,1900");

			Assert.AreEqual(4800, testEmployee.managerSalaryBudget("CEO"));

		}
	}
}
