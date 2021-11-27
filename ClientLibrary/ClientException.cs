using System;

namespace ClientLibrary
{
    /// <summary>
    /// Исключение
    /// </summary>
    public class ClientException : Exception
    {
        /// <summary>
        /// Конструктору присвается значения свойства Message
        /// </summary>
        /// <param name="message"></param>
        public ClientException(string message) : base(message) { }
    }
}
