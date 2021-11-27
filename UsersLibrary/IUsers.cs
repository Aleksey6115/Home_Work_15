using System;
using System.Collections.Generic;
using System.Text;

namespace UsersLibrary
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public interface IUsers
    {
        /// <summary>
        /// Название должности
        /// </summary>
        public string Name { get; }
    }
}
