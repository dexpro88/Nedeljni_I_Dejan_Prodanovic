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
        ISectorService sectorService;


        public EmployeeRegisterViewModel(EmployeeRegisterView employeeRegisterView)
        {
            view = employeeRegisterView;
            qualifications = new List<string>() { "I", "II", "III", "IV", "V", "VI", "VII" };

            userService = new UserService();
            sectorService = new SectorService();

            User = new tblUser();
            Employee = new tblEmployee();
            SectorList = sectorService.GetSectors();
            if (SectorList.Count==0)
            {
                ViewNoSectorMessage = Visibility.Visible;
              
            }
            else
            {
                ViewNoSectorMessage = Visibility.Hidden;
                
            }
        }



        private Visibility viewNoSectorMessage;
        public Visibility ViewNoSectorMessage
        {
            get
            {
                return viewNoSectorMessage;
            }
            set
            {
                viewNoSectorMessage = value;
                OnPropertyChanged("ViewNoSectorMessage");
            }
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

        private List<tblPosition> positionList;
        public List<tblPosition> PositionList
        {
            get
            {
                return positionList;
            }
            set
            {
                positionList = value;
                OnPropertyChanged("PositionList");
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
                    string str1 = string.Format("JMBG is not valid\nEmployee has " +
                        "to be at least 18 years old");
                    MessageBox.Show(str1);
                    return;
                }

                tblUser userInDb = userService.GetUserByUserName(User.Username);

                if (userInDb != null)
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
