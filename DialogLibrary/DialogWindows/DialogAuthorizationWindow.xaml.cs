﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DialogLibrary.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для DialogAuthorizationWindow.xaml
    /// </summary>
    public partial class DialogAuthorizationWindow : Window
    {
        public DialogAuthorizationWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
