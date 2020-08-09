using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    interface IEmployeeService
    {
        tblEmployee AddEmployee(tblEmployee employee);
        tblEmployee GetEmployeeByUserId(int userId);
        void DeleteEmployee(int employeeId);
        vwEmployee2 GetvwEmployeeByEmployeeId(int employeeId);
        tblEmployee GetEmployeeByEmployeeId(int employeeId);
    }
}
