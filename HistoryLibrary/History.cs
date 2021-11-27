using System;
using ClientLibrary.Model;
using UsersLibrary;

namespace HistoryLibrary
{
    /// <summary>
    /// Класс описывает операции совершённые со счетами
    /// </summary>
    public class History
    {
        #region Свойства
        /// <summary>
        /// Дата выполнения операции
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Кем было выполненно действие
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public decimal TransactionAmount { get; set; }

        /// <summary>
        /// Произведённая операция
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// Имя клиента со счётом которого было выполненно действие
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Статус клиента над счётом которого было выполненно действие
        /// </summary>
        public string ClientStatus { get; set; }
        #endregion

        /// <summary>
        /// Конструтор класса History
        /// </summary>
        /// <param name="user">текущий пользователь</param>
        /// <param name="oper">описание операции</param>
        /// <param name="sum">сумма операции</param>
        /// <param name="client">клиент над счётом которого выполняется действие</param>
        public History(IUsers user, string oper, decimal sum, ClientAbstract client)
        {
            Date = DateTime.Now.ToString();
            Author = user.Name;
            TransactionAmount = sum;
            Operation = oper;
            ClientName = $"{client.FirstName} {client.LastName}";
            ClientStatus = client.Status;
        }

        /// <summary>
        /// Указать явно пустой конструктор
        /// </summary>
        public History() { }
    }
}
