using System;
using System.Collections.Generic;
using System.Text;

namespace BankArchitecture.Routes
{
    public interface IRouter
    {
        void Route(int[] state);
    }
}
