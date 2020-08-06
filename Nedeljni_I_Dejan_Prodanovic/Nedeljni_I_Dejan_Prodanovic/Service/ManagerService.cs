using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    class ManagerService:IManagerService
    {
        public tblManager AddManager(tblManager manager)
        {
            try
            {
                using (CompanyDataEntities1 context = new CompanyDataEntities1())
                {

                    tblManager newManager = new tblManager();
                    newManager.UserID = manager.UserID;
                    newManager.Email = manager.Email;
                    newManager.ReservePassword = manager.ReservePassword;
                    newManager.NumberOfSuccesfullProjects = manager.NumberOfSuccesfullProjects;
                    newManager.OfficeNumber = manager.OfficeNumber;

                    context.tblManagers.Add(newManager);
                    context.SaveChanges();

                    return newManager;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        public tblManager GetManagerByEmail(string email)
        {
            try
            {
                using (CompanyDataEntities1 context = new CompanyDataEntities1())
                {


                    tblManager user = (from x in context.tblManagers
                                    where x.Email.Equals(email)

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

        public tblManager GetManagerByUserId(int userId)
        {
            try
            {
                using (CompanyDataEntities1 context = new CompanyDataEntities1())
                {


                    tblManager manager = (from x in context.tblManagers
                                      where x.UserID == userId
                                      select x).First();

                    return manager;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
