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
    class AddAdminViewModel:ViewModelBase
    {
        AddAdmin view;
        IAdminService adminService;
        IUserService userService;

        public AddAdminViewModel(AddAdmin addAdminView)
        {
            view = addAdminView;
            
            AdministratorTypesList = new List<string>() { "Team", "System", "Local" };
            adminService = new AdminService();
            userService = new UserService();
            User = new tblUser();
            Admin = new tblAdmin();
        }

        private string selctedType;
        public string SelctedType
        {
            get
            {
                return selctedType;
            }
            set
            {
                selctedType = value;
                OnPropertyChanged("SelctedType");
            }
        }



        private List<string> administratorTypesList;
        public List<string> AdministratorTypesList
        {
            get
            {
                return administratorTypesList;
            }
            set
            {
                administratorTypesList = value;
                OnPropertyChanged("AdministratorTypesList");
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
        private tblAdmin admin;
        public tblAdmin Admin
        {
            get
            {
                return admin;
            }
            set
            {
                admin = value;
                OnPropertyChanged("Admin");
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(SaveExecute,   CanSaveExecute);
                }
                return save;
            }
        }

        private void SaveExecute(object parameter)
        {
            try
            {
                DateTime dateOfBirth;

                if (!ValidationClass.JMBGisValid(User.JMBG,out dateOfBirth))
                {
                    MessageBox.Show("JMBG is not valid");
                    return;
                }

                int age = ValidationClass.CountAge(dateOfBirth);
                if (age<18)
                {
                    MessageBox.Show("JMBG is not valid\nAdmin has to be at least 18 years old");
                    return;
                }
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;

                string encryptedString = EncryptionHelper.Encrypt(password);
                User.Gender = Gender;
                User.MaritalStatus = MartialStatus;
                User.Password = encryptedString;
                User = userService.AddUser(User);

                DateTime today = DateTime.Now;
                Admin.ExpiryDate = today.AddDays(7);
                Admin.AdministratorType = SelctedType;
                Admin.UserID = User.UserID;

                adminService.AddAdmin(Admin);
                string str = string.Format("You added new admin of type {0}", SelctedType);
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
                || String.IsNullOrEmpty(User.Username)|| parameter as PasswordBox == null
                || SelctedType==null)
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


        private ICommand chooseGender;
        public ICommand ChooseGender
        {
            get
            {
                if (chooseGender == null)
                {
                    chooseGender = new RelayCommand(ChooseGenderExecute, CanChooseGenderExecute);
                }
                return chooseGender;
            }
        }

        private void ChooseGenderExecute(object parameter)
        {
            Gender = (string)parameter;
        }

        private bool CanChooseGenderExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private ICommand chooseMartialStatus;
        public ICommand ChooseMartialStatus
        {
            get
            {
                if (chooseMartialStatus == null)
                {
                    chooseMartialStatus = new RelayCommand(ChooseMartialStatusExecute, CanChooseMartialStatusExecute);
                }
                return chooseMartialStatus;
            }
        }

        private void ChooseMartialStatusExecute(object parameter)
        {
            MartialStatus = (string)parameter;
        }

        private bool CanChooseMartialStatusExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
