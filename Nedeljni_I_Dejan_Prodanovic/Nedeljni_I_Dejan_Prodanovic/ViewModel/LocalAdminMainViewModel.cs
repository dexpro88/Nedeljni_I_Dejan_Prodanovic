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
    class LocalAdminMainViewModel:ViewModelBase
    {
        LocaldAminMainView view;

        IManagerService managerService;

        public LocalAdminMainViewModel(LocaldAminMainView adminView)
        {
            view = adminView;


            managerService = new ManagerService();
            Manager = new vwManager();
           
            ManagerList = managerService.GetManagers();
        }

        private vwManager manager;
        public vwManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private List<vwManager> managerList;
        public List<vwManager> ManagerList
        {
            get
            {
                return managerList;
            }
            set
            {
                managerList = value;
                OnPropertyChanged("ManagerList");
            }
        }
        private ICommand setResponsibility;
        public ICommand SetResponsibility
        {
            get
            {
                if (setResponsibility == null)
                {
                    setResponsibility = new RelayCommand(param =>SetResponsibilityExecute(),
                        param => CanSetResponsibilityExecute());
                }
                return setResponsibility;
            }
        }

        private void SetResponsibilityExecute()
        {
            try
            {
               
                SetResponsibilityView setResponsibility = new SetResponsibilityView(Manager);
                setResponsibility.ShowDialog();
                ManagerList = managerService.GetManagers();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSetResponsibilityExecute()
        {
            return true;
        }


        private ICommand deleteManager;
        public ICommand DeleteManager
        {
            get
            {
                if (deleteManager == null)
                {
                    deleteManager = new RelayCommand(param => DeleteManagerExecute(),
                        param => CanDeleteManagerExecute());
                }
                return deleteManager;
            }
        }

        private void DeleteManagerExecute()
        {
            try
            {
                if (Manager != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to delete this manager?" +
                        "", "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int managerId = Manager.ManagerID;

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            managerService.DeleteManager(managerId);
                            ManagerList = managerService.GetManagers().ToList();

                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteManagerExecute()
        {
            if (Manager == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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
                AdminMainView adminView =
                    new AdminMainView(new tblAdmin() { AdministratorType = "System" });
                adminView.Show();
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
    }
}
