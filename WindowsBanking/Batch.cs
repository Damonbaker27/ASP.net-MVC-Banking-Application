using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using BankOfBIT_DB.Data;
using BankOfBIT_DB.Models;
using Utility;

namespace WindowsBanking
{
    public class Batch
    {
        /// <summary>
        /// The name of the xml input file.
        /// </summary>
        private String inputFileName;

        /// <summary>
        /// The name of the log file.
        /// </summary>
        private String logFileName;

        /// <summary>
        /// The data to be written to the log file.
        /// </summary>
        private String logData;

        private BankOfBIT_DBContext db = new BankOfBIT_DBContext();

        DateTime date;
        XAttribute institution;
        int headerCheckSum;


        /// <summary>
        /// writes the failed transactions to the log file.
        /// </summary>
        /// <param name="beforeQuery"></param>
        /// <param name="afterQuery"></param>
        /// <param name="message"></param>
        private void ProcessErrors(IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, String message)
        {        
            IEnumerable<XElement> errors = beforeQuery.Except(afterQuery);

            foreach (var record in errors)
            {
                logData += "---------------ERROR---------------\n";
                logData += $"File: {inputFileName}\n";
                logData += $"Institution: {record.Element("institution")}\n";
                logData += $"Account Number: {record.Element("account_no")}\n";
                logData += $"Transaction Type: {record.Element("type")}\n";
                logData += $"Amount: {record.Element("amount")}\n";
                logData += $"Notes: {record.Element("notes")}\n";
                logData += $"Nodes: {record.Nodes().Count()}\n";
                logData += $"{message}.\n";
            }

        }

        /// <summary>
        /// validated the attributes in the xml header.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ProcessHeader()
        {
            XDocument document = XDocument.Load(inputFileName);
            XElement root = document.Element("account_update");

            //retrive date and institution attributes.
            date = (DateTime)root.Attribute("date");
            institution = root.Attribute("institution");

            //retrive the check sum values
            headerCheckSum = (int)root.Attribute("checksum");
            IEnumerable<XElement> transactions = root.Descendants("transaction");
            IEnumerable<XElement> checkSumValues = transactions.Elements("account_no");

            int checksum = 0;

            var institutions = db.Institutions.Select(x => x.InstitutionNumber);

            if (!institutions.Contains(int.Parse(institution.Value)))
            {
                throw new Exception($"ERROR: Institution: {institution} in file {inputFileName} does not exist");
            }

            //validate the number of attributes
            if (root.Attributes().Count() != 3)
            {
                throw new Exception($"ERROR: Incorrect number of root Attributes for file {inputFileName}.");
            }

            //validate the date.
            if (!date.Equals(DateTime.Today))
            {
                throw new Exception($"ERROR: Incorrect date for file {inputFileName}.");
            }

            //calculate the checksum
            int test = checkSumValues.Count();
            foreach (var sum in checkSumValues)
            {
                checksum += Convert.ToInt32(sum.Value);
            }

            //validate the header checksum matches the calculated checksum value.
            if (headerCheckSum != checksum)
            {
                throw new Exception($"ERROR: Incorrect checksum value, header value: {headerCheckSum} did not equal calculated: {checksum}.");
            }
        }

        /// <summary>
        /// validates the body of the xml document.
        /// </summary>
        private void ProcessDetails()
        {
            XDocument document = XDocument.Load(inputFileName);
     
            //retrive the transactions elements
            IEnumerable<XElement> transactions = document.Descendants("transaction");      

            //retrive all the transactions that have 5 children
            IEnumerable<XElement> childElements = transactions.Where(x => x.Elements().Nodes().Count() == 5);
            ProcessErrors(transactions, childElements,"transaction does not have 5 nodes");
        
            //retrive all transactions where institution matches header
            IEnumerable<XElement> institutions = childElements.Where(x => x.Element("institution").Value.Equals(institution.Value));
            ProcessErrors(childElements, institutions, " transaction institution value does not match header");
         
            //check if the type and amount child is numeric
            IEnumerable<XElement> amountTypes = institutions.Where(x => Numeric.IsNumeric(x.Element("type").Value, 
                System.Globalization.NumberStyles.Integer) 
            && Numeric.IsNumeric(x.Element("amount").Value, System.Globalization.NumberStyles.Float));
            
            ProcessErrors(institutions, amountTypes, "type or amount must be numbers.");

            //select type 2 and 6
            IEnumerable<XElement> amounts = amountTypes.Where(x => x.Element("type").Value.Equals("2") || x.Element("type").Value.Equals("6"));
            ProcessErrors(amountTypes, amounts, "type must be 2 or 6");

            //selects all transaction where type is 2 AND amount greater than 0 and type is 6 and amount 0.
            IEnumerable<XElement> typetwoAmountNotZero = amounts.Where(x => x.Element("type").Value.Equals("2")
            && double.Parse(x.Element("amount").Value) > 0 || x.Element("type").Value.Equals("6") && double.Parse(x.Element("amount").Value).Equals(0));         
            
            ProcessErrors(amounts, typetwoAmountNotZero, "Amount must be positive when type is 2 and amount must be 0 when type is 6");

            //selects all the bank account numbers
            IEnumerable<long> accountNumbers = db.BankAccounts.Select(x => x.AccountNumber).ToList();

            //selects all transaction where account number is in accountnumber query results
            IEnumerable<XElement> accounts = typetwoAmountNotZero.Where(x => accountNumbers.Contains(long.Parse(x.Element("account_no").Value)));
            
            ProcessTransactions(accounts);
        }

        /// <summary>
        /// Processes the transactions by calling the transaction manager service.
        /// </summary>
        /// <param name="transactionRecords"></param>
        private void ProcessTransactions(IEnumerable<XElement> transactionRecords)
        {
            TransactionManagerService.TransactionManagerClient transactionManagerClient = new TransactionManagerService.TransactionManagerClient();

            foreach (var transaction in transactionRecords)
            {                
                int transactionType = int.Parse(transaction.Element("type").Value);
                int accountNumber = int.Parse(transaction.Element("account_no").Value);
                int accountId = db.BankAccounts.Where(x => x.AccountNumber == accountNumber).Select(x => x.BankAccountId).SingleOrDefault();

                double amount = double.Parse(transaction.Element("amount").Value);
                string notes = transaction.Element("notes").Value;
                
                if (transactionType == (int)TransactionTypeValues.WITHDRAWAL)
                {
                    try
                    {
                        transactionManagerClient.Withdrawal(accountId, amount, notes);
                        logData += $"Transaction completed successfully: Withdrawal - {amount} applied to account {accountNumber}.\n";
                    }
                    catch (Exception)
                    {
                        logData += "Transaction completed unsucessfully.\n";
                    }                  
                }

                if (transactionType == (int)TransactionTypeValues.INTEREST)
                {
                    try
                    {
                        transactionManagerClient.CalculateInterest(accountId, notes);

                        logData += $"Transaction completed sucessfully: Interest - *** applied to account {accountNumber}.\n";
                    }
                    catch (Exception)
                    {
                        logData += "Transaction completed unsucessfully.\n";
                    }

                }
            }                              
        }

        /// <summary>
        /// writes the log data to the log file.
        /// </summary>
        /// <returns></returns>
        public String WriteLogData()
        {
            //to be modified

            StreamWriter log = new StreamWriter(logFileName);
          
            log.Write(this.logData);
            log.Close();

            string logData = this.logData;
            this.logData = "";
            this.logFileName = "";


            return logData;
        }

        /// <summary>
        /// created and Checks if the input file already exists. creates the log file name.
        /// </summary>
        /// <param name="institution"></param>
        /// <param name="key"></param>
        public void ProcessTransmission(String institution, String key)
        {
            DateTime date = DateTime.Now;
            string day = date.DayOfYear.ToString();
            
            inputFileName = $"{date.ToString("yyyy")}-{day}-{institution}.xml";

            logFileName = $"LOG {date.ToString("yyyy")}-{day}-{institution}.txt";

            try
            {
                if (File.Exists(inputFileName))
                {
                    ProcessHeader();
                    ProcessDetails();
                }
                else
                {
                    logData += $"File {inputFileName} does not exist.\n";
                }
            }
            catch (Exception ex)
            {
                logData += ex.Message + "\n";
            }
  
        }
    }
}
