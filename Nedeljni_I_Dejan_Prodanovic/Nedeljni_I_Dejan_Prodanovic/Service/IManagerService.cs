using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    interface IManagerService
    {
        tblManager AddManager(tblManager manager);
        tblManager GetManagerByEmail(string email);
        tblManager GetManagerByUserId(int userId);
        List<vwManager> GetManagers();
        void DeleteManager(int managerId);
        vwManager SetResponsibility(vwManager manager );
        List<vwEmployee2> GetEmployeesOfManager(int managerId);
    }
}
