using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLibrary.Model
{
    /// <summary>
    /// Клиент - Физическое лицо
    /// </summary>
    public class ClientIndividual : ClientAbstract
    {
        public override string Status => "Физическое лицо";
        public override decimal DepositRate => 0.1M;

        public ClientIndividual(string fname, string lname, int cliAge, decimal acc = 0, decimal deposAcc = 0) :
               base(fname, lname, cliAge, acc, deposAcc)
        { }

        public ClientIndividual() : base() { }
    }
}
