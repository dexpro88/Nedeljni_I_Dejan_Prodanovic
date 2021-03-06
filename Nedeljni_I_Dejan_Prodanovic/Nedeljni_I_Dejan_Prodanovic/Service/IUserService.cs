﻿using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    interface IUserService
    {
        tblUser AddUser(tblUser user);
        tblUser GetUserByUserNameAndPassword(string username, string password);
        tblUser GetUserByUserName(string username);
        tblUser GetUserByJMBG(string JMBG);
        void DeleteUser(int UserId);
        void EditUser(tblUser user);

    }
}
