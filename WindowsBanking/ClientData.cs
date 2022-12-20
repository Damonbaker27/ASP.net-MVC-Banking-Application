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

namespace WindowsBanking
{
    public partial class ClientData : Form
    {
        ConstructorData constructorData = new ConstructorData();

        private BankOfBIT_DBContext db = new BankOfBIT_DBContext();       

        /// <summary>
        /// This constructor will execute when the form is opened
        /// from the MDI Frame.
        /// </summary>
        public ClientData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This constructor will execute when the form is opened by
        /// returning from the History or Transaction forms.
        /// </summary>
        /// <param name="constructorData">Populated ConstructorData object.</param>
        public ClientData(ConstructorData constructorData)
        {
            //Given:
            InitializeComponent();
            this.constructorData = constructorData;

            //More code to be added:
            this.constructorData.Client = constructorData.Client;
            this.constructorData.BankAccount = constructorData.BankAccount;

            clientNumberMaskedTextBox.Text = this.constructorData.Client.ClientNumber.ToString();

            clientNumberMaskedTextBox_Leave(null, null);

        }

        /// <summary>
        /// Open the Transaction form passing ConstructorData object.
        /// </summary>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Given, more code to be added.

            int clientNumber = Convert.ToInt32(clientNumberMaskedTextBox.Text);
            int accountId = Convert.ToInt32(accountNumberComboBox.SelectedValue);

            this.constructorData.Client = db.Clients.Where(x => x.ClientNumber == clientNumber).SingleOrDefault();
            this.constructorData.BankAccount = db.BankAccounts.Where(x => x.BankAccountId == accountId).SingleOrDefault();

            ProcessTransaction transaction = new ProcessTransaction(constructorData);
            transaction.MdiParent = this.MdiParent;
            transaction.Show();
            this.Close();
        }

        /// <summary>
        /// Open the History form passing ConstructorData object.
        /// </summary>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Given, more code to be added.

            int accountId = Convert.ToInt32(accountNumberComboBox.SelectedValue);
            this.constructorData.BankAccount = db.BankAccounts.Where(x => x.BankAccountId == accountId).SingleOrDefault();
            
            History history = new History(constructorData);
            history.MdiParent = this.MdiParent;
            history.Show();
            this.Close();
        }

        /// <summary>
        /// Always display the form in the top right corner of the frame.
        /// </summary>
        private void ClientData_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0,0);

        }

        private void notesLabel1_Click(object sender, EventArgs e)
        {

        }

        private void balanceLabel1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the leave event of the masked text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clientNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {

            if (clientNumberMaskedTextBox.MaskCompleted)
            {

                int clientNumber = Convert.ToInt32(clientNumberMaskedTextBox.Text);
                
                Client client = db.Clients.Where(x => x.ClientNumber == clientNumber).SingleOrDefault();

                if(client == null)
                {
                    MessageBox.Show($"Client Number: {clientNumber} does not exist.","Invalid Client Number", MessageBoxButtons.OK);

                    llProcessTransaction.Enabled = false;
                    llViewDetails.Enabled = false;
                    clientNumberMaskedTextBox.Focus();

                    clientBindingSource.DataSource = typeof(Client);
                    bankAccountBindingSource.DataSource = typeof(BankAccount);

                }
                else
                {

                    // executes when the client query retrieved a client.
                    clientBindingSource.DataSource = client;

                    IQueryable<BankAccount> bankAccounts = db.BankAccounts.Where(x => x.ClientId == client.ClientId);

                    //If no bank accounts were found.
                    if (!bankAccounts.Any())
                    {
                        llProcessTransaction.Enabled = false;
                        llViewDetails.Enabled = false;

                        bankAccountBindingSource.DataSource = typeof(BankAccount);

                    }
                    else //Bank accounts were found
                    {
                        bankAccountBindingSource.DataSource = bankAccounts.ToList();
                        llProcessTransaction.Enabled = true;
                        llViewDetails.Enabled = true;

                        if (constructorData.BankAccount != null)
                        {
                            accountNumberComboBox.Text = constructorData.BankAccount.AccountNumber.ToString();
                        }
                       
                    }
                }
                
            }
            
            

        }
    }
}
