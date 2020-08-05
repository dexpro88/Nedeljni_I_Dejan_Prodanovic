using Nedeljni_I_Dejan_Prodanovic.Commands;
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
    class PredifinedAccountViewModel:ViewModelBase
    {
        PredifinedAccount view;
        IAdminService adminService;
      

     
        public PredifinedAccountViewModel(PredifinedAccount predifinedAccOpen)
        {
            view = predifinedAccOpen;
            adminService = new AdminService();
          
        }
      
    
        //private tblProduct selectetProduct;
        //public tblProduct SelectetProduct
        //{
        //    get
        //    {
        //        return selectetProduct;
        //    }
        //    set
        //    {
        //        selectetProduct = value;
        //        OnPropertyChanged("SelectetProduct");
        //    }
        //}
        //private List<tblProduct> productList;
        //public List<tblProduct> ProductList
        //{
        //    get
        //    {
        //        return productList;
        //    }
        //    set
        //    {
        //        productList = value;
        //        OnPropertyChanged("ProductList");
        //    }
        //}
        
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



       

        private ICommand addAdmin;
        public ICommand AddAdmin
        {
            get
            {
                if (addAdmin == null)
                {
                    addAdmin = new RelayCommand(param => AddAdminExecute(),
                        param => CanAddAdminExecute());
                }
                return addAdmin;
            }
        }

        private void AddAdminExecute()
        {
            try
            {
                AddAdmin addAdmin = new AddAdmin();
                addAdmin.Show();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddAdminExecute()
        {

            return true;
        }




    }
}
