using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using UsersLibrary;
using DialogLibrary.DialogWindows;

namespace DialogLibrary
{
    /// <summary>
    /// Сервис окна - Авторизация
    /// </summary>
    public class DialogAuthorizationService
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        private ObservableCollection<IUsers> users = new ObservableCollection<IUsers>
        {
            new Maneger(),
            new Consultant()
        };

        /// <summary>
        /// Выбранный пользователь
        /// </summary>
        public IUsers SelectedUser { get; set; }

        /// <summary>
        /// Открыть окно авторизации
        /// </summary>
        /// <returns></returns>
        public bool OpenAuthorizationDialog()
        {
            DialogAuthorizationWindow AW = new DialogAuthorizationWindow();
            AW.comboUsers.Items.Clear();
            AW.comboUsers.ItemsSource = users;

            if (AW.ShowDialog() == true)
            {
                SelectedUser = AW.comboUsers.SelectedItem as IUsers;
                return true;
            }

            return false;
        }
    }
}
