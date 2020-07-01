using CRUDEmployee.ViewModel;
using System.Windows;

namespace CRUDEmployee.View
{
    /// <summary>
    /// Interaction logic for AddEmployeeView.xaml
    /// </summary>
    public partial class AddEmployeeView : Window
    {
        public AddEmployeeView()
        {
            InitializeComponent();
            this.DataContext = new AddEmployeeViewModel(this);
        }
    }
}
