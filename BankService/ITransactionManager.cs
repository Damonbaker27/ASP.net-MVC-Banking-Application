using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BankService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransactionManager" in both code and config file together.
    [ServiceContract]
    public interface ITransactionManager
    {

        /// <summary>
        /// adds the amount and notes argument to the bank account balance and creates transaction.
        /// </summary>
        /// <param name="accountId">represents the bank account</param>
        /// <param name="amount"> represents the deposit amount</param>
        /// <param name="notes">Represents the transaction Notes</param>
        /// <returns>New account balance</returns>
        [OperationContract]
        double? Deposit(int accountId, double amount, string notes);

        /// <summary>
        /// subtracts the amount specified in the amount argument and creates a transaction with notes. 
        /// </summary>
        /// <param name="accountId">represents the bank account</param>
        /// <param name="amount">represents the withdraw amount</param>
        /// <param name="notes">Represents the transaction Notes</param>
        /// <returns>New account balance</returns>
        [OperationContract]
        double? Withdrawal(int accountId, double amount,string notes);

        /// <summary>
        /// subtracts the amount argument from the bank account balance specified.
        /// </summary>
        /// <param name="accountId">represents the bank account</param>
        /// <param name="amount">represents the withdraw amount</param>
        /// <param name="notes">Represents the transaction Notes</param>
        /// <returns>New account balance</returns>
        [OperationContract]
        double? BillPayment(int accountId, double amount, string notes);

        /// <summary>
        /// Transfers the value of the amount argument from one bank account to the other. 
        /// Also creates 2 new transactions.
        /// </summary>
        /// <param name="fromAccountId">Represents the sending account </param>
        /// <param name="toAccountId">Represents the recieving account</param>
        /// <param name="amount">Represents the amount transfered</param>
        /// <param name="notes">Represents the transaction notes</param>
        /// <returns>New account balance</returns>
        [OperationContract]
        double? Transfer(int fromAccountId, int toAccountId, double amount, string notes);

        /// <summary>
        /// Calls the RateAdjustment method and and updates the balance of the bank account.
        /// Also creates a new Transaction.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="notes"></param>
        /// <returns> first account balance</returns>
        [OperationContract]
        double? CalculateInterest(int accountId, string notes);





    }
}
