using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Nedeljni_I_Dejan_Prodanovic.Model;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    class RequestService : IRequestService
    {
        public tblRequestForChange AddRequest(tblEmployee employeeOld, tblEmployee employeeNew)
        {
            try
            {
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {

                    tblRequestForChange newRequest = new tblRequestForChange();
                    newRequest.OldEmployeeID = employeeOld.EmployeeID;
                    newRequest.NewEmployeeID = employeeNew.EmployeeID;
                    newRequest.CreationDate = DateTime.Today;

                    context.tblRequestForChanges.Add(newRequest);
                    context.SaveChanges();

                    return newRequest;

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
