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
    class EditEmployeeViewModel:ViewModelBase
    {
        EditEmployee view;
        IUserService userService;
        ISectorService sectorService;
        IEmployeeService employeeService;
        IPositionService positionService;
        IManagerService managerService;
        IRequestService requestService;
      
        string oldJMBG;
        string oldUserName;

        tblEmployee oldEmployee;

        public EditEmployeeViewModel(EditEmployee editEmployeeView,tblEmployee employeeLogedIn)
        {
            view = editEmployeeView;
            qualifications = new List<string>() { "I", "II", "III", "IV", "V", "VI", "VII" };

            userService = new UserService();
            sectorService = new SectorService();
            employeeService = new EmployeeService();
            positionService = new PositionService();
            managerService = new ManagerService();
            requestService = new RequestService();

            User = new tblUser();
            EmployeeToEdit = employeeService.GetvwEmployeeByEmployeeId(employeeLogedIn.EmployeeID);
            //Employee = new tblEmployee();
            SectorList = sectorService.GetSectors();
            PositionList = positionService.GetPositions();
            //MessageBox.Show(employeeLogedIn.EmployeeID.ToString());
            oldEmployee = employeeLogedIn;

            oldUserName = EmployeeToEdit.Username;
            oldJMBG = EmployeeToEdit.JMBG;
        }

         
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

        private vwEmployee2 employeeToEdit;
        public vwEmployee2 EmployeeToEdit
        {
            get
            {
                return employeeToEdit;
            }
            set
            {
                employeeToEdit = value;
                OnPropertyChanged("EmployeeToEdit");
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
        //private tblEmployee employee;
        //public tblEmployee Employee
        //{
        //    get
        //    {
        //        return employee;
        //    }
        //    set
        //    {
        //        employee = value;
        //        OnPropertyChanged("Employee");
        //    }
        //}





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

                if (!ValidationClass.JMBGisValid(EmployeeToEdit.JMBG, out dateOfBirth))
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

                tblUser userInDb = userService.GetUserByUserName(EmployeeToEdit.Username);

                if (userInDb != null)
                {
                    if (EmployeeToEdit.Username.Equals(oldUserName))
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

                userInDb = userService.GetUserByJMBG(EmployeeToEdit.JMBG);

                if (userInDb != null)
                {
                    if (EmployeeToEdit.JMBG.Equals(oldJMBG))
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
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;

                string encryptedString = EncryptionHelper.Encrypt(password);
                tblUser newUser = CreateUser(EmployeeToEdit);


                newUser.Password = encryptedString;
                newUser = userService.AddUser(newUser);

                EmployeeToEdit.SectorID = SelectedSector.SectorID;
                if (SelectedPosition != null)
                {
                    EmployeeToEdit.PositionID = SelectedPosition.PositionID;

                }

                EmployeeToEdit.ProfessionalQualifications = Qualification;

                tblEmployee newEmployee = CreateEmployee(EmployeeToEdit);
                newEmployee.UserID = newUser.UserID;
                newEmployee.ManagerID = EmployeeToEdit.ManagerID;
                newEmployee.IsNewRequest = true;

                newEmployee = employeeService.AddEmployee(newEmployee);

                tblRequestForChange newRequest = new tblRequestForChange();

                newRequest.OldEmployeeID = oldEmployee.EmployeeID;
                newRequest.NewEmployeeID = newEmployee.EmployeeID;
                newRequest.CreationDate = DateTime.Today;

                employeeService.AddRequest(newRequest);

                //string str = string.Format("You registered as employee\nYour manager is");
                MessageBox.Show("You send request for changes to your manager");
                 
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (String.IsNullOrEmpty(EmployeeToEdit.FirstName) || String.IsNullOrEmpty(EmployeeToEdit.LastName)
                || String.IsNullOrEmpty(EmployeeToEdit.JMBG) || String.IsNullOrEmpty(EmployeeToEdit.Residence)
                || String.IsNullOrEmpty(Qualification)
                || String.IsNullOrEmpty(EmployeeToEdit.YearsOfService)
                || String.IsNullOrEmpty(EmployeeToEdit.Username) || parameter as PasswordBox == null
                || SelectedSector == null || String.IsNullOrEmpty((parameter as PasswordBox).Password)
                || SectorList.Count == 0)
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

        tblUser CreateUser(vwEmployee2 employee)
        {
            tblUser user = new tblUser();
           
            user.FirstName = employee.FirstName;
            user.LastName = employee.LastName;
            user.JMBG = employee.JMBG;
            user.Gender = employee.Gender;
            user.Residence = employee.Residence;
            user.MaritalStatus = employee.MaritalStatus;
            user.Username = employee.Username;

            return user;
           
        }

        tblEmployee CreateEmployee(vwEmployee2 employee)
        {
            tblEmployee newEmployee = new tblEmployee();
             
            newEmployee.YearsOfService = employee.YearsOfService;
            newEmployee.Salary = employee.Salary;
            newEmployee.ProfessionalQualifications = employee.ProfessionalQualifications;
            newEmployee.SectorID = employee.SectorID;
            newEmployee.PositionID = employee.PositionID;
            

            return newEmployee;
        }
    }
}
