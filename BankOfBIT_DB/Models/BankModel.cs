using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility;
using BankOfBIT_DB.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace BankOfBIT_DB.Models
{
    /// <summary>
    /// Bank Account model. Represents the Bank account table in the database.
    /// </summary>
    public abstract class BankAccount
    {
        private BankOfBIT_DBContext db = new BankOfBIT_DBContext();


        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BankAccountId { get; set; }


        [ForeignKey("Client")]
        public int ClientId { get; set; }


        [ForeignKey("AccountState")]
        public int AccountStateId { get; set; }

      
        [Display(Name = "Account\nNumber")]
        public long AccountNumber { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public double Balance { get; set; }

        [Required]
        [Display(Name = "Date\nCreated")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        public string Notes { get; set; }

        [Display(Name ="Account\nState")]
        public string Description
        {
            get
            {            
                return BusinessRules.TrimAccount(this.GetType().Name);             
            }
        }
        
        /// <summary>
        /// Checks to see if the state of the account needs to be changed.
        /// </summary>
        public void ChangeState()
        {
            
            AccountState account = db.AccountStates.Find(this.AccountStateId);

            int previousAccountStateId = 0;

            do
            {
                account.StateChangeCheck(this);
                previousAccountStateId = account.AccountStateId;
                account = db.AccountStates.Find(this.AccountStateId);
            }
            while(account.AccountStateId != previousAccountStateId);
           
        }


        public virtual void SetNextAccountNumber()
        {

        }

        //Navigation properties

        public virtual Client Client { get; set; }

        public virtual AccountState AccountState { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }


    }

    /// <summary>
    /// Saving account model. Represents the savings account table in the database.
    /// </summary>
    public class SavingsAccount :BankAccount
    {
        [Required]
        [Display(Name ="Savings Service\nCharge")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public double SavingsServiceCharges { get; set; }

        public override void SetNextAccountNumber()
        {
            this.AccountNumber = (long)StoredProcedure.NextNumber("NextSavingsAccount");
        }
    }

    /// <summary>
    /// Mortage account model. Represents the Mortage account table in the database.
    /// </summary>
    public class MortgageAccount : BankAccount
    {
        [Required]
        [Display(Name ="Mortgage\nRate")]
        [DisplayFormat(DataFormatString = "{0:p}", ApplyFormatInEditMode = true)]
        public double MortageRate { get; set; }
        
        [Required]
        public int Amortization { get; set; }

        public override void SetNextAccountNumber()
        {
            this.AccountNumber = (long)StoredProcedure.NextNumber("NextMortgageAccount");
        }
    }

    /// <summary>
    /// Investment account model. Represents the Investment account table in the database.
    /// </summary>
    public class InvestmentAccount : BankAccount
    {
        [Required]
        [Display(Name = "Interest\nRate")]
        [DisplayFormat(DataFormatString = "{0:p}")]
        public double InterestRate { get; set; }

        public override void SetNextAccountNumber()
        {
            this.AccountNumber = (long)StoredProcedure.NextNumber("NextInvestmentAccount");
        }
    }

    /// <summary>
    /// Chequing account model. Represents the Chequing account table in the database.
    /// </summary>
    public class ChequingAccount : BankAccount
    {
        [Required]
        [Display(Name ="Chequing Service\nCharges")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public double ChequingServiceCharges { get; set; }

        public override void SetNextAccountNumber()
        {
            this.AccountNumber = (long)StoredProcedure.NextNumber("NextChequingAccount");
        }
    }

    /// <summary>
    /// Account state model. Represents the account state table in the database.
    /// </summary>
    public abstract class AccountState
    {
        protected static BankOfBIT_DBContext db = new BankOfBIT_DBContext();

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccountStateId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        [Display(Name = "Lower\nLimit")]
        public double LowerLimit { get; set; }


        [Required]
        [DisplayFormat(DataFormatString ="{0:c}", ApplyFormatInEditMode = true)]
        [Display(Name = "Upper\nLimit")]
        public double UpperLimit { get; set; }


        [Required]
        [DisplayFormat(DataFormatString ="{0:c}", ApplyFormatInEditMode = true)]
        public double Rate { get; set; }


        [Display(Name ="Account\nState")]
        public string Description
        {
            get
            {
                //return this.GetType().Name;
                return Trim.GetName(this.GetType().Name);
            }
        }

        
        public virtual double RateAdjustment(BankAccount bankAccount)
        {
            return 0;
        }             

        public virtual void StateChangeCheck(BankAccount bankAccount)
        {
            
        }


        //Navigation properties
        public virtual ICollection<BankAccount> BankAccount { get; set; }


    }

    /// <summary>
    /// Bronze state model. Represents the bronze state table in the database.
    /// </summary>
    public class BronzeState : AccountState
    {
        private static BronzeState bronzeState;

        private BronzeState()
        {
            this.LowerLimit = 0;
            this.UpperLimit = 5000;
            this.Rate = 0.0100;
        }

        /// <summary>
        /// Creates and or returns the Bronze state from the database.
        /// </summary>
        /// <returns></returns>
        public static BronzeState GetInstance()
        {
            if(bronzeState == null)
            {
                bronzeState = db.BronzeStates.SingleOrDefault();

                if(bronzeState == null)
                {
                    bronzeState=new BronzeState();
                    db.BronzeStates.Add(bronzeState);
                    db.SaveChanges();
                }
            }

            return bronzeState;
        }

        /// <summary>
        /// Checks the account balance and returns an different rate accordingly.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            if(bankAccount.Balance < 0)
            {
                return 0.055;
            }

            return Rate;
        }

        /// <summary>
        /// Check the balance of bankaccount and determines if it moves to the Silver State.
        /// </summary>
        /// <param name="bankAccount"></param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            if(bankAccount.Balance > this.UpperLimit)
            {              
                bankAccount.AccountStateId = SilverState.GetInstance().AccountStateId;
                
            }
           
            db.SaveChanges();
        }

    }

    /// <summary>
    /// Silverstate model. Represents the Silverstate table in the database.
    /// </summary>
    public class SilverState : AccountState
    {
       private static SilverState silverState;

        private SilverState()
        {
            this.LowerLimit = 5000;
            this.UpperLimit = 10000;
            this.Rate = 0.0125;
        }

        /// <summary>
        /// Creates and or returns the SilverState from the database.
        /// </summary>
        /// <returns></returns>
        public static SilverState GetInstance()
        {
            if(silverState == null)
            {
                silverState = db.SilverStates.SingleOrDefault();

                if(silverState == null)
                {
                    silverState = new SilverState();
                    db.SilverStates.Add(silverState);
                    db.SaveChanges();
                }
            }
            
            return silverState;
        }

        /// <summary>
        /// Returns the default rate for SilverState.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            return this.Rate;
        }

        /// <summary>
        /// Checks the bankAccount balance to determine if the state will move to Gold or Bronze.
        /// </summary>
        /// <param name="bankAccount"></param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
           if(bankAccount.Balance < this.LowerLimit)
           {
               bankAccount.AccountStateId = BronzeState.GetInstance().AccountStateId;
           }

           if(bankAccount.Balance > this.UpperLimit)
           {
               bankAccount.AccountStateId = GoldState.GetInstance().AccountStateId;
           }

            db.SaveChanges();

        }

    }

    /// <summary>
    /// Goldstate model. Represents the Goldstate table in the database.
    /// </summary>
    public class GoldState : AccountState
    {
       private static GoldState goldState;

        /// <summary>
        /// Private constructor for GoldState.
        /// </summary>
        private GoldState()
        {
            this.LowerLimit = 10000;
            this.UpperLimit = 20000;
            this.Rate = 0.0200;
        }


        /// <summary>
        /// Creates and or returns the GoldState from the database.
        /// </summary>
        /// <returns></returns>
        public static GoldState GetInstance()
        {
            if(goldState == null)
            {
                goldState = db.GoldStates.SingleOrDefault();

                if(goldState == null)
                {
                    goldState = new GoldState();
                    db.GoldStates.Add(goldState);
                    db.SaveChanges();
                }
            }

            return goldState;
        }

        /// <summary>
        /// Adjust the interest rate depending on account age.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {

            int creationDate = 10;
            
            if(creationDate < 10)
            {
                return Rate + 0.10;
            }

            return Rate;

        }

        /// <summary>
        /// Check the balance of account to detemine if it need to change to next or previous state.
        /// </summary>
        /// <param name="bankAccount"></param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            if (bankAccount.Balance < this.LowerLimit)
            {
                bankAccount.AccountStateId = SilverState.GetInstance().AccountStateId;
            }

            if (bankAccount.Balance > this.UpperLimit)
            {
                bankAccount.AccountStateId = PlatinumState.GetInstance().AccountStateId;
            }

            db.SaveChanges();
        }


    }

    /// <summary>
    /// Platinum state model. Represents the Platinum state table in the database.
    /// </summary>
    public class PlatinumState : AccountState
    {
        private static PlatinumState platinumState;

        private PlatinumState()
        {
            this.LowerLimit = 20000;
            this.UpperLimit = 0;
            this.Rate = 0.0250;
        }

        /// <summary>
        /// Creates and or returns the PlatinumState from the database.
        /// </summary>
        /// <returns></returns>
        public static PlatinumState GetInstance()
        {
            if (platinumState == null)
            {
                platinumState = db.PlatinumStates.SingleOrDefault();

                if(platinumState == null)
                {
                    platinumState = new PlatinumState();
                    db.PlatinumStates.Add(platinumState);
                    db.SaveChanges(); 
                }

            }
            return platinumState;
        }

        /// <summary>
        /// Adjusts the interest rate depending on account age and balance.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            TimeSpan timeSpan = DateTime.Today - bankAccount.DateCreated;
            double accountAge = timeSpan.TotalDays / 365.25;

            double newRate = Rate;
  
            if(accountAge >= 10)
            {
                //Add 1% as the account is older than 10 years.
                newRate += 0.01;              


            }
            if (LowerLimit * 2 < bankAccount.Balance)
            {
                //If the account is older than 10 and twice the lower limit.
                newRate += 0.005;
            }


            return newRate;           

        }

        /// <summary>
        /// Checks the bankAccount balance to determine if the state will move to gold state or stay Platinum. 
        /// </summary>
        /// <param name="bankAccount"></param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            if (bankAccount.Balance < this.LowerLimit)
            {
                bankAccount.AccountStateId = GoldState.GetInstance().AccountStateId;
            }

            db.SaveChanges();

        }

    }

    /// <summary>
    /// Client model. Represents the Client table in the database.
    /// </summary>
    public class Client
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

     
        [Display(Name ="Client\nNumber")]
        public long ClientNumber { get; set; }


        [Required]
        [StringLength(35)]
        [Display(Name ="First\nName")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(35)]
        [Display(Name ="Last\nName")]
        public string LastName { get; set; }


        [Required]
        [StringLength(35)]
        public string Address { get; set; }


        [Required]
        [StringLength(35)]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK|YT)")]
        public string Province { get; set; }


        [Display(Name = "Date\nCreated")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime DateCreated { get; set; }


        public string Notes { get; set; }

        [Display(Name ="Name")]
        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        [Display(Name ="Address")]
        public string FullAddress
        {
            get
            {
                return String.Format("{0} {1} {2}", Address, City, Province);
            }
        }

        /// <summary>
        /// calls the stored procedure and updates the Client number with the returned value.
        /// </summary>
        public void SetNextClientNumber()
        {
            this.ClientNumber = (long)StoredProcedure.NextNumber("NextClient");
        }

         
        //Navigation Properties

        public virtual ICollection<BankAccount> BankAccount { get; set; }

    }


    /// <summary>
    /// Represents the Payee table in the database.
    /// </summary>
    public class Payee
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PayeeId  { get; set; }

        [Display(Name = "Payee")]
        public string Description { get; set; }

    }

    /// <summary>
    /// Represents the Institution table in the database.
    /// </summary>
    public class Institution
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InstitutionId { get; set; }

        [Required]
        [Display(Name ="Number")]
        public int InstitutionNumber { get; set; }

        [Required]
        [Display(Name = "Institution")]
        public string Description { get; set; }


    }

    /// <summary>
    /// Represents the Transaction table in the database.
    /// </summary>
    public class Transaction
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [ForeignKey("BankAccount")]
        public int BankAccountId { get; set; }

        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }

        [Display(Name ="Number")]
        public long TransactionNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public double? Deposit { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public double? Withdrawal { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public string Notes { get; set; }

        /// <summary>
        /// calls the stored procedure and updates the Transaction number with the returned value.
        /// </summary>
        public void SetNextTransactionNumber()
        {
            this.TransactionNumber = (long)StoredProcedure.NextNumber("NextTransaction");
        }


        //navigational properties here.

        public virtual TransactionType TransactionType { get; set; }

        public virtual BankAccount BankAccount { get; set; }

    }


    /// <summary>
    /// represents the TransactionType table in the database.
    /// </summary>
    public class TransactionType
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionTypeId { get; set; }

        [Required]
        [Display(Name ="Type")]
        public string Description { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }


    }

    /// <summary>
    /// represents the next unique number in the database.
    /// </summary>
    public abstract class NextUniqueNumber
    {
        protected static BankOfBIT_DBContext db = new BankOfBIT_DBContext();

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextUniqueNumberId { get; set; }

        [Required]
        public long NextAvailableNumber { get; set; }

    }

    /// <summary>
    /// Represents the next savings account in the database.
    /// </summary>
    public class NextSavingsAccount : NextUniqueNumber
    {
        private static NextSavingsAccount nextSavingsAccount;

        private NextSavingsAccount()
        {
            this.NextAvailableNumber = 20000;
        }
        /// <summary>
        /// Create and or returns the next savings account from the database.
        /// </summary>
        /// <returns></returns>
        public static NextSavingsAccount GetInstance()
        {
            if(nextSavingsAccount == null)
            {
                nextSavingsAccount = db.NextSavingsAccounts.SingleOrDefault();

                if(nextSavingsAccount == null)
                {
                    nextSavingsAccount= new NextSavingsAccount();
                    db.NextSavingsAccounts.Add(nextSavingsAccount);
                    db.SaveChanges();
                }
            }                           
            return nextSavingsAccount;
        }
    }

    /// <summary>
    /// Represents the next mortagage account in the database.
    /// </summary>
    public class NextMortgageAccount : NextUniqueNumber
    {
        private static NextMortgageAccount nextMortgageAccount;

        private NextMortgageAccount()
        {
            this.NextAvailableNumber = 200000;
        }

        /// <summary>
        /// Creates or returns the next mortagage account from the database.
        /// </summary>
        /// <returns></returns>
        public static NextMortgageAccount GetInstance()
        {
            if(nextMortgageAccount == null)
            {
                nextMortgageAccount = db.NextMortgageAccounts.SingleOrDefault();

                if(nextMortgageAccount == null)
                {
                    nextMortgageAccount = new NextMortgageAccount();
                    db.NextMortgageAccounts.Add(nextMortgageAccount);
                    db.SaveChanges();
                }
            }
            return nextMortgageAccount;
        }
    }

    /// <summary>
    /// represents the nextInvestment account in the database.
    /// </summary>
    public class NextInvestmentAccount : NextUniqueNumber
    {
        private static NextInvestmentAccount nextInvestmentAccount;

        private NextInvestmentAccount()
        {
            this.NextAvailableNumber = 2000000;
        }

        /// <summary>
        /// Create and or returns the next investment account from the database.
        /// </summary>
        /// <returns></returns>
        public static NextInvestmentAccount GetInstance()
        {
            if (nextInvestmentAccount == null)
            {
                nextInvestmentAccount = db.NextInvestmentAccounts.SingleOrDefault();

                if (nextInvestmentAccount == null)
                {
                    nextInvestmentAccount = new NextInvestmentAccount();
                    db.NextInvestmentAccounts.Add(nextInvestmentAccount);
                    db.SaveChanges();
                }
            }

            return nextInvestmentAccount;
        }

    }

    /// <summary>
    /// Represents the next chequing account in the database.
    /// </summary>
    public class NextChequingAccount : NextUniqueNumber
    {
        private static NextChequingAccount nextChequingAccount;

        private NextChequingAccount()
        {
            this.NextAvailableNumber = 20000000;
        }

        /// <summary>
        /// Creates and or returns the next chequing account from the database.
        /// </summary>
        /// <returns></returns>
        public static NextChequingAccount GetInstance()
        {
            if(nextChequingAccount == null)
            {
                nextChequingAccount = db.NextChequingAccounts.SingleOrDefault();

                if(nextChequingAccount == null)
                {
                    nextChequingAccount = new NextChequingAccount();
                    db.NextChequingAccounts.Add(nextChequingAccount);
                    db.SaveChanges();
                }
            }
            
            return nextChequingAccount;
        }

    }

    /// <summary>
    /// Represents the Next Client table in the database
    /// </summary>
    public class NextClient : NextUniqueNumber
    {
        private static NextClient nextClient;

        private NextClient()
        {
            this.NextAvailableNumber = 20000000;
        }
        
        /// <summary>
        /// Creates and or returns the next client instance from the database.
        /// </summary>
        /// <returns></returns>
        public static NextClient GetInstance()
        {
            if(nextClient == null)
            {
                nextClient = db.NextClients.SingleOrDefault();

                if(nextClient == null)
                {
                    nextClient = new NextClient();
                    db.NextClients.Add(nextClient);
                    db.SaveChanges();
                }
            }

            return nextClient;
        }



    }

    /// <summary>
    /// represents the NextTranaction table in the database
    /// </summary>
    public class NextTransaction : NextUniqueNumber
    {
        private static NextTransaction nextTransaction;

        private NextTransaction()
        {
            this.NextAvailableNumber = 700;
        }

        /// <summary>
        /// creates and or returns the next transaction instance from the database.
        /// </summary>
        /// <returns></returns>
        public static NextTransaction GetInstance()
        {
            if (nextTransaction == null)
            {
                nextTransaction = db.NextTransactions.SingleOrDefault();

                if(nextTransaction == null)
                {
                    nextTransaction= new NextTransaction();
                    db.NextTransactions.Add(nextTransaction);
                    db.SaveChanges();
                }
            }
            
            return nextTransaction;
        }

    }


    /// <summary>
    /// Class representing the stored procedure on the database
    /// </summary>
    public static class StoredProcedure
    {
        /// <summary>
        /// Returns the next auto incrementing Id available in the database.
        /// </summary>
        /// <param name="discriminator"></param>
        /// <returns></returns>
        public static long? NextNumber(string discriminator)
        {
            try
            {
                //Create a new SQl connection to the database.
                SqlConnection connection = new SqlConnection("Data Source=localhost; " +
                                                "Initial Catalog=BankOfBIT_DBContext;Integrated Security=True");

                //Create a nullable long type variable for the return value.
                long? returnValue = 0;

                //Create and sql command with the command text and the connection that should used.
                SqlCommand storedProcedure = new SqlCommand("next_number", connection);

                //Set the command type of the SQL command to storedProcedure type from the enumeration. 
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Set the Parameters of the command to add and additional one with the name and value of the variable discriminator. 
                storedProcedure.Parameters.AddWithValue("@Discriminator", discriminator);

                //Create a new output parameter called @NewVal and setting the datatype to BigInt.
                SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };

                //Add the output parameter to the SQL command stored Procedure.
                storedProcedure.Parameters.Add(outputParameter);

                //Open the database connection.
                connection.Open();

                //Execute the query on the database.
                storedProcedure.ExecuteNonQuery();

                //close the connection to the database.
                connection.Close();

                //cast the output parameter to type long? and store it in the return value.
                returnValue = (long?)outputParameter.Value;

                //return the value.
                return returnValue;
            }
            catch
            {
                return null;
            }
            
            
       

        }
    }













}