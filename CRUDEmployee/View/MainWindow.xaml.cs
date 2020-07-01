using CRUDEmployee.Model;
using CRUDEmployee.ViewModel;
using System.Windows;

namespace CRUDEmployee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeModel emp = new EmployeeModel();
        LocationModel locModel = new LocationModel();
        SectorModel secModel  = new SectorModel();
        EmployeeModel empMode = new EmployeeModel();
        public MainWindow()
        {
            InitializeComponent();
            locModel.LocationsToDB();
            secModel.SectorsToDB();
            empMode.EmployeesToDB();
            this.DataContext = new MainWindowViewModel(this);
        }
    }
}
