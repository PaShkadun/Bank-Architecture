using System;
using System.Collections.Generic;
using System.Text;

namespace BankArchitecture.Providers
{
    public interface IBankProvider
    {
        public string InputBankName();

        public void ChooseAccountForDelete();

        public void ChooseAccountForTransfer();

        public void Actions();

        public void Start(int choose);
    }
}
