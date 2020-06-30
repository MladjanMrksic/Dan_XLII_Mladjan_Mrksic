﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRUDEmployee.Model
{
    class SectorModel
    {
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
    }
}
