using System;
using System.Collections;

namespace EmployeeHierarchy
{
    public class Employee
    {
        ArrayList EmployeeData = new ArrayList();
        ArrayList companyEmployees = new ArrayList(); //this stores emplyees
        ArrayList companymanagers = new ArrayList();  //this stores managers
        ArrayList companyceos = new ArrayList();   //this stores ceo
        ArrayList companyjuniors = new ArrayList();  //this store junior employees who are under a certain manager
        //constructor
        public Employee(string csv)
        {
            if(string.IsNullOrEmpty(csv) || csv.Length ==0) //check of string is empty or not
            {
                throw new Exception("Employee Data is Required");
            }
            //otherwise process the data
            string[] employeedatarows = csv.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
          //  ArrayList empinfo = new ArrayList();
            foreach (string row in employeedatarows)
            {
                string[] empdata = row.Split(',');
                ArrayList csvdata = new ArrayList();

                foreach (string cell in empdata)
                {
                    csvdata.Add(cell);
                }
                if (csvdata.Count != 3)
                {
                    throw new Exception("The data in the CSV is incomplete");
                }
                EmployeeData.Add(empdata);
            }

           //do validations accordingly
            //check if salary is integer
            foreach (string[] emp in EmployeeData)
            {
                string salary = Convert.ToString(emp[2]);
                int number;
                if (!(Int32.TryParse(salary, out number)))
                {
                    throw new Exception("The data in the CSV file contains invalid salary details");
                }
            }

            //one employee does not report to more than one manager
            ValidateManagementHierachy(EmployeeData);
        }

        public void ValidateManagementHierachy(ArrayList employeeinfo)
        {
            foreach (string[] emp in employeeinfo)
            {
                string employeename = emp[0] as string;
                string managername = emp[1] as string;

                if (companyEmployees.Contains(employeename.Trim()))
                {
                    throw new Exception("An employee must report to one manager, check on " + employeename);
                }

                companyEmployees.Add(employeename.Trim());

                if (!string.IsNullOrEmpty(managername.Trim()))
                {
                    companymanagers.Add(managername.Trim());
                }
                else
                {
                    companyceos.Add(employeename.Trim());
                }

            }

            int managerandceocount = companyEmployees.Count - companymanagers.Count;
            if (managerandceocount != 1)
            {
                throw new Exception("The company must have only one CEO");
            }
            // Validate if the managers are also on the employee column
            foreach (string manager in companymanagers)
            {
                if (!companyEmployees.Contains(manager.Trim()))
                {
                    throw new Exception("There are managers that are not employees,  all managers must be listed in the employee column in the CSV");
                }
            }


            // Add a employees that report to other employees - company juniors here
            foreach (string employee in companyEmployees)
            {
                if (!companymanagers.Contains(employee) && !companyceos.Contains(employee))
                {
                    companyjuniors.Add(employee.Trim());
                }
            }
            //4. . There is no circular reference, i.e. a first employee reporting to a second employee that is also under the first employee.
           for (var i = 0; i < employeeinfo.Count; i++)
            {
                var employeeData = employeeinfo[i] as string[];
                var employeeManager = employeeData[1] as string;
                int index = companyEmployees.IndexOf(employeeManager);

                if (index != -1)
                {
                    var managerData = employeeinfo[index] as string[];
                    var topManager = managerData[1] as string;

                    if ((companymanagers.Contains(topManager.Trim()) && !companyceos.Contains(topManager.Trim()))
                        || companyjuniors.Contains(topManager.Trim()))
                    {
                        throw new Exception("There is a circular hierarchy detected in the CSV file, please confirm");
                    }
                }
            }


        }

        public long SalaryBudgetforManager(string manageName)
        {
            long ManagerSalaryBudget = 0;
            foreach (string[] emp in EmployeeData)
            {
                var name = emp[1] as string;
                var Salary = emp[2] as string;
                var employeName = emp[0] as string;
                if (name.Trim() == manageName.Trim() || employeName.Trim() == manageName.Trim())
                {
                    ManagerSalaryBudget += Convert.ToInt64(Salary);
                }
            }
            return ManagerSalaryBudget;
        }
    }
}
