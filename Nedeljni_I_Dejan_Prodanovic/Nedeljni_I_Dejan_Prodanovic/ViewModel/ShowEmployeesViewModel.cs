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
    class ShowEmployeesViewModel:ViewModelBase
    {
        ShowEmployees view;

        IManagerService managerService;
        IUserService userService;
        IEmployeeService employeeService;

        public ShowEmployeesViewModel(ShowEmployees employeeView,tblManager managerLogedIn)
        {
            view = employeeView;


            managerService = new ManagerService();
            userService = new UserService();
            employeeService = new EmployeeService();

            Manager = managerLogedIn;

            Employee = new vwEmployee2();
            EmployeeList = managerService.GetEmployeesOfManager(Manager.ManagerID);

            //EmployeeList = FilterEmployeesOfManager(EmployeeList);


        }

        private vwEmployee2 employee;
        public vwEmployee2 Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
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

        private List<vwEmployee2> employeeList;
        public List<vwEmployee2> EmployeeList
        {
            get
            {
                return employeeList;
            }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private ICommand deleteEmployee;
        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(),
                        param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }
        }

        private void DeleteEmployeeExecute()
        {
            try
            {
                if (Manager != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to delete this employee?" +
                        "", "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int empId = Employee.EmployeeID;

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            userService.DeleteUser((int)Employee.UserID);
                            employeeService.DeleteEmployee(Employee.EmployeeID);

                            EmployeeList = managerService.GetEmployeesOfManager(Manager.ManagerID);
                         
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteEmployeeExecute()
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
                ManagerMainView managerMainView = new ManagerMainView(Manager);
                managerMainView.Show();
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

        private List<vwEmployee2> FilterEmployeesOfManager(List<vwEmployee2>employees)
        {
            List<vwEmployee2> newList = new List<vwEmployee2>();
            foreach (var employee in employees)
            {
                if (employee.IsNewRequest!=null)
                {
                    if (!(bool)employee.IsNewRequest)
                    {
                        newList.Add(employee);
                    }
                }
               
            }
            return newList;
        }

    }
}
