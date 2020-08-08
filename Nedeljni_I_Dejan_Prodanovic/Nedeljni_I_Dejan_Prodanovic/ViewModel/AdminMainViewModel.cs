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
    class AdminMainViewModel:ViewModelBase
    {
        AdminMainView view;

        ISectorService sectorService;
        public AdminMainViewModel(AdminMainView adminMainView, tblAdmin adminLogedIn)
        {

            Admin = adminLogedIn;
            view = adminMainView;

            if (admin.AdministratorType.Equals("System"))
            {
                sectorService = new SectorService();
                IsSystemAdmin = Visibility.Visible;
                SectorList = sectorService.GetSectors();
            }
            else if(admin.AdministratorType.Equals("Team"))
            {
                IsTeamAdmin = Visibility.Visible;
                    
            }else if (admin.AdministratorType.Equals("Local"))
            {
                IsLocalAdmin = Visibility.Visible;
            }
        }

        private Visibility isSystemAdmin = Visibility.Hidden;
        public Visibility IsSystemAdmin
        {
            get
            {
                return isSystemAdmin;
            }
            set
            {
                isSystemAdmin = value;
                OnPropertyChanged("IsSystemAdmin");
            }
        }

        private Visibility isTeamAdmin = Visibility.Hidden;
        public Visibility IsTeamAdmin
        {
            get
            {
                return isTeamAdmin;
            }
            set
            {
                isTeamAdmin = value;
                OnPropertyChanged("IsTeamAdmin");
            }
        }

        private Visibility isLocalAdmin = Visibility.Hidden;
        public Visibility IsLocalAdmin
        {
            get
            {
                return isLocalAdmin;
            }
            set
            {
                isLocalAdmin = value;
                OnPropertyChanged("IsLocalAdmin");
            }
        }

        private tblAdmin admin;
        public tblAdmin Admin
        {
            get
            {
                return admin;
            }
            set
            {
                admin = value;
                OnPropertyChanged("Admin");
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

      
        private ICommand showSectors;
        public ICommand ShowSectors
        {
            get
            {
                if (showSectors == null)
                {
                    showSectors = new RelayCommand(param => ShowSectorsExecute(),
                        param => CanShowSectorsExecute());
                }
                return showSectors;
            }
        }

        private void ShowSectorsExecute()
        {
            try
            {
                Sectors sectors = new Sectors();
                sectors.Show();
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowSectorsExecute()
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
                Positions positions = new Positions();
                positions.Show();
                view.Close();
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
