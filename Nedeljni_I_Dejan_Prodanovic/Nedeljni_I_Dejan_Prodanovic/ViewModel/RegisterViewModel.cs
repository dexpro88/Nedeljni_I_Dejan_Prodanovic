using Nedeljni_I_Dejan_Prodanovic.Commands;
using Nedeljni_I_Dejan_Prodanovic.InputDialog;
using Nedeljni_I_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.IO;
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

        bool canAsManager = true;

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

        private ICommand asManager;
        public ICommand AsManager
        {
            get
            {
                if (asManager == null)
                {
                    asManager = new RelayCommand(param => AsManagerExecute(),
                        param => CanAsManagerExecute());
                }
                return asManager;
            }
        }

        private void AsManagerExecute()
        {
            
            string passwordFromFile = ReadPasswordFromFile();
          
            int tryCounter = 3;
            try
            {
                ManagerRegisterView registerView = new ManagerRegisterView();

                MessageBoxResult result = MessageBox.Show("Are you sure that you want to register as manager?" +
                        "", "My App",
                       MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        for (int i = 0; i < 3; i++)
                        {
                            InputDialogSample inputDialog = new InputDialogSample("Please enter password from file " +
                           "ManagerAccess.txt:", "");
                            if (inputDialog.ShowDialog() == true)
                                if (inputDialog.Answer.Equals(passwordFromFile))
                                {
                                    registerView.ShowDialog();
                                    LoginView loginView = new LoginView();
                                    loginView.Show();
                                    view.Close();
                                    return;
                                }
                                else
                                {
                                    tryCounter--;
                                    if (tryCounter!=0)
                                    {
                                        MessageBox.Show("Wrong password. You have " + tryCounter + " more attempts");

                                    }
                                  
                                    if (tryCounter==0)
                                    {
                                        MessageBox.Show("You can't create manager account.\n" +
                                            "Create employee account.");
                                        canAsManager = false;
                                        EmployeeRegisterView employeeRegisterView =
                                            new EmployeeRegisterView();
                                        employeeRegisterView.ShowDialog();
                                       
                                       
                                        return;
                                    }
                                }
                        }
                       
                           
                            break;
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAsManagerExecute()
        {
            if (!canAsManager)
            {
                return false;
            }
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

        string ReadPasswordFromFile()
        {
            try
            {
                
                using (StreamReader sr = new StreamReader(@"..\..\ManagerAccess.txt"))
                {
                    string line;
                   
                    while ((line = sr.ReadLine()) != null)
                    {
                        return line;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
