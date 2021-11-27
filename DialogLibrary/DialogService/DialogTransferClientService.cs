using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using ClientLibrary.Model;
using System.Linq;
using DialogLibrary.DialogWindows;

namespace DialogLibrary.DialogService
{
    /// <summary>
    /// Сервис окна - перевод другому клиенту
    /// </summary>
    public class DialogTransferClientService
    {
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public ClientAbstract clientTransfer { get; set; }

        /// <summary>
        /// Открытие окна для перевода другому клиенту
        /// Результат = выбор клиента которому нужно сделать перевод
        /// </summary>
        /// <param name="clients">Список клиентов</param>
        /// <param name="selectedClient">Выбранный клиент</param>
        /// <returns></returns>
        public bool OpenTransferClientDialog(ObservableCollection<ClientAbstract> clients, ClientAbstract selectedClient)
        {
            ObservableCollection<ClientAbstract> clientsList = new ObservableCollection<ClientAbstract>();

            // Выбор клиента у которых открыт счёт
            var selectClient = from s in clients
                               where s.Account > 0
                               select s;

            // Сформировать лист клиентов с открытыми счетами
            foreach (var client in selectClient)
            {
                if (client.Equals(selectedClient)) continue;
                clientsList.Add(client);
            }

            TransferClientWindow TCW = new TransferClientWindow(); // Открыть диалоговое окно
            TCW.clientListBox.Items.Clear();
            TCW.clientListBox.ItemsSource = clientsList;

            if (TCW.ShowDialog() == true)
            {
                clientTransfer = TCW.clientListBox.SelectedItem as ClientAbstract;
                return true;
            }
            return false;
        }
    }
}
