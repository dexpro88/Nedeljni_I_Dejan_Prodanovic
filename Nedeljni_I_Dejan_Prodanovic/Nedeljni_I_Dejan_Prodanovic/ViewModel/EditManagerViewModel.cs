using Nedeljni_I_Dejan_Prodanovic.Commands;
using Nedeljni_I_Dejan_Prodanovic.Model;
using Nedeljni_I_Dejan_Prodanovic.Service;
using Nedeljni_I_Dejan_Prodanovic.Utility;
using Nedeljni_I_Dejan_Prodanovic.Validation;
using Nedeljni_I_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_I_Dejan_Prodanovic.ViewModel
{
    class EditManagerViewModel:ViewModelBase
    {
        EditManager view;
        IUserService userService;
        IManagerService managerService;
        string oldMail;
        string oldJMBG;
        string oldUserName;

        public EditManagerViewModel(EditManager editManagerView, tblManager managerLogedIn)
        {
            view = editManagerView;


            userService = new UserService();
            managerService = new ManagerService();
            User = new tblUser();
            Manager = managerLogedIn;
            ManagerToEdit = managerService.GetvwManager(Manager.ManagerID);

            oldMail = ManagerToEdit.Email;
            oldJMBG = ManagerToEdit.JMBG;
            oldUserName = ManagerToEdit.Username;
        }


        private string selectedSector;
        public string SelectedSector
        {
            get
            {
                return selectedSector;
            }
            set
            {
                selectedSector = value;
                OnPropertyChanged("SelectedSector");
            }
        }





        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string gender = "male";
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private string martialStatus = "married";
        public string MartialStatus
        {
            get { return martialStatus; }
            set
            {
                martialStatus = value;
                OnPropertyChanged("MartialStatus");
            }
        }
        private tblManager manager;
        public tblManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private vwManager managerToEdit;
        public vwManager ManagerToEdit
        {
            get
            {
                return managerToEdit;
            }
            set
            {
                managerToEdit = value;
                OnPropertyChanged("ManagerToEdit");
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(SaveExecute, CanSaveExecute);
                }
                return save;
            }
        }

        private void SaveExecute(object parameter)
        {
            try
            {
                DateTime dateOfBirth;
                if (!ValidationClass.JMBGisValid(ManagerToEdit.JMBG, out dateOfBirth))
                {
                    MessageBox.Show("JMBG is not valid");
                    return;
                }

                int age = ValidationClass.CountAge(dateOfBirth);
                if (age < 18)
                {
                    string str1 = string.Format("JMBG is not valid\nManager has to be at least 18 years old");
                    MessageBox.Show(str1);
                    return;
                }

                tblUser userInDb = userService.GetUserByUserName(ManagerToEdit.Username);

                if (userInDb != null)
                {
                    if (userInDb.Username.Equals(oldUserName))
                    {

                    }
                    else
                    {
                        string str1 = string.Format("User with this username exists\n" +
                       "Enter another username");
                        MessageBox.Show(str1);
                        return;
                    }
                   
                }

                userInDb = userService.GetUserByJMBG(ManagerToEdit.JMBG);

                if (userInDb != null)
                {
                    if (userInDb.JMBG.Equals(oldJMBG))
                    {

                    }
                    else
                    {
                        string str1 = string.Format("User with this JMBG exists\n" +
                        "Enter another JMBG");
                        MessageBox.Show(str1);
                        return;
                    }
                    
                }

                if (!ValidationClass.IsValidEmail(ManagerToEdit.Email))
                {
                    MessageBox.Show("Email is not valid");
                    return;
                }

                tblManager managerInDb = managerService.GetManagerByEmail(ManagerToEdit.Email);

                if (managerInDb != null)
                {
                    if (managerInDb.Email.Equals(oldMail))
                    {

                    }else
                    {
                        string str1 = string.Format("Manager with this email exists\n" +
                       "Enter another email");
                        MessageBox.Show(str1);
                        return;
                    }
                   
                }
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;

                string encryptedString = EncryptionHelper.Encrypt(password);

                tblUser newUser = CreateUser(ManagerToEdit);
                newUser.Password = encryptedString;

                //string str3 = string.Format("{0} {1}", newUser.UserID,newUser.FirstName);
                //MessageBox.Show(str3);

                userService.EditUser(newUser);

                string reservePassword = string.Format("{0}WPF", Manager.ReservePassword);

                tblManager newManager = new tblManager();

                newManager.ManagerID = ManagerToEdit.ManagerID;
                newManager.ReservePassword = reservePassword;
                newManager.Email = ManagerToEdit.Email;
                newManager.OfficeNumber = ManagerToEdit.OfficeNumber;


                managerService.EditManager(newManager);               
	   
                string str = string.Format("You succesfully edited you profile");
                MessageBox.Show(str);

                //ManagerMainView managerMainView = new ManagerMainView(Manager);
                //managerMainView.Show();
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (String.IsNullOrEmpty(ManagerToEdit.FirstName) || String.IsNullOrEmpty(ManagerToEdit.LastName)
                || String.IsNullOrEmpty(ManagerToEdit.JMBG) || String.IsNullOrEmpty(ManagerToEdit.Residence)
                || String.IsNullOrEmpty(ManagerToEdit.Username) || parameter as PasswordBox == null
                || String.IsNullOrEmpty(ManagerToEdit.Email)  
                || String.IsNullOrEmpty(ManagerToEdit.OfficeNumber)

                || String.IsNullOrEmpty((parameter as PasswordBox).Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {

                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        tblUser CreateUser(vwManager employee)
        {
            tblUser user = new tblUser();
            user.UserID = employee.UserID;
            user.FirstName = employee.FirstName;
            user.LastName = employee.LastName;
            user.JMBG = employee.JMBG;
            user.Gender = employee.Gender;
            user.Residence = employee.Residence;
            user.MaritalStatus = employee.MaritalStatus;
            user.Username = employee.Username;
           
            return user;
           
	 
        }
    }
}
