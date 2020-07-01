using CRUDEmployee.Commands;
using CRUDEmployee.Model;
using CRUDEmployee.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace CRUDEmployee.ViewModel
{
    class AddEmployeeViewModel : ViewModelBase
    {
        AddEmployeeView addEmployeeView;
        EmployeeModel empModel = new EmployeeModel();
        LocationModel locModel = new LocationModel();
        SectorModel secModel = new SectorModel();
        BackgroundWorker bw = new BackgroundWorker();

        public AddEmployeeViewModel(AddEmployeeView addEmployeeOpen)
        {
            employee = new Employee();
            location = new Location();
            sector = new Sector();
            addEmployeeView = addEmployeeOpen;

            LocationList = locModel.GetAllLocations();
            SectorList = secModel.GetAllSectors();
            EmployeeList = empModel.GetAllEmployees();

            bw.DoWork += DoWork;
            bw.RunWorkerCompleted += WorkCompleted;
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

        private Location location;
        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        private Sector sector;
        public Sector Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }

        private Employee employeeManager;
        public Employee EmployeeManager
        {
            get
            {
                return employeeManager;
            }
            set
            {
                employeeManager = value;
                OnPropertyChanged("EmployeeManager");
            }
        }

        private List<Location> locationList;
        public List<Location> LocationList
        {
            get
            {
                return locationList;
            }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }

        private List<Sector> sectorList;
        public List<Sector> SectorList
        {
            get
            {
                return sectorList;
            }
            set
            {
                sectorList = value;
                OnPropertyChanged("SectorList");
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
                OnPropertyChanged("SectorList");
            }
        }

        private bool isUpdateEmployee;
        public bool IsUpdateEmployee
        {
            get
            {
                return isUpdateEmployee;
            }
            set
            {
                isUpdateEmployee = value;
            }
        }

        private ICommand _save;
        public ICommand Save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return _save;
            }
        }
        private void SaveExecute()
        {
            bw.RunWorkerAsync();
        }
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(employee.FirstName) || String.IsNullOrEmpty(employee.LastName) || employee.DateOfBirth > DateTime.Now || String.IsNullOrEmpty(employee.IDNumber) || String.IsNullOrEmpty(employee.JMBG) || String.IsNullOrEmpty(employee.Gender) || String.IsNullOrEmpty(employee.PhoneNumber) ||employee.SectorID > 0|| employee.LocationID > 0|| String.IsNullOrEmpty(employee.Manager))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand _close;
        public ICommand Close
        {
            get
            {
                if (_close == null)
                {
                    _close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return _close;
            }
        }
        private void CloseExecute()
        {
            try
            {
                addEmployeeView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message.ToString());
            }
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Employee.DateOfBirth = empModel.JMBGCheck(Employee.JMBG);
                Employee temp = empModel.AddEmployee(Employee);
                addEmployeeView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString());
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
