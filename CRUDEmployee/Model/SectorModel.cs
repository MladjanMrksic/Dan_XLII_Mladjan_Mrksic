using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace CRUDEmployee.Model
{
    class SectorModel
    {
        Archive archive = new Archive();
        public Sector AddSector (Sector sector)
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    Sector newSector = new Sector();
                    newSector.SectorID = sector.SectorID;
                    newSector.SectorName = sector.SectorName;
                    context.Sectors.Add(newSector);
                    context.SaveChanges();
                    return newSector;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message.ToString() + "/" + ex.Source + "/" + ex.InnerException);
                return null;
            }
        }

        /// <summary>
        /// This method gets all the sectors from database and adds them to a list
        /// </summary>
        /// <returns>A list of all sectors in DataBase</returns>
        public List<Sector> GetAllSectors()
        {
            try
            {
                using (Task_1Entities context = new Task_1Entities())
                {
                    List<Sector> list = new List<Sector>();
                    list = (from x in context.Sectors select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void SectorsToDB()
        {
            List<Sector> existingSectors = GetAllSectors();
            List<Sector> newSectors = new List<Sector>();
            StreamReader sr = new StreamReader(@".../.../Sectors.txt");
            if (!File.Exists(@".../.../Sectors.txt"))
                File.Create(@".../.../Sectors.txt").Close();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Sector s = new Sector();
                s.SectorName = line;
                newSectors.Add(s);
            }
            var sectorsToAdd = newSectors.Where(s => existingSectors.All(s2 => s2.SectorName != s.SectorName));
            using (Task_1Entities context = new Task_1Entities())
            {
                foreach (var sec in sectorsToAdd)
                {
                    context.Sectors.Add(sec);
                    string input = (DateTime.Now + " / Added new location with address " + sec.SectorName + " directly to database");
                    archive.WriteToFile(input);
                    context.SaveChanges();
                }
            }
        }
    }
}
