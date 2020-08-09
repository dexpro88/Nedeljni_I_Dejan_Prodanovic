using Nedeljni_I_Dejan_Prodanovic.Commands;
using Nedeljni_I_Dejan_Prodanovic.Model;
using Nedeljni_I_Dejan_Prodanovic.Service;
using Nedeljni_I_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_I_Dejan_Prodanovic.ViewModel
{
    class EmployeeMainViewModel:ViewModelBase
    {

        EmployeeMainView view;
       

        public EmployeeMainViewModel(EmployeeMainView employeeMainView, tblEmployee employeeLogedIn)
        {

            //Manager = managerLogedIn;
            view = employeeMainView;
            Employee = employeeLogedIn;
        }


        //private Visibility viewEmployeesSign;
        //public Visibility ViewEmployeesSign
        //{
        //    get
        //    {
        //        return viewEmployeesSign;
        //    }
        //    set
        //    {
        //        viewEmployeesSign = value;
        //        OnPropertyChanged("ViewEmployeesSign");
        //    }
        //}

       

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




        

        private ICommand editProfile;
        public ICommand EditProfile
        {
            get
            {
                if (editProfile == null)
                {
                    editProfile = new RelayCommand(param => EditProfileExecute(),
                        param => CanEditProfileExecute());
                }
                return editProfile;
            }
        }

        private void EditProfileExecute()
        {
            try
            {
                EditEmployee editEmployee = new EditEmployee(Employee);
                editEmployee.ShowDialog();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditProfileExecute()
        {
            return true;
        }

        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logout;
            }
        }

        private void LogoutExecute()
        {
            try
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanLogoutExecute()
        {
            return true;
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
