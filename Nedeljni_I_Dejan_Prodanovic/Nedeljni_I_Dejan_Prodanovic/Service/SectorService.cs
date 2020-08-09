using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    class SectorService:ISectorService
    {
        public tblSector AddSector(tblSector sector)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {

                    tblSector newSector = new tblSector();
                    newSector.SectorName = sector.SectorName;
                    newSector.SectorDescription = sector.SectorDescription;
                    
                    context.tblSectors.Add(newSector);
                    context.SaveChanges();

                    return newSector;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblSector GetSectorByName(string sectorName)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {


                    tblSector user = (from x in context.tblSectors
                                    where x.SectorName.Equals(sectorName)

                                    select x).First();

                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblSector> GetSectors()
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {
                    List<tblSector> list = new List<tblSector>();
                    list = (from x in context.tblSectors select x).ToList();

                    list.RemoveAll(x => x.SectorName == "default");
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        public void DeleteSector(int sectorId)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {
                    tblSector sectorToDelete = (from u in context.tblSectors
                                                where u.SectorID == sectorId
                                                select u).First();
                  

                    context.tblSectors.Remove(sectorToDelete);
                 
                    context.SaveChanges();

                  
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
