using CRUDEmployee.ViewModel;
using System.Windows;

namespace CRUDEmployee.View
{
    /// <summary>
    /// Interaction logic for UpdateEmployee.xaml
    /// </summary>
    public partial class UpdateEmployeeView : Window
    {
        public UpdateEmployeeView(Employee employee)
        {
            InitializeComponent();
            this.DataContext = new UpdateEmployeeViewModel(this, employee);
        }
    }
}
