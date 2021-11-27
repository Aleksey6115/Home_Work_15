using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientLibrary.Model
{
    /// <summary>
    /// Класс описывает сущность Клиента
    /// </summary>
    public class ClientAbstract : INotifyPropertyChanged
    {
        #region Поля
        private string firstName; // Имя клиента
        private string lastName; // Фамилия клиента
        private int age; // Возраст клиента
        private decimal account; // Сумма на счёте клиента
        private decimal depositAccount; // Сумма на депозите
        private decimal accumulatedDeposit; // Накопленная сумма по депозиту
        #endregion

        #region Свойства

        /// <summary>
        /// Статус клиента
        /// </summary>
        public virtual string Status { get; }

        /// <summary>
        /// Процентная ставка по депозиту
        /// </summary>
        public virtual decimal DepositRate { get; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string FirstName
        {
            get => firstName;
            set => Set(ref firstName, value);
        }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName
        {
            get => lastName;
            set => Set(ref lastName, value);
        }

        /// <summary>
        /// Возраст клиента
        /// </summary>
        public int Age
        {
            get => age;
            set => Set(ref age, value);
        }

        /// <summary>
        /// Счёт клиента
        /// </summary>
        public decimal Account
        {
            get => account;
            set => Set(ref account, value);
        }

        /// <summary>
        /// Депозитный счёт клиента
        /// </summary>
        public decimal DepositAccount
        {
            get => depositAccount;
            set => Set(ref depositAccount, value);
        }

        /// <summary>
        /// Накопленная сумма по депозиту
        /// </summary>
        public decimal AccumulatedDeposit
        {
            get => accumulatedDeposit;
            set => Set(ref accumulatedDeposit, value);
        }
        #endregion

        /// <summary>
        /// Конструктор класса ClientAbstract
        /// </summary>
        /// <param name="fname">Имя клиента</param>
        /// <param name="lname">Фамилия клиента</param>
        /// <param name="cliAge">Возраст клиента</param>
        /// <param name="acc">Счёт клиента</param>
        /// <param name="deposAcc">Депозитный счёт клиента</param>
        public ClientAbstract(string fname, string lname, int cliAge, decimal acc = 0, decimal deposAcc = 0)
        {
            FirstName = fname;
            LastName = lname;
            Age = cliAge;
            Account = acc;
            DepositAccount = deposAcc;
        }

        /// <summary>
        /// Конструктор класса ClientAbstract без параметоров
        /// </summary>
        public ClientAbstract()
        {
            FirstName = "Имя";
            LastName = "Фамилия";
            Age = 18;
            DepositAccount = 0;
            Account = 0;
        }

        #region Реализация INPC
        public event PropertyChangedEventHandler PropertyChanged; // Оповещение внешних клиентов об изменениях

        /// <summary>
        /// Генерирование события PropertyChanged
        /// </summary>
        /// <param name="prop"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Проверка на то, что свойство действительно изменилось, 
        /// Если нет события PropertyChanged не генерируется
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string prop = "")
        {
            if (Equals(field, value)) return false; // Св-во не было измененно

            else // Св-во было измененно
            {
                field = value;
                OnPropertyChanged(prop);
                return true;
            }
        }
        #endregion

        /// <summary>
        /// Переопределние для отображения в ComboBox
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Status;
        }

        /// <summary>
        /// Сравнение объектов класса ClientAbstact
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            ClientAbstract client = obj as ClientAbstract;
            if (client == null) return false;

            else return (this.Status == client.Status && this.FirstName.ToLower() == client.FirstName.ToLower() &&
                this.LastName.ToLower() == client.LastName.ToLower() && this.Age == client.Age);
        }
    }
}
