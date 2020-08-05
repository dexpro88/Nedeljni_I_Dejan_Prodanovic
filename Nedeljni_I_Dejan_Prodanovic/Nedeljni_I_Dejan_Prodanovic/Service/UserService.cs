﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nedeljni_I_Dejan_Prodanovic.Model;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    class UserService : IUserService
    {
        public tblUser AddUser(tblUser user)
        {
            try
            {
                using (CompanyDataEntities context = new CompanyDataEntities())
                {

                    tblUser newUser = new tblUser();
                    newUser.FirstName = user.FirstName;
                    newUser.LastName = user.LastName;
                    newUser.JMBG = user.JMBG;
                    newUser.Gender = user.Gender;
                    newUser.MaritalStatus = user.MaritalStatus;
                    newUser.Username = user.Username;
                    newUser.Password = user.Password;

                    context.tblUsers.Add(newUser);
                    context.SaveChanges();

                    return newUser;


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblUser GetUserByUserNameAndPassword(string username, string password)
        {
            try
            {
                using (CompanyDataEntities context = new CompanyDataEntities())
                {


                    tblUser user = (from x in context.tblUsers
                                    where x.Username == username
                                    && x.Password == password
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
    }
}
