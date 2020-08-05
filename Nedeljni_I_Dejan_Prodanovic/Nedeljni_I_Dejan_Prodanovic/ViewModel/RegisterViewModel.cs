using Nedeljni_I_Dejan_Prodanovic.Commands;
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
    class RegisterViewModel:ViewModelBase
    {
        RegisterView view;
         



        public RegisterViewModel(RegisterView registerView)
        {
            view = registerView;
           

        }

        private ICommand asEmployee;
        public ICommand AsEmployee
        {
            get
            {
                if (asEmployee == null)
                {
                    asEmployee = new RelayCommand(param => AsEmployeeExecute(), param => CanAsEmployeeExecute());
                }
                return asEmployee;
            }
        }

        private void AsEmployeeExecute()
        {
            try
            {
                EmployeeRegisterView registerView = new EmployeeRegisterView();
                registerView.Show();
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAsEmployeeExecute()
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




        private ICommand back;
        public ICommand Back
        {
            get
            {
                if (back == null)
                {
                    back = new RelayCommand(param => BackExecute(), param => CanBackExecute());
                }
                return back;
            }
        }

        private void BackExecute()
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
        private bool CanBackExecute()
        {
            return true;
        }





        private ICommand addAdmin;
        public ICommand AddAdmin
        {
            get
            {
                if (addAdmin == null)
                {
                    addAdmin = new RelayCommand(param => AddAdminExecute(),
                        param => CanAddAdminExecute());
                }
                return addAdmin;
            }
        }

        private void AddAdminExecute()
        {
            try
            {
                AddAdmin addAdmin = new AddAdmin();
                addAdmin.Show();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddAdminExecute()
        {

            return true;
        }
    }
}
