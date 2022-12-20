using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BankOfBIT_DB.Data;
using BankOfBIT_DB.Models;
using Utility;

namespace BankService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TransactionManager" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TransactionManager.svc or TransactionManager.svc.cs at the Solution Explorer and start debugging.

    public class TransactionManager : ITransactionManager
    {

        private BankOfBIT_DBContext db = new BankOfBIT_DBContext();


        /// <summary>
        /// subtracts the amount argument from the bank account balance specified.
        /// </summary>
        /// <param name="accountId">represents the bank account</param>
        /// <param name="amount">represents the bill amount</param>
        /// <param name="notes">Represents the transaction Notes</param>
        /// <returns>new account balance</returns>
        public double? BillPayment(int accountId, double amount, string notes)
        {
            amount = Math.Abs(amount) * -1;
            
            try
            {
                double newBalance = (double)UpdateBalance(accountId, amount);

                CreateTransaction(accountId, amount, (int)TransactionTypeValues.BILL_PAYMENT, notes);

                return newBalance;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        /// <summary>
        /// Calls the RateAdjustment method and updates the balance of the bank account.
        /// Also creates a new Transaction.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="notes"></param>
        /// <returns>new account balance</returns>
        public double? CalculateInterest(int accountId, string notes)
        {

            BankAccount bankAccount = (from results in db.BankAccounts
                                     where results.BankAccountId == accountId
                                     select results).SingleOrDefault();

            //BankAccount bankAccount = db.BankAccounts.Where(x => x.BankAccountId == accountId).SingleOrDefault();

            double newRate = bankAccount.AccountState.RateAdjustment(bankAccount);

            double interest = (newRate * bankAccount.Balance * 1) / 12;


            try
            {
                double newBalance = (double)UpdateBalance(accountId, interest);

                CreateTransaction(accountId, interest, (int)TransactionTypeValues.INTEREST, notes);

                return newBalance;
            }
            catch (Exception)
            {

                return null;
            }

        }

        /// <summary>
        /// adds the amount and notes argument to the bank account balance and creates transaction.
        /// </summary>
        /// <param name="accountId">represents the bank account</param>
        /// <param name="amount"> represents the deposit amount</param>
        /// <param name="notes">Represents the transaction Notes</param>
        /// <returns>New account balance.</returns>
        public double? Deposit(int accountId, double amount, string notes)
        {
            try
            {
                double newBalance = (double)UpdateBalance(accountId, Math.Abs(amount));

                CreateTransaction(accountId, amount, (int)TransactionTypeValues.DEPOSIT, notes);

                return newBalance;
            }
            catch
            {
                return null;
            }              
        }

        /// <summary>
        /// Transfers the value of the amount argument from one bank account to the other. 
        /// Also creates 2 new transactions.
        /// </summary>
        /// <param name="fromAccountId">Represents the sending account </param>
        /// <param name="toAccountId">Represents the recieving account</param>
        /// <param name="amount">Represents the amount transfered</param>
        /// <param name="notes">Represents the transaction notes</param>
        /// <returns>new account balance</returns>
        public double? Transfer(int fromAccountId, int toAccountId, double amount, string notes)
        {       
            try
            {
                //update the balance of the sending account 
                double newBalance = (double)UpdateBalance(fromAccountId, Math.Abs(amount) * -1);
                CreateTransaction(fromAccountId, Math.Abs(amount) * -1, (int)TransactionTypeValues.TRANSFER, notes);

                //update the balance of the recieving account.
                UpdateBalance(toAccountId, Math.Abs(amount));
                CreateTransaction(toAccountId, Math.Abs(amount), (int)TransactionTypeValues.TRANSFER_RECIPIENT, notes);

                return newBalance;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// subtracts the amount specified in the amount argument and creates a transaction with notes. 
        /// </summary>
        /// <param name="accountId">represents the bank account</param>
        /// <param name="amount">represents the withdraw amount</param>
        /// <param name="notes">Represents the transaction Notes</param>
        /// <returns>new account balance</returns>
        public double? Withdrawal(int accountId, double amount, string notes)
        {
            amount = Math.Abs(amount) * -1;

            try
            {
                double newBalance = (double)UpdateBalance(accountId, amount);

                CreateTransaction(accountId, amount, (int)TransactionTypeValues.WITHDRAWAL, notes);

                return newBalance;
            }
            catch (Exception)
            {

                return null;
            }          
        }

        /// <summary>
        /// Updates the balance of the bank account with amount argument value.
        /// </summary>
        /// <param name="accountId">Represents the bank account</param>
        /// <param name="amount">Represents the amount</param>
        /// <returns>updated account balance</returns>
        private double? UpdateBalance(int accountId, double amount)
        {
            BankAccount account = (from results in db.BankAccounts
                                   where results.BankAccountId == accountId
                                   select results).SingleOrDefault();

            //BankAccount account = db.BankAccounts.Where(x => x.BankAccountId == accountId).SingleOrDefault();

            account.Balance += amount;
        
            try
            {
               db.SaveChanges();
            }
            catch (Exception)
            {

                return null;
            }   
            
            return account.Balance;
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="accountId">Represents the bank account</param>
        /// <param name="amount">Represents the amount</param>
        /// <param name="transactionTypeId">Represents the transaction type</param>
        /// <param name="notes">Represents the transaction notes</param>
        private void CreateTransaction(int accountId,double amount, int transactionTypeId, string notes)
        {
            Transaction newTransaction = new Transaction();

            
            if(amount < 0)
            {
                newTransaction.Deposit = null;
                newTransaction.Withdrawal = Math.Abs(amount);
            }
            else
            {
                newTransaction.Deposit = amount;
                newTransaction.Withdrawal = null;
            }
            
            newTransaction.DateCreated = DateTime.Now;
            
            newTransaction.BankAccountId = accountId;

            newTransaction.TransactionTypeId = transactionTypeId;

            newTransaction.Notes = notes;

            newTransaction.SetNextTransactionNumber();

            db.Transactions.Add(newTransaction);
            db.SaveChanges();


        }


    }
}
