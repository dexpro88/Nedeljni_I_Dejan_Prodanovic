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
                using (CompanyDataEntities context = new CompanyDataEntities())
                {

                    tblAdmin newAdmin = new tblAdmin();
                    newAdmin.UserID = admin.UserID;
                    newAdmin.ExpiryDate = admin.ExpiryDate;

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
    }
}
