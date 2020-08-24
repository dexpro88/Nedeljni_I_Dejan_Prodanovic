using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    class EmployeeService : IEmployeeService
    {
        public tblEmployee AddEmployee(tblEmployee employee)
        {
            try
            {
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {

                    tblEmployee newEmployee = new tblEmployee();
                    newEmployee.EmployeeID = employee.EmployeeID;
                    newEmployee.YearsOfService = employee.YearsOfService;
                    newEmployee.UserID = employee.UserID;
                    newEmployee.SectorID = employee.SectorID;
                    newEmployee.PositionID = employee.PositionID;


                    newEmployee.ProfessionalQualifications = employee.ProfessionalQualifications;
                    newEmployee.ManagerID = employee.ManagerID;


                    context.tblEmployees.Add(newEmployee);
                    context.SaveChanges();

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
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
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

        public void DeleteEmployee(int employeeId)
        {
            try
            {
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {
                    tblEmployee employeeToDelete = (from u in context.tblEmployees
                                                    where u.EmployeeID == employeeId
                                                    select u).First();


                    context.tblEmployees.Remove(employeeToDelete);

                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public vwEmployee2 GetvwEmployeeByEmployeeId(int employeeId)
        {
            try
            {
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {


                    vwEmployee2 employee = (from x in context.vwEmployee2
                                            where x.EmployeeID == employeeId
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

        public tblEmployee GetEmployeeByEmployeeId(int employeeId)
        {
            try
            {
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {


                    tblEmployee employee = (from x in context.tblEmployees
                                            where x.EmployeeID == employeeId
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

        public tblRequestForChange AddRequest(tblRequestForChange request)
        {
            try
            {
               
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {

                   
                    //newRequest.OldEmployeeID = employeeOld.EmployeeID;
                    //newRequest.NewEmployeeID = employeeNew.EmployeeID;
                    //newRequest.CreationDate = DateTime.Today;

                    context.tblRequestForChanges.Add(request);
                    context.SaveChanges();

                    return request;

                    //tblRequestForChange req = (from x in context.tblRequestForChanges
                    //                        where x.ID == 1
                    //                        select x).First();

                    //MessageBox.Show(req.ID.ToString());
                    //return null;

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