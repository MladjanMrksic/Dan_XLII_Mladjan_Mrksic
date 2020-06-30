using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRUDEmployee.Model
{
    class LocationModel
    {
        public List<Location> GetAllLocations()
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    List<Location> list = new List<Location>();
                    list = (from x in context.Locations select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
