using System;

namespace UsersLibrary
{
    /// <summary>
    /// Пользователь консультант
    /// </summary>
    public class Consultant : IUsers
    {
        public string Name => "Консультант";

        public override string ToString()
        {
            return Name;
        }
    }
}
