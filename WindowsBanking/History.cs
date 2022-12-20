using BankOfBIT_DB.Data;
using BankOfBIT_DB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace WindowsBanking
{
    public partial class History : Form
    {
        ConstructorData constructorData;

        private BankOfBIT_DBContext db = new BankOfBIT_DBContext();


        /// <summary>
        /// Form can only be opened with a Constructor Data object
        /// containing client and account details.
        /// </summary>
        /// <param name="constructorData">Populated Constructor data object.</param>
        public History(ConstructorData constructorData)
        {
            //Given, more code to be added.
            InitializeComponent();
            this.constructorData = constructorData;

            this.bankAccountBindingSource.DataSource = constructorData.BankAccount;
                           
        }


        /// <summary>
        /// Return to the Client Data form passing specific client and 
        /// account information within ConstructorData.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // added this because it is empty otherwise and causes and exception on client data when returning.
            constructorData.Client = constructorData.BankAccount.Client;           
            
            ClientData client = new ClientData(constructorData);
            client.MdiParent = this.MdiParent;
            client.Show();
            this.Close();
        }
        /// <summary>
        /// Always display the form in the top right corner of the frame.
        /// </summary>
        private void History_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            //set the label mask according to account type.
            accountNumberMaskedLabel.Mask = BusinessRules.AccountFormat(constructorData.BankAccount.Description);

            try
            {
                //find the transactions that correspond to the client id and bank account id.
                var query = from transactions in db.Transactions

                            join bankAccounts in db.BankAccounts
                            on transactions.BankAccountId equals bankAccounts.BankAccountId

                            where bankAccounts.ClientId == this.constructorData.BankAccount.ClientId && 
                            bankAccounts.BankAccountId == this.constructorData.BankAccount.BankAccountId
                            
                            select new { deposit = transactions.Deposit, Withdrawal = transactions.Withdrawal, Notes = transactions.Notes, 
                                transactionType = transactions.TransactionType.Description, DateCreated = transactions.DateCreated }; 


                transactionDataGridView.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}","An error occured", MessageBoxButtons.OK);
            }        

        }

        private void clientBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
