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
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
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
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
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
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
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
        public List<vwManager> GetManagers()
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {
                    List<vwManager> list = new List<vwManager>();
                    list = (from x in context.vwManagers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void DeleteManager(int managerId)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {
                    tblManager managerToDelete = (from u in context.tblManagers
                                                where u.ManagerID == managerId
                                                  select u).First();

                    tblUser userToDelete = (from u in context.tblUsers
                                                  where u.UserID == managerToDelete.UserID
                                                  select u).First();

                    context.tblManagers.Remove(managerToDelete);
                    context.tblUsers.Remove(userToDelete);

                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        public vwManager SetResponsibility(vwManager manager)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {

                    tblManager managerToEdit = (from u in context.tblManagers
                                              where u.ManagerID == manager.ManagerID
                                              select u).First();


                    managerToEdit.ResponsibilityLevel = manager.ResponsibilityLevel;
                   

                    context.SaveChanges();

                  
                    return manager;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwEmployee2> GetEmployeesOfManager(int managerID)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {
                    List<vwEmployee2> list = new List<vwEmployee2>();
                    list = (from x in context.vwEmployee2
                            where x.ManagerID == managerID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void EditManager(tblManager manager)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {

                    tblManager managerToEdit = (from u in context.tblManagers
                                                where u.ManagerID == manager.ManagerID
                                                select u).First();

                    managerToEdit.Email = manager.Email;
                    managerToEdit.ReservePassword = manager.ReservePassword;
                    managerToEdit.OfficeNumber = manager.OfficeNumber;
                  

                    context.SaveChanges();



                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());

            }
        }

        public vwManager GetvwManager(int managerId)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {


                    vwManager manager = (from x in context.vwManagers
                                          where x.ManagerID == managerId
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
