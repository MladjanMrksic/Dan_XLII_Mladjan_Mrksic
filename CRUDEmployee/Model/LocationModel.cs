using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace CRUDEmployee.Model
{
    class LocationModel
    {
        Archive archive = new Archive();
        /// <summary>
        /// This method gets all the locations from database and adds them to a list
        /// </summary>
        /// <returns>A list of all locations in DataBase</returns>
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
        /// <summary>
        /// Adds new location to database
        /// </summary>
        /// <param name="L">A location that is to be added</param>
        public void AddLocation (Location L)
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    context.Locations.Add(L);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LocationsToDB()
        {
            List<Location> existingLocations = GetAllLocations();
            List<Location> newLocations = new List<Location>();
            StreamReader sr = new StreamReader(@".../.../Locations.txt");
            if (!File.Exists(@".../.../Locations.txt"))
                File.Create(@".../.../Locations.txt").Close();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Location l = new Location();
                l.Address = line;
                newLocations.Add(l);
            }
            var locationsToAdd = newLocations.Where(l => existingLocations.All(l2 => l2.Address != l.Address));
            locationsToAdd = locationsToAdd.OrderBy(l => l.Address).ToList();
            using (Task_1Entities context = new Task_1Entities())
            {
                foreach (var loc in locationsToAdd)
                {
                    context.Locations.Add(loc);
                    string input = (DateTime.Now + " / Added new location with address " + loc.Address + " directly to database");
                    archive.WriteToFile(input);
                    context.SaveChanges();
                }
            }
        }
    }
}
