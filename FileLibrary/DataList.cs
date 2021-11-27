using System;
using System.Collections.ObjectModel;
using ClientLibrary.Model;
using HistoryLibrary;

namespace FileLibrary
{
    /// <summary>
    /// Класс хранит в себе информацию о БД и истории
    /// </summary>
    public class DataList
    {
        /// <summary>
        /// Коллекция клиентов
        /// </summary>
        public ObservableCollection<ClientAbstract> clientsList;

        /// <summary>
        /// Коллекция операций произведенных со счетами клиентов
        /// </summary>
        public ObservableCollection<History> historyList;

        /// <summary>
        /// Конструктор класса DataList
        /// </summary>
        public DataList()
        {
            clientsList = new ObservableCollection<ClientAbstract>();
            historyList = new ObservableCollection<History>();
        }
    }
}
