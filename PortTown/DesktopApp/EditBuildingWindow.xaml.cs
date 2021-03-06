﻿using System;
using System.Collections.Generic;
using System.Data;
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
using DesktopApp.Models;
namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditBuildingWindow : Window
    {
        public EditBuildingWindow()
        {
            InitializeComponent();
        }

        private void EditBuildingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var selectedRow = (Application.Current.MainWindow as AdminWindow).GetSelectedRows((Application.Current.MainWindow as AdminWindow).ItemTable)[0].Row;

            textbox1.Text = selectedRow[1].ToString();
            textbox2.Text = selectedRow[2].ToString();
            textbox3.Text = selectedRow[3].ToString();
        }

        private void EditItemClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as AdminWindow).EditAddable(new Item());
            this.Hide();
        }
    }
}
