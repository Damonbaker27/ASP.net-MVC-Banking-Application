using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankOfBIT_DB.Data
{
    public class BankOfBIT_DBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BankOfBIT_DBContext() : base("name=BankOfBIT_DBContext")
        {
        }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.AccountState> AccountStates { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.BankAccount> BankAccounts { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.BronzeState> BronzeStates { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.SilverState> SilverStates { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.GoldState> GoldStates { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.PlatinumState> PlatinumStates { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.SavingsAccount> SavingsAccounts { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.MortgageAccount> MortgageAccounts { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.InvestmentAccount> InvestmentAccounts { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.ChequingAccount> ChequingAccounts { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.NextUniqueNumber> NextUniqueNumbers { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.NextSavingsAccount> NextSavingsAccounts { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.NextMortgageAccount> NextMortgageAccounts { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.NextInvestmentAccount> NextInvestmentAccounts { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.NextChequingAccount> NextChequingAccounts { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.NextTransaction> NextTransactions { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.NextClient> NextClients { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.Payee> Payees { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.Institution> Institutions { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.TransactionType> TransactionTypes { get; set; }

        public System.Data.Entity.DbSet<BankOfBIT_DB.Models.Transaction> Transactions { get; set; }
    }
}
