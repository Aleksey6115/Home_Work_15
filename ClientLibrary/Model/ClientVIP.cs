using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLibrary.Model
{
    /// <summary>
    /// Клиент - VIP клиент
    /// </summary>
    public class ClientVIP : ClientAbstract
    {
        public override string Status => "VIP Клиент";
        public override decimal DepositRate => 0.2M;

        public ClientVIP(string fname, string lname, int cliAge, decimal acc = 0, decimal deposAcc = 0) :
               base(fname, lname, cliAge, acc, deposAcc)
        { }

        public ClientVIP() : base() { }
    }
}
