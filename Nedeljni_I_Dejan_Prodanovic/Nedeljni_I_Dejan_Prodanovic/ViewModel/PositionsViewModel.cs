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
    class PositionsViewModel:ViewModelBase
    {
        Positions view;

        IPositionService positionService;

        public PositionsViewModel(Positions positions)
        {
            view = positions;


            positionService = new PositionService();

            Position = new tblPosition();
            PositionList = positionService.GetPositions();
        }

        private tblPosition position;
        public tblPosition Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        private List<tblPosition> positionList;
        public List<tblPosition> PositionList
        {
            get
            {
                return positionList;
            }
            set
            {
                positionList = value;
                OnPropertyChanged("PositionList");
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
                PositionList = positionService.GetPositions();

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

        private ICommand deletePosition;
        public ICommand DeletePosition
        {
            get
            {
                if (deletePosition == null)
                {
                    deletePosition = new RelayCommand(param => DeletePositionExecute(),
                        param => CanDeletePositionExecute());
                }
                return deletePosition;
            }
        }

        private void DeletePositionExecute()
        {
            try
            {
                if (Position != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to delete this position?" +
                        "", "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int positionId = Position.PositionID;

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            positionService.DeletePosition(positionId);
                            PositionList = positionService.GetPositions().ToList();

                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeletePositionExecute()
        {
            if (Position == null)
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
