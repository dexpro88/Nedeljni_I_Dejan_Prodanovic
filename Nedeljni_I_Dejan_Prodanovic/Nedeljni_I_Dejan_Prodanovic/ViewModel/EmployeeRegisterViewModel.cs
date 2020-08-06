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
    class EmployeeRegisterViewModel:ViewModelBase
    {
        EmployeeRegisterView view;
        IUserService userService;
        
       
        public EmployeeRegisterViewModel(EmployeeRegisterView employeeRegisterView)
        {
            view = employeeRegisterView;
            qualifications = new List<string>() { "I", "II", "III", "IV", "V", "VI", "VII" };

            userService = new UserService();
            User = new tblUser();
            Employee = new tblEmployee();
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



        private List<string> qualifications;
        public List<string> Qualifications
        {
            get
            {
                return qualifications;
            }
            set
            {
                qualifications = value;
                OnPropertyChanged("Qualifications");
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
        private tblEmployee employee;
        public tblEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private List<tblSector> sectorList;
        public List<tblSector> SectorList
        {
            get
            {
                return sectorList;
            }
            set
            {
                sectorList = value;
                OnPropertyChanged("SectorList");
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

                //DateTime today = DateTime.Now;
                //Admin.ExpiryDate = today.AddDays(7);
                //Admin.AdministratorType = SelctedType;
                //Admin.UserID = User.UserID;

                //adminService.AddAdmin(Admin);
                //string str = string.Format("You added new admin of type {0}", SelctedType);
                //MessageBox.Show(str);
                //view.Close();

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
                || String.IsNullOrEmpty(User.Username)|| String.IsNullOrEmpty(User.Residence)
                ||String.IsNullOrEmpty(Employee.YearsOfService) 
                || String.IsNullOrEmpty(User.Username) || parameter as PasswordBox == null
                || SelectedSector == null|| String.IsNullOrEmpty((parameter as PasswordBox).Password))
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
