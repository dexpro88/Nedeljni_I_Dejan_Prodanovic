using Nedeljni_I_Dejan_Prodanovic.Commands;
using Nedeljni_I_Dejan_Prodanovic.Events;
using Nedeljni_I_Dejan_Prodanovic.Model;
using Nedeljni_I_Dejan_Prodanovic.Service;
using Nedeljni_I_Dejan_Prodanovic.Utility;
using Nedeljni_I_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_I_Dejan_Prodanovic.ViewModel
{
    class LoginViewModel:ViewModelBase
    {
        LoginView view;
        IUserService userService;
        IAdminService adminService;
        ManagerAccesClass managerAcces;

        public LoginViewModel(LoginView loginView)
        {
            view = loginView;
            userService = new UserService();
            adminService = new AdminService();

            managerAcces = new ManagerAccesClass();
            managerAcces.ApplicationStarted += WriteRandomStrToFile;

            managerAcces.WriteToFile();
        }

        private string userName;
        public string UserName
        {

            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string someProperty]
        {
            get
            {

                return string.Empty;
            }
        }



        private ICommand submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (submitCommand == null)
                {
                    submitCommand = new RelayCommand(Submit);
                    return submitCommand;
                }
                return submitCommand;
            }
        }

        void Submit(object obj)
        {

            string password = (obj as PasswordBox).Password;

          

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Wrong user name or password");
                return;
            }
            if (UserName.Equals("WPFMaster") &&
                password.Equals("WPFAccess"))
            {
                PredifinedAccount predifinedAccount = new PredifinedAccount();
                view.Close();
                predifinedAccount.Show();
                return;
            }

            string encryptedString = EncryptionHelper.Encrypt(password);
            
            tblUser user = userService.GetUserByUserNameAndPassword(UserName, encryptedString);
            if (user!=null)
            {
                tblAdmin admin = adminService.GetAdminByUserId(user.UserID);

                if (admin!=null)
                {
                    AdminMainView adminMainView = new AdminMainView();
                    adminMainView.Show();
                    view.Close();
                }
            }
            //else if (UserName.Equals(UserConstants.STOREKEEPER_USER_NAME) &&
            //    password.Equals(UserConstants.STOREKEEPER_PASSWORD))
            //{
            //    StorekeeperMainView storekeeperView = new StorekeeperMainView();
            //    view.Close();
            //    storekeeperView.Show();

            //}
            else
            {
                MessageBox.Show("Wrong username or password");

            }


        }

        private ICommand register;
        public ICommand Register
        {
            get
            {
                if (register == null)
                {
                    register = new RelayCommand(param => RegisterExecute(), param => CanRegisterExecute());
                }
                return register;
            }
        }

        private void RegisterExecute()
        {
            try
            {
                RegisterView register = new RegisterView();
                register.Show();
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanRegisterExecute()
        {

            return true;
        }

        void WriteRandomStrToFile(object source,string radnomStr)
        {
            using (StreamWriter sw = new StreamWriter(@"..\..\ManagerAccess.txt"))
            {
                
                    sw.WriteLine(radnomStr);
                 
            }
        }
    }
}
