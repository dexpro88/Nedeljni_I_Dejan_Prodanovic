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
    class ManagerRegisterViewModel:ViewModelBase
    {
        ManagerRegisterView view;
        IUserService userService;
        IManagerService managerService;

        public ManagerRegisterViewModel(ManagerRegisterView managerRegisterView)
        {
            view = managerRegisterView;
            

            userService = new UserService();
            managerService = new ManagerService();
            User = new tblUser();
            Manager = new tblManager();
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
                if (!ValidationClass.JMBGisValid(User.JMBG, out dateOfBirth))
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

                tblUser userInDb = userService.GetUserByUserName(User.Username);

                if (userInDb!=null)
                {
                    string str1 = string.Format("User with this username exists\n" +
                        "Enter another username");
                    MessageBox.Show(str1);
                    return;
                }

                userInDb = userService.GetUserByJMBG(User.JMBG);

                if (userInDb != null)
                {
                    string str1 = string.Format("User with this JMBG exists\n" +
                        "Enter another JMBG");
                    MessageBox.Show(str1);
                    return;
                }

                if (!ValidationClass.IsValidEmail(Manager.Email))
                {
                    MessageBox.Show("Email is not valid");
                    return;
                }

                tblManager managerInDb = managerService.GetManagerByEmail(Manager.Email);

                if (managerInDb != null)
                {
                    string str1 = string.Format("Manager with this emai exists\n" +
                        "Enter another email");
                    MessageBox.Show(str1);
                    return;
                }
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;

                string encryptedString = EncryptionHelper.Encrypt(password);
                User.Gender = Gender;
                User.MaritalStatus = MartialStatus;
                User.Password = encryptedString;
                User = userService.AddUser(User);

                string reservePassword = string.Format("{0}WPF", Manager.ReservePassword);
                Manager.ReservePassword = reservePassword;
                Manager.UserID = User.UserID;
                managerService.AddManager(Manager);
               

                string str = string.Format("You registered as manager");
                MessageBox.Show(str);
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (String.IsNullOrEmpty(User.FirstName) || String.IsNullOrEmpty(User.LastName)
                || String.IsNullOrEmpty(User.JMBG) || String.IsNullOrEmpty(User.Residence)
                || String.IsNullOrEmpty(User.Username) || parameter as PasswordBox == null
                || String.IsNullOrEmpty(Manager.Email) || String.IsNullOrEmpty(Manager.ReservePassword)
                || String.IsNullOrEmpty(Manager.OfficeNumber)
                 
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
    }
}
