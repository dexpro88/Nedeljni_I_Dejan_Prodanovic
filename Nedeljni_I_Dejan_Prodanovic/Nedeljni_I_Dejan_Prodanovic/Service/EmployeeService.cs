using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    class EmployeeService:IEmployeeService
    {
        public tblEmployee AddEmployee(tblEmployee employee)
        {
            try
            {
                using (CompanyDataEntities1 context = new CompanyDataEntities1())
                {

                    tblEmployee newEmployee = new tblEmployee();
                    //newEmployee.UserID = admin.UserID;
                    //newEmployee.ExpiryDate = admin.ExpiryDate;
                    //newEmployee.AdministratorType = admin.AdministratorType;

                    //context.tblAdmins.Add(newAdmin);
                    //context.SaveChanges();

                    return newEmployee;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblEmployee GetEmployeeByUserId(int userId)
        {
            try
            {
                using (CompanyDataEntities1 context = new CompanyDataEntities1())
                {


                    tblEmployee employee = (from x in context.tblEmployees
                                      where x.UserID == userId
                                      select x).First();

                    return employee;
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
