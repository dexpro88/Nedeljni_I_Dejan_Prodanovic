﻿using Nedeljni_I_Dejan_Prodanovic.Commands;
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
