using System;
using System.Collections.Generic;
using System.Text;
using ClientLibrary.Model;
using UsersLibrary;

namespace ClientLibrary.Service
{
    /// <summary>
    /// Работа с депозитом клиента
    /// </summary>
    public class DepositService
    {
        /// <summary>
        /// События для изменения истории с депозитом клиента
        /// </summary>
        public event Action<IUsers, string, decimal, ClientAbstract> DepositAddHistory;

        #region Методы
        /// <summary>
        /// Открыть депозитный счёт
        /// </summary>
        /// <param name="client">Клиент у которого открывается счёт</param>
        /// <returns></returns>
        public ClientAbstract OpenDepositAccount(ClientAbstract client, IUsers user)
        {
            if (client.DepositAccount > 0) throw new ClientException("Депозитный счёт уже открыт!"); // Генерируем исключение

            client.DepositAccount = 1;
            client.AccumulatedDeposit = client.DepositAccount * client.DepositRate;
            DepositAddHistory?.Invoke(user, "Депозит открыт", 1, client);
            return client;
        }

        /// <summary>
        /// Закрыть депозитный счёт клиента
        /// </summary>
        /// <param name="client">клиент у которого закрывается депозитный счёт</param>
        /// <returns></returns>
        public ClientAbstract CloseDepositAccount(ClientAbstract client, IUsers user)
        {
            if (client.DepositAccount == 0) throw new ClientException("Депозитный счёт не открыт!"); // Генерируем исключение

            client.DepositAccount = 0;
            client.AccumulatedDeposit = 0;
            DepositAddHistory?.Invoke(user, "Депозит закрыт", 0, client);
            return client;
        }

        /// <summary>
        /// Пополнить депозитный счёт клиента
        /// </summary>
        /// <param name="client">Клиент у которого пополняется счёт</param>
        /// <param name="sum">Сумма пополнения счёта</param>
        /// <returns></returns>
        public ClientAbstract ReplenishDeposit(ClientAbstract client, decimal sum, IUsers user)
        {
            client.DepositAccount += sum;
            client.AccumulatedDeposit = client.DepositAccount * client.DepositRate;
            DepositAddHistory?.Invoke(user, "Депозит пополнен", sum, client);
            return client;
        }

        /// <summary>
        /// Перевести средства с депозита на счёт
        /// </summary>
        /// <param name="client">Клиент с чьим счётом выполняется операция</param>
        /// <param name="sum">Сумма перевода</param>
        /// <returns></returns>
        public ClientAbstract TransferToAccount(ClientAbstract client, decimal sum, IUsers user)
        {
            client.DepositAccount -= sum;
            client.AccumulatedDeposit = client.DepositAccount * client.DepositRate;
            client.Account += sum;
            DepositAddHistory?.Invoke(user, "Перевод: Депозит -> счёт", sum, client);
            return client;
        }
        #endregion
    }
}
