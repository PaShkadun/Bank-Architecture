﻿using Microsoft.Extensions.DependencyInjection;
using BankArchitecture.Bll.Accounts.Implementations;
using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Bll.Bank.Implementations;
using BankArchitecture.Bll.Bank.interfaces;
using BankArchitecture.Bll.Cards.Implementations;
using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common.Models;

namespace BankArchitecture.Di
{
    public static class IoC
    {
        public static void BuildIoC(this IServiceCollection services)
        {
            services.AddSingleton<ICreditCardService, CreditCardService>();
            services.AddSingleton<IDebitCardService, DebitCardService>();
            services.AddSingleton<ICreditAccountService, CreditAccountService>();
            services.AddSingleton<IDebitAccountService, DebitAccountService>();
            services.AddSingleton<IBankService, BankService>();
        }
    }
}
