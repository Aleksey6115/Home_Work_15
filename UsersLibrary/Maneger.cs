using System;
using System.Collections.Generic;
using System.Text;

namespace UsersLibrary
{
    /// <summary>
    /// Пользователь - Менеджер
    /// </summary>
    public class Maneger : IUsers
    {
        public string Name => "Менеджер";

        public override string ToString()
        {
            return Name;
        }
    }
}
