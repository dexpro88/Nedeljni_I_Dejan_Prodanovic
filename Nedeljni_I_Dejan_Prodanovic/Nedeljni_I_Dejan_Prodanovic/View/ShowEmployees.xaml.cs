﻿using Nedeljni_I_Dejan_Prodanovic.Model;
using Nedeljni_I_Dejan_Prodanovic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nedeljni_I_Dejan_Prodanovic.View
{
    /// <summary>
    /// Interaction logic for ShowEmployees.xaml
    /// </summary>
    public partial class ShowEmployees : Window
    {
        public ShowEmployees()
        {
            InitializeComponent();
        }
        public ShowEmployees(tblManager manager)
        {
            InitializeComponent();
            DataContext = new ShowEmployeesViewModel(this, manager);
        }
    }
}