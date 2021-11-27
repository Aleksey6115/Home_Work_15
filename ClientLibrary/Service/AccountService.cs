using System;
using System.Collections.Generic;
using System.Text;
using ClientLibrary.Model;
using UsersLibrary;

namespace ClientLibrary.Service
{
    /// <summary>
    /// Работа со счётом клиента
    /// </summary>
    public class AccountService
    {
        /// <summary>
        /// События для добавления измений в счёте клиента
        /// </summary>
        public event Action<IUsers, string, decimal, ClientAbstract> AccountAddHistory;

        #region Методы
        /// <summary>
        /// Открыть счёт
        /// </summary>
        /// <param name="client">Клиент у которого открывается счёт</param>
        /// <returns></returns>
        public ClientAbstract OpenAccount(ClientAbstract client, IUsers user)
        {
            if (client.Account > 0) throw new ClientException("Счёт уже открыт"); // Генерируем исключение

            client.Account = 1;
            AccountAddHistory?.Invoke(user, "Счёт открыт", 1, client);
            return client;
        }

        /// <summary>
        /// Закрыть счёт
        /// </summary>
        /// <param name="client">Клиент у которого закрывается счёт</param>
        /// <returns></returns>
        public ClientAbstract CloseAccount(ClientAbstract client, IUsers user)
        {
            if (client.Account == 0) throw new ClientException("Счёт не открыт"); // Генерируем исключение

            client.Account = 0;
            AccountAddHistory?.Invoke(user, "Счёт закрыт", 0, client);
            return client;
        }

        /// <summary>
        /// Пополнение счёта клиента
        /// </summary>
        /// <param name="client">Клиент у которого попполняется счёт</param>
        /// <param name="sum">Сумма пополнения</param>
        /// <returns></returns>
        public ClientAbstract ReplenishAccount(ClientAbstract client, decimal sum, IUsers user)
        {
            client.Account += sum;
            AccountAddHistory?.Invoke(user, "Счёт пополнен", sum, client);
            return client;
        }

        /// <summary>
        /// Снять средства со счёта клиента
        /// </summary>
        /// <param name="client">Клиент у которого снимаются средства</param>
        /// <param name="sum">Сумма для снятия</param>
        /// <returns></returns>
        public ClientAbstract WithdrawAccount(ClientAbstract client, decimal sum, IUsers user)
        {
            client.Account -= sum;
            AccountAddHistory?.Invoke(user, "Снятие со счёта", sum, client);
            return client;
        }

        /// <summary>
        /// Перевести деньги со счёта на депозит
        /// </summary>
        /// <param name="client">Клиент со ссчетами которого выполняются операции</param>
        /// <param name="sum">Сумма перевода</param>
        /// <returns></returns>
        public ClientAbstract TransferToDeposit(ClientAbstract client, decimal sum, IUsers user)
        {
            client.Account -= sum;
            client.DepositAccount += sum;
            client.AccumulatedDeposit = client.DepositAccount * client.DepositRate;
            AccountAddHistory?.Invoke(user, "Перевод: Счёт -> Депозит", sum, client);
            return client;
        }
        #endregion
    }
}
