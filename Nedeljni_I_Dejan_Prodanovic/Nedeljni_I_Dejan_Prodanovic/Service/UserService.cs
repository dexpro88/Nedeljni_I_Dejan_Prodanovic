using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Nedeljni_I_Dejan_Prodanovic.Model;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    class UserService : IUserService
    {
        public tblUser AddUser(tblUser user)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {

                    tblUser newUser = new tblUser();
                    newUser.FirstName = user.FirstName;
                    newUser.LastName = user.LastName;
                    newUser.JMBG = user.JMBG;
                    newUser.Gender = user.Gender;
                    newUser.Residence = user.Residence;
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
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {


                    tblUser user = (from x in context.tblUsers
                                    where x.Username.Equals(username)
                                    && x.Password.Equals(password)
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

        public tblUser GetUserByUserName(string username)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {


                    tblUser user = (from x in context.tblUsers
                                    where x.Username.Equals(username)
                                   
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

        public tblUser GetUserByJMBG(string JMBG)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {


                    tblUser user = (from x in context.tblUsers
                                    where x.JMBG.Equals(JMBG)

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

        public void DeleteUser(int userId)
        {
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {
                    tblUser userToDelete = (from u in context.tblUsers
                                                where u.UserID == userId
                                            select u).First();


                    context.tblUsers.Remove(userToDelete);

                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public void EditUser(tblUser user)
        {
            
            try
            {
                using (CompanyDataEntities2 context = new CompanyDataEntities2())
                {
                 
                    tblUser userToEdit = (from u in context.tblUsers
                                          where u.UserID == user.UserID
                                          select u).First();              
               
                    userToEdit.FirstName = user.FirstName;
                    userToEdit.LastName = user.LastName;
                    userToEdit.JMBG = user.JMBG;
                    userToEdit.Gender = user.Gender;
                    userToEdit.Residence = user.Residence;
                    userToEdit.MaritalStatus = user.MaritalStatus;
                    userToEdit.Username = user.Username;
                    userToEdit.Password = user.Password;

                    
                    context.SaveChanges();

                

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                
            }
        }
    }
}
