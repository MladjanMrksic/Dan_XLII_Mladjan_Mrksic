using CRUDEmployee.Commands;
using CRUDEmployee.Model;
using CRUDEmployee.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace CRUDEmployee.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        EmployeeModel empModel = new EmployeeModel();
        BackgroundWorker bw = new BackgroundWorker();

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            EmpViews = empModel.GetEmployeeViews();
            bw.DoWork += DoWork;
            bw.RunWorkerCompleted += WorkCompleted;
        }
        
        private EmployeeView employee;
        public EmployeeView Employee
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
            bw.RunWorkerAsync();
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

        private ICommand updateEmployee;
        public ICommand UpdateEmployee
        {
            get
            {
                if (updateEmployee == null)
                {
                    updateEmployee = new RelayCommand(param => UpdateEmployeeExecute(), param => CanUpdateEmployeeExecute());
                }
                return updateEmployee;
            }
        }
        private void UpdateEmployeeExecute()
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    int userID = Employee.EmployeeID;
                    Employee e = (from x in context.Employees where x.EmployeeID == userID select x).First();
                    UpdateEmployeeView updateEmplyee = new UpdateEmployeeView(e);
                    updateEmplyee.ShowDialog();
                    if ((updateEmplyee.DataContext as UpdateEmployeeViewModel).IsUpdateEmployee == true)
                    {
                        EmpViews = empModel.GetEmployeeViews();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message.ToString());
            }
        }
        private bool CanUpdateEmployeeExecute()
        {
            return true;
        }

        private ICommand addNewEmployee;
        public ICommand AddNewEmployee
        {
            get
            {
                if (addNewEmployee == null)
                {
                    addNewEmployee = new RelayCommand(param => AddNewEmployeeExecute(), param => CanAddNewEmployeeExecute());
                }
                return addNewEmployee;
            }
        }
        private void AddNewEmployeeExecute()
        {
            try
            {
                AddEmployeeView addEmployee = new AddEmployeeView();
                addEmployee.ShowDialog();
                EmpViews = empModel.GetEmployeeViews().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString());
            }
        }
        private bool CanAddNewEmployeeExecute()
        {
            return true;
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Employee != null)
                {
                    int employeeID = employee.EmployeeID;
                    empModel.DeleteEmplyee(employeeID);
                    EmpViews = empModel.GetEmployeeViews().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Thread.Sleep(2000);
        }
        void WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Error " + e.Error.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Completed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
