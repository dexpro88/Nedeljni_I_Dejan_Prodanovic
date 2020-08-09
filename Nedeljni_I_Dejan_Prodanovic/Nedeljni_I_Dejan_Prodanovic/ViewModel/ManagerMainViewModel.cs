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
    class ManagerMainViewModel:ViewModelBase
    {

        ManagerMainView view;
        IManagerService managerService;

        public ManagerMainViewModel(ManagerMainView managerMainView, tblManager managerLogedIn)
        {

            Manager = managerLogedIn;
            view = managerMainView;
            managerService = new ManagerService();

            if (managerService.GetEmployeesOfManager(Manager.ManagerID).Count == 0)
            {
                viewEmployeesSign = Visibility.Hidden;
            }
            else
            {
                viewEmployeesSign = Visibility.Visible;

            }
            if (Manager.ResponsibilityLevel.Equals("2"))
            {
                ViewAddPosition = Visibility.Visible;
            }
            else
            {
                ViewAddPosition = Visibility.Hidden;
            }
        }


        private Visibility viewEmployeesSign;
        public Visibility ViewEmployeesSign
        {
            get
            {
                return viewEmployeesSign;
            }
            set
            {
                viewEmployeesSign = value;
                OnPropertyChanged("ViewEmployeesSign");
            }
        }

        private Visibility viewAddPosition;
        public Visibility ViewAddPosition
        {
            get
            {
                return viewAddPosition;
            }
            set
            {
                viewAddPosition = value;
                OnPropertyChanged("ViewAddPosition");
            }
        }

        private Visibility viewMainMenu = Visibility.Visible;
        public Visibility ViewMainMenu
        {
            get
            {
                return viewMainMenu;
            }
            set
            {
                viewMainMenu = value;
                OnPropertyChanged("ViewMainMenu");
            }
        }

        private tblManager manager;
        public tblManager Manager
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


        private ICommand addPosition;
        public ICommand AddPosition
        {
            get
            {
                if (addPosition == null)
                {
                    addPosition = new RelayCommand(param => AddPositionExecute(),
                        param => CanAddPositionExecute());
                }
                return addPosition;
            }
        }

        private void AddPositionExecute()
        {
            try
            {
                AddPosition addPosition = new AddPosition();
                addPosition.ShowDialog();
                //PositionList = positionService.GetPositions();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddPositionExecute()
        {
            return true;
        }

        private ICommand showPositions;
        public ICommand ShowPositions
        {
            get
            {
                if (showPositions == null)
                {
                    showPositions = new RelayCommand(param => ShowPositionsExecute(),
                        param => CanShowPositionsExecute());
                }
                return showPositions;
            }
        }

        private void ShowPositionsExecute()
        {
            try
            {
                PositionsForManager positionsView = new PositionsForManager();
                positionsView.ShowDialog();
                //PositionList = positionService.GetPositions();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowPositionsExecute()
        {
            return true;
        }

        private ICommand showEmployees;
        public ICommand ShowEmployees
        {
            get
            {
                if (showEmployees == null)
                {
                    showEmployees = new RelayCommand(param => ShowEmployeesExecute(), 
                        param => CanShowEmployeesExecute());
                }
                return showEmployees;
            }
        }

        private void ShowEmployeesExecute()
        {
            try
            {
                ShowEmployees showEmployees = new ShowEmployees(Manager);
                showEmployees.Show();
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowEmployeesExecute()
        {
            return true;
        }

        private ICommand addProject;
        public ICommand AddProject
        {
            get
            {
                if (addProject == null)
                {
                    addProject = new RelayCommand(param => AddProjectExecute(),
                        param => CanAddProjectExecute());
                }
                return addProject;
            }
        }

        private void AddProjectExecute()
        {
            try
            {
                AddProject addEmployee = new AddProject();
                addEmployee.ShowDialog();
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddProjectExecute()
        {
            return true;
        }
        private ICommand editManager;
        public ICommand EditManager
        {
            get
            {
                if (editManager == null)
                {
                    editManager = new RelayCommand(param =>EditManagerExecute(),
                        param => CanEditManagerExecute());
                }
                return editManager;
            }
        }

        private void EditManagerExecute()
        {
            try
            {
                EditManager editManager = new EditManager(Manager);
                editManager.ShowDialog();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditManagerExecute()
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
