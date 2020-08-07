using Nedeljni_I_Dejan_Prodanovic.Commands;
using Nedeljni_I_Dejan_Prodanovic.Model;
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
        
       
      
        public ManagerMainViewModel(ManagerMainView managerMainView, tblManager managerLogedIn)
        {

            Manager = managerLogedIn;
            

        }
       

        private Visibility viewAprovedOrder = Visibility.Hidden;
        public Visibility ViewAprovedOrder
        {
            get
            {
                return viewAprovedOrder;
            }
            set
            {
                viewAprovedOrder = value;
                OnPropertyChanged("ViewAprovedOrder");
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
