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
        IEmployeeService employeeService;
        IPositionService positionService;
        IManagerService managerService;

        public EmployeeRegisterViewModel(EmployeeRegisterView employeeRegisterView)
        {
            view = employeeRegisterView;
            qualifications = new List<string>() { "I", "II", "III", "IV", "V", "VI", "VII" };

            userService = new UserService();
            sectorService = new SectorService();
            employeeService = new EmployeeService();
            positionService = new PositionService();
            managerService = new ManagerService();

            User = new tblUser();
            Employee = new tblEmployee();
            SectorList = sectorService.GetSectors();
            PositionList = positionService.GetPositions();
            
            //if (SectorList.Count==0)
            //{
            //    ViewNoSectorMessage = Visibility.Visible;
              
            //}
            //else
            //{
            //    ViewNoSectorMessage = Visibility.Hidden;
                
            //}
          
        }



        //private Visibility viewNoSectorMessage;
        //public Visibility ViewNoSectorMessage
        //{
        //    get
        //    {
        //        return viewNoSectorMessage;
        //    }
        //    set
        //    {
        //        viewNoSectorMessage = value;
        //        OnPropertyChanged("ViewNoSectorMessage");
        //    }
        //}

     



        private tblSector selectedSector;
        public tblSector SelectedSector
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

        private tblPosition selectedPosition;
        public tblPosition SelectedPosition
        {
            get
            {
                return selectedPosition;
            }
            set
            {
                selectedPosition = value;
                OnPropertyChanged("SelectedPosition");
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

        private string qualification;
        public string Qualification
        {
            get
            {
                return qualification;
            }
            set
            {
                qualification = value;
                OnPropertyChanged("Qualification");
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

                Employee.SectorID = SelectedSector.SectorID;
                if (SelectedPosition!=null)
                {
                    Employee.PositionID = SelectedPosition.PositionID;

                }
                
                Employee.ProfessionalQualifications = Qualification;
                List<vwManager>managers = managerService.GetManagers();
                var idList = managers.Select(item => item.ManagerID).ToList();

                Random random = new Random();
                int index = random.Next(idList.Count);
                int managerId = idList[index];
                Employee.ManagerID = managerId;

                employeeService.AddEmployee(Employee);
                

                //string str = string.Format("You registered as employee\nYour manager is");
                MessageBox.Show("You registered as employee");
                LoginView loginView = new LoginView();
                loginView.Show();
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
                || String.IsNullOrEmpty(Qualification)
                || String.IsNullOrEmpty(Employee.YearsOfService) 
                || String.IsNullOrEmpty(User.Username) || parameter as PasswordBox == null
                || SelectedSector == null || String.IsNullOrEmpty((parameter as PasswordBox).Password)
                || SectorList.Count==0)
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
                RegisterView registerView = new RegisterView();
                registerView.Show();
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
