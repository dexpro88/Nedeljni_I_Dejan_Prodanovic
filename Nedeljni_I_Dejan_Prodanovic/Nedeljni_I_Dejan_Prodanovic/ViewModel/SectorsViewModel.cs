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
    class SectorsViewModel : ViewModelBase
    {
        Sectors view;

        ISectorService sectorService;

        public SectorsViewModel(Sectors sectors)
        {
            view = sectors;


            sectorService = new SectorService();

            Sector = new tblSector();
            SectorList = sectorService.GetSectors();
        }

        private tblSector sector;
        public tblSector Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
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
        private ICommand addSector;
        public ICommand AddSector
        {
            get
            {
                if (addSector == null)
                {
                    addSector = new RelayCommand(param => AddSectorExecute(), param => CanAddSectorExecute());
                }
                return addSector;
            }
        }

        private void AddSectorExecute()
        {
            try
            {
                AddSector addSector = new AddSector();
                addSector.ShowDialog();
                SectorList = sectorService.GetSectors();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddSectorExecute()
        {
            return true;
        }


        private ICommand deleteSector;
        public ICommand DeleteSector
        {
            get
            {
                if (deleteSector == null)
                {
                    deleteSector = new RelayCommand(param => DeleteSectorExecute(), 
                        param => CanDeleteSectorExecute());
                }
                return deleteSector;
            }
        }

        private void DeleteSectorExecute()
        {
            try
            {
                if (Sector != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to delete this sector?" +
                        "", "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int sectorId = Sector.SectorID;

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            sectorService.DeleteSector(sectorId);
                            SectorList = sectorService.GetSectors().ToList();
                            
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteSectorExecute()
        {
            if (Sector == null)
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
