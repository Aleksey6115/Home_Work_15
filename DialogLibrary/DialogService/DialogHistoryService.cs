using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using HistoryLibrary;
using DialogLibrary.DialogWindows;

namespace DialogLibrary.DialogService
{
    /// <summary>
    /// Сервис окна - история
    /// </summary>
    public class DialogHistoryService
    {
        /// <summary>
        /// Выбранная запись
        /// </summary>
        public History SelectedLog { get; set; }

        /// <summary>
        /// Открыть окно с историей
        /// </summary>
        /// <param name="historyList">Лист истории который нужно отобразить</param>
        /// <returns></returns>
        public bool OpenHistoryWindow(ObservableCollection<History> historyList)
        {
            HistoryWindow HW = new HistoryWindow();
            HW.historyList.Items.Clear();
            HW.historyList.ItemsSource = historyList;

            if (HW.ShowDialog() == true) return true;
            return false;
        }
    }
}
