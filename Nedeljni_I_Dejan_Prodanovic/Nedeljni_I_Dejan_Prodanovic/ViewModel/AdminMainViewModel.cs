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
        ISectorService sectorService;
        public AdminMainViewModel(AdminMainView adminMainView, tblAdmin adminLogedIn)
        {

            Admin = adminLogedIn;

            if (admin.AdministratorType.Equals("System"))
            {
                sectorService = new SectorService();
                IsSystemAdmin = Visibility.Visible;
                SectorList = sectorService.GetSectors();
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
                addSector.Show();

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
    }
}
