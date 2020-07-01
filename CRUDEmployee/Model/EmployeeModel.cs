using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRUDEmployee.Model
{
    class EmployeeModel
    {
        Archive archive = new Archive();
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
    }
}