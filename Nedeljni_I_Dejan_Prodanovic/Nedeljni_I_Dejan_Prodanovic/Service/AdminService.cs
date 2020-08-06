using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    class AdminService:IAdminService
    {
        public tblAdmin AddAdmin(tblAdmin admin)
        {
            try
            {
                using (CompanyDataEntities1 context = new CompanyDataEntities1())
                {

                    tblAdmin newAdmin = new tblAdmin();
                    newAdmin.UserID = admin.UserID;
                    newAdmin.ExpiryDate = admin.ExpiryDate;
                    newAdmin.AdministratorType = admin.AdministratorType;

                    context.tblAdmins.Add(newAdmin);
                    context.SaveChanges();

                    return newAdmin;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblAdmin GetAdminByUserId(int userId)
        {
            try
            {
                using (CompanyDataEntities1 context = new CompanyDataEntities1())
                {


                    tblAdmin admin = (from x in context.tblAdmins
                                    where x.UserID == userId
                                      select x).First();

                    return admin;
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
