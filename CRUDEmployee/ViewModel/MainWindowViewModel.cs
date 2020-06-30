using CRUDEmployee.Commands;
using CRUDEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CRUDEmployee.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        EmployeeModel empModel = new EmployeeModel();

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            EmpViews = empModel.GetEmployeeViews();
        }

        private Employee employee;
        public Employee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private List<Employee> employeeList;
        public List<Employee> EmployeeList
        {
            get
            {
                return employeeList;
            }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private List<EmployeeView> empViews;

        public List<EmployeeView> EmpViews
        {
            get
            {
                return empViews;
            }
            set
            {
                empViews = value;
                OnPropertyChanged("EmpViews");
            }
        }


        private ICommand deleteEmployee;
        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }
        }
        private void DeleteEmployeeExecute()
        {
            try
            {
                if (Employee != null)
                {
                    int employeeID = employee.EmployeeID;
                    empModel.DeleteEmplyee(employeeID);
                    EmployeeList = empModel.GetAllEmployees().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteEmployeeExecute()
        {
            if (Employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



    }
}
