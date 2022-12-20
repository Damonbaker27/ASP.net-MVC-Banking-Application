using BankOfBIT_DB.Data;
using BankOfBIT_DB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace WindowsBanking
{
    public partial class ProcessTransaction : Form
    {
        ConstructorData constructorData;
        private BankOfBIT_DBContext db = new BankOfBIT_DBContext();

        /// Form can only be opened with a Constructor Data object
        /// containing client and account details.
        /// </summary>
        /// <param name="constructorData">Populated Constructor data object.</param>
        public ProcessTransaction(ConstructorData constructorData)
        {
            //Given, more code to be added.
            InitializeComponent();
            this.constructorData = constructorData;
            this.constructorData.Client = constructorData.Client;
            this.constructorData.BankAccount = constructorData.BankAccount;
        }

        /// <summary>
        /// Return to the Client Data form passing specific client and 
        /// account information within ConstructorData.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClientData client = new ClientData(constructorData);
            client.MdiParent = this.MdiParent;
            client.Show();
            this.Close();
        }
        /// <summary>
        /// Always display the form in the top right corner of the frame.
        /// </summary>
        private void ProcessTransaction_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0,0);

            try
            {
                accountNumberMaskedLabel.Mask = Utility.BusinessRules.AccountFormat(constructorData.BankAccount.Description);

                IQueryable<TransactionType> transactionTypes = db.TransactionTypes.Where(x => x.TransactionTypeId < 5);

                //set binding sources
                transactionTypeBindingSource.DataSource = transactionTypes.ToList();
                clientBindingSource.DataSource = constructorData.Client;
                bankAccountBindingSource1.DataSource = constructorData.BankAccount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}","An error occured.", MessageBoxButtons.OK);
            }

        }

        private void grpClient_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the selected index change action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {

            //deposit selected
            if(cboTransactionType.SelectedIndex == (int)TransactionTypes.DEPOSIT || cboTransactionType.SelectedIndex == (int)TransactionTypes.WITHDRAWAL)
            {           
                cboPayeeAccount.Visible = false;
                lblNoAdditionalAccounts.Visible = false;
                lblPayeeAccount.Visible = false;
                lnkUpdate.Enabled = true;          
            }         

            //bill payment selected
            if (cboTransactionType.SelectedIndex == (int)TransactionTypes.BILL_PAYMENT)
            {      
                cboPayeeAccount.Visible = true;
                lblNoAdditionalAccounts.Visible = false;
                lblPayeeAccount.Visible = true;
                lnkUpdate.Enabled = true;

                cboPayeeAccount.DataSource = db.Payees.ToList();
                cboPayeeAccount.DisplayMember = "Description";
                cboPayeeAccount.ValueMember = "PayeeId";
                           
            }

            //transfer selected
            if (cboTransactionType.SelectedIndex == (int)TransactionTypes.TRANSFER)
            {
                IQueryable<BankAccount> query = db.BankAccounts.Where(x => x.ClientId == constructorData.Client.ClientId
                                                                      && x.AccountNumber != constructorData.BankAccount.AccountNumber);
                // if no other accounts found
                if(query.Count() == 0)
                {
                    cboPayeeAccount.Visible = false;
                    lblNoAdditionalAccounts.Visible = true;
                    lnkUpdate.Enabled = false;
                }
                else // if 1 or more accounts found
                {
                    cboPayeeAccount.Visible = true;
                    lblNoAdditionalAccounts.Visible = false;
                    lnkUpdate.Enabled = true;

                    cboPayeeAccount.DataSource = query.ToList();
                    cboPayeeAccount.DisplayMember = "AccountNumber";
                    cboPayeeAccount.ValueMember = "BankAccountId";                                   
                }

            }

        }

        /// <summary>
        /// Handles the link clicked event of the link button "Update".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if the text in amount textbox is numeric.
            if (Numeric.IsNumeric(txtAmount.Text,System.Globalization.NumberStyles.Number))
            {
                TransactionManagerService.TransactionManagerClient service = new TransactionManagerService.TransactionManagerClient();
                double amount = Convert.ToDouble(txtAmount.Text);              
                
                //will execute if the amount field is less than the account balance.
                if (amount < constructorData.BankAccount.Balance)
                {
                    try
                    {
                        double newBalance = 0;

                        switch (cboTransactionType.SelectedIndex)
                        {
                            //if Deposit is selected.
                            case (int)TransactionTypes.DEPOSIT :
                                newBalance = (double)service.Deposit(constructorData.BankAccount.BankAccountId, amount, "Deposit");
                                break;
                           
                            //if Withdrawal is selected.
                            case (int)TransactionTypes.WITHDRAWAL :
                                
                                newBalance = (double)service.Withdrawal(constructorData.BankAccount.BankAccountId, amount, "Withdrawal");                               
                                break;
                            
                            //if Bill Payment is selected.
                            case (int)TransactionTypes.BILL_PAYMENT:
                                
                                newBalance = (double)service.BillPayment(constructorData.BankAccount.BankAccountId, amount, 
                                    $"Online Banking Payment to {cboPayeeAccount.Text}");                              
                                break;
                            
                            //if transfer is selected.
                            case (int)TransactionTypes.TRANSFER:
                                
                                newBalance = (double)service.Transfer(constructorData.BankAccount.BankAccountId, Convert.ToInt32(cboPayeeAccount.SelectedValue),
                                    amount, $"Online Banking Transfer From: {constructorData.BankAccount.AccountNumber} To: {cboPayeeAccount.Text}");                              
                                break;
                        }

                        balanceLabel1.Text = newBalance.ToString("C", CultureInfo.CurrentCulture);
                    
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error Completing Transaction.", "Transaction Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Insufficient funds exist for requested transaction.", "Insufficient Funds");
                }       
            }
            else
            {
                MessageBox.Show("Only numbers are permitted", "An error occured");
            }
        }
    }
}
