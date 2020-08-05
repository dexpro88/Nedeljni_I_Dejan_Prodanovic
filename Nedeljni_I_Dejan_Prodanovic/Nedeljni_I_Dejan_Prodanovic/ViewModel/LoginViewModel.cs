using Nedeljni_I_Dejan_Prodanovic.Commands;
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
    class LoginViewModel:ViewModelBase
    {
        LoginView view;

        public LoginViewModel(LoginView loginView)
        {
            view = loginView;
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
    }
}
