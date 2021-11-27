using System;
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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (comboStatusClient.SelectedItem != null && comboAgeClient.SelectedItem != null &&
                             txtNameClient.Text.Length > 0 && txtLastNameClient.Text.Length > 0)
                this.DialogResult = true;

            else MessageBox.Show("Не все поля были заполнены!!!");
        }
    }
}
