using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace CRUDEmployee.Model
{
    class EmployeeModel
    {
        Archive archive = new Archive();
        /// <summary>
        /// This method gets all the employees from database and adds them to a list
        /// </summary>
        /// <returns>A list of all employees in DataBase</returns>
        public List<Employee> GetAllEmployees()
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    List<Employee> list = new List<Employee>();
                    list = (from x in context.Employees select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        /// <summary>
        /// This method gets all the employees from view and adds them to a list
        /// </summary>
        /// <returns>A list of all employees in view</returns>
        public List<EmployeeView> GetEmployeeViews ()
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    List<EmployeeView> list = new List<EmployeeView>();
                    list = (from x in context.EmployeeViews select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        /// <summary>
        /// This method deletes an employee from the database
        /// </summary>
        /// <param name="deleteID">ID of employee that is to be deleted</param>
        public void DeleteEmplyee(int deleteID)
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    Employee employee = (from e in context.Employees where e.EmployeeID == deleteID select e).FirstOrDefault();
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                    string input = (DateTime.Now + " / Deleted Employee named " + employee.FirstName + " " + employee.LastName + " in sector " + employee.Sector.SectorName);
                    archive.WriteToFile(input);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// This method updates an employee from the database
        /// </summary>
        /// <param name="employee">Employee that is to be updated</param>
        /// <returns>A updated employee</returns>
        public Employee UpdateEmployee(Employee employee)
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    Employee EmployeeToUpdate = (from e in context.Employees where e.EmployeeID == employee.EmployeeID select e).First();
                    EmployeeToUpdate.FirstName = employee.FirstName;
                    EmployeeToUpdate.LastName = employee.LastName;
                    EmployeeToUpdate.DateOfBirth = employee.DateOfBirth;
                    EmployeeToUpdate.IDNumber = employee.IDNumber;
                    EmployeeToUpdate.JMBG = employee.JMBG;
                    EmployeeToUpdate.Gender = employee.Gender;
                    EmployeeToUpdate.PhoneNumber = employee.PhoneNumber;
                    EmployeeToUpdate.SectorID = employee.SectorID;
                    EmployeeToUpdate.Location = employee.Location;
                    EmployeeToUpdate.Manager = employee.Manager;
                    context.SaveChanges();
                    string input = (DateTime.Now + " / Updated Employee named " + EmployeeToUpdate.FirstName + " " + EmployeeToUpdate.LastName + " in sector " + EmployeeToUpdate.Sector.SectorName);
                    archive.WriteToFile(input);
                    return employee;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        /// <summary>
        /// A method that parses JMBG to Date of birth
        /// </summary>
        /// <param name="JMBG">JMBG of an employee</param>
        /// <returns>Date of Birth</returns>
        public DateTime JMBGCheck(string JMBG)
        {
            try
            {
                //Taking only first 7 digits of JMBG
                string firstSevenDigits = new string(JMBG.Take(7).ToArray());
                //Inserting "/" after 2nd and then 5th character
                firstSevenDigits = firstSevenDigits.Insert(2, "/");
                firstSevenDigits = firstSevenDigits.Insert(5, "/");
                //Checking if 6th character is 9 and adding 1 before it
                if (firstSevenDigits.ElementAt(6) == '9')
                {
                    firstSevenDigits = firstSevenDigits.Insert(6, "1");
                }
                //Checking if 6th character is 0 and adding 2 in before it
                else if (firstSevenDigits.ElementAt(6) == '0')
                {
                    firstSevenDigits = firstSevenDigits.Insert(6, "2");
                }
                //Parsing the results to DateTime format
                DateTime DateOfBirth = DateTime.Parse(firstSevenDigits);
                return DateOfBirth;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString());
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Adds and employee to the database
        /// </summary>
        /// <param name="employee">Employee to be added to the database</param>
        /// <returns>An employee that was added to the database</returns>
        public Employee AddEmployee(Employee employee)
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    Employee newEmployee = new Employee();
                    newEmployee.FirstName = employee.FirstName;
                    newEmployee.LastName = employee.LastName;
                    newEmployee.DateOfBirth = employee.DateOfBirth;
                    newEmployee.IDNumber = employee.IDNumber;
                    newEmployee.JMBG = employee.JMBG;
                    newEmployee.Gender = employee.Gender;
                    newEmployee.PhoneNumber = employee.PhoneNumber;
                    newEmployee.SectorID = employee.SectorID;
                    newEmployee.Location = employee.Location;
                    newEmployee.Manager = employee.Manager;
                    context.Employees.Add(newEmployee);
                    context.SaveChanges();
                    string input = (DateTime.Now + " / Added new Employee named " + newEmployee.FirstName + " " + newEmployee.LastName + " to sector " + newEmployee.Sector.SectorName);
                    archive.WriteToFile(input);
                    return newEmployee;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void EmployeesToDB()
        {
            List<Employee> existingEmployees = GetAllEmployees();
            List<Employee> newEmployees = new List<Employee>();
            StreamReader sr = new StreamReader(@".../.../Employees.txt");
            if (!File.Exists(@".../.../Employees.txt"))
                File.Create(@".../.../Employees.txt").Close();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] employeeData = line.Split(',');
                Employee e = new Employee();
                e.FirstName = employeeData[0];
                e.LastName = employeeData[1];
                e.DateOfBirth = Convert.ToDateTime(employeeData[2]);
                e.IDNumber = employeeData[3];
                e.JMBG = employeeData[4];
                e.Gender = employeeData[5];
                e.PhoneNumber = employeeData[6];
                e.SectorID = Convert.ToInt32(employeeData[7]);
                e.LocationID = Convert.ToInt32(employeeData[8]);
                e.Manager = employeeData[9];
                newEmployees.Add(e);
            }
            var employeesToAdd = newEmployees.Where(e => existingEmployees.All(e2 => e2.JMBG != e.JMBG));
            using (Task_1Entities context = new Task_1Entities())
            {
                foreach (var emp in employeesToAdd)
                {
                    context.Employees.Add(emp);
                    string input = (DateTime.Now + " / Added new Employee with name " + emp.FirstName + " " + emp.LastName + " directly to database");
                    archive.WriteToFile(input);
                    context.SaveChanges();
                }
            }
        }

    }
}