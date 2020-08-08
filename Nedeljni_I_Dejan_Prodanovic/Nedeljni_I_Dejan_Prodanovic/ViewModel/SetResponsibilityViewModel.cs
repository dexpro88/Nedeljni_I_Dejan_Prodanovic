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
    class SetResponsibilityViewModel:ViewModelBase
    {
        SetResponsibilityView view;
        
        IManagerService menagerService;

        public SetResponsibilityViewModel(SetResponsibilityView setResponsibilityView,
            vwManager managerLogedIn)
        {
            view = setResponsibilityView;
            ResList = new List<string>() { "1", "2", "3" };

            menagerService = new ManagerService();
            Manager = managerLogedIn;            
        }
       
        private List<string> resList;
        public List<string> ResList
        {
            get
            {
                return resList;
            }
            set
            {
                resList = value;
                OnPropertyChanged("ResList");
            }
        }

        private string responsibility;
        public string Responsibility
        {
            get
            {
                return responsibility;
            }
            set
            {
                responsibility = value;
                OnPropertyChanged("Responsibility");
            }
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


        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(SaveExecute, CanSaveExecute);
                }
                return save;
            }
        }

        private void SaveExecute(object parameter)
        {
            try
            {     
                Manager.ResponsibilityLevel = Responsibility;
                MessageBox.Show(Manager.ResponsibilityLevel);
                menagerService.SetResponsibility(Manager);

                string str1 = string.Format("You set manager responsibility to {0}", 
                    Responsibility);
                MessageBox.Show(str1);
                
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (string.IsNullOrEmpty(Responsibility))
            {
                return false;
            }
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
