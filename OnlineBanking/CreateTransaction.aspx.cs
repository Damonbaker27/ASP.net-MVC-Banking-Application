using BankOfBIT_DB.Data;
using BankOfBIT_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBanking
{
    public partial class CreateTransaction : System.Web.UI.Page
    {
        private BankOfBIT_DBContext db = new BankOfBIT_DBContext();
       
        /// <summary>
        /// Handles the page load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.User.Identity.IsAuthenticated)
                {

                    int accountNumber = Convert.ToInt32(Session["accountNumber"]);

                    this.txtAmount.Style.Add("text-align", "right");
                    lblAccountNumber.Text = Session["accountNumber"].ToString();

                    //set the account balance text field to account balance.
                    BankAccount bankAccount = (BankAccount)Session["bankAccount"];                                      
                    lblBalance.Text = bankAccount.Balance.ToString();

                    // Query and populate the Transaction drop down list 
                    IQueryable<TransactionType> transactionTypes = db.TransactionTypes.Where(x => x.TransactionTypeId == 3 || x.TransactionTypeId == 4);
                    ddlTransactionType.DataSource = transactionTypes.ToList();
                    ddlTransactionType.DataTextField = "Description";
                    ddlTransactionType.DataValueField = "TransactionTypeId";          
                    this.DataBind();

                    // Query and populate the payee/account drop down list.
                    IQueryable<Payee> payees = db.Payees;
                    ddlToPayee.DataSource = payees.ToList();
                    ddlToPayee.DataTextField = "Description";
                    ddlToPayee.DataValueField = "PayeeId";
                    this.DataBind();

                }
            }

        }

        /// <summary>
        /// Handles the selected index change for the transaction type drop down list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlToPayee.DataSource = null;
            ddlToPayee.DataTextField = null;
            ddlToPayee.DataValueField = null;

            //if bill payment selected.
            if (ddlTransactionType.SelectedIndex == 0)
            {             
                IQueryable<Payee> payees = db.Payees;

                ddlToPayee.DataSource = payees.ToList();
                ddlToPayee.DataTextField = "Description";
                ddlToPayee.DataValueField = "PayeeId";
                this.DataBind();

            }
            
            //if Transfer is selected.
            if(ddlTransactionType.SelectedIndex == 1)
            {
                Client client = (Client)Session["client"];
                BankAccount bankAccount = (BankAccount)Session["bankAccount"];

                IQueryable<BankAccount> bankAccounts = db.BankAccounts.Where(x => x.ClientId == client.ClientId 
                                                                             && x.AccountNumber != bankAccount.AccountNumber);

                ddlToPayee.DataSource= bankAccounts.ToList();
                ddlToPayee.DataTextField = "AccountNumber";
                ddlToPayee.DataValueField = "BankAccountId";
                this.DataBind();

            }                   
 
        }

        /// <summary>
        /// Handles the click event of the complete transaction button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        protected void lbCompleteTransaction_Click(object sender, EventArgs e)
        {

            rfvAmount.Enabled = true;

            if (Page.IsValid)
            {

                BankAccount bankAccount = (BankAccount)Session["bankAccount"];
                double amount = double.Parse(txtAmount.Text);

                try
                {
                    if (bankAccount.Balance >= amount)
                    {
                        ServiceReference1.TransactionManagerClient service = new ServiceReference1.TransactionManagerClient();

                        //if Bill Payment is selected
                        if (ddlTransactionType.SelectedIndex == 0)
                        {
                            try
                            {
                                double newBalance = (double)service.BillPayment(bankAccount.BankAccountId, amount,
                                    $"Online Banking Payment to: {ddlToPayee.SelectedItem.Text}");

                                lblBalance.Text = newBalance.ToString();
                            }
                            catch (Exception ex)
                            {

                                lblExceptionMessage.Visible = true;
                                lblExceptionMessage.Text = ex.Message;
                            }

                        }


                        //if Transfer is selected
                        if (ddlTransactionType.SelectedIndex == 1)
                        {
                            int toAccountId = Convert.ToInt32(ddlToPayee.SelectedValue);

                            try
                            {
                                double newBalance = (double)service.Transfer(bankAccount.BankAccountId, toAccountId, amount,
                                    $"Online Banking Transfer From:   {bankAccount.AccountNumber} To:   {ddlToPayee.SelectedItem.Text}");

                                lblBalance.Text = newBalance.ToString();

                            }
                            catch (Exception ex)
                            {
                                lblExceptionMessage.Visible = true;
                                lblExceptionMessage.Text = ex.Message;

                            }

                        }

                    }
                    else
                    {
                        throw new Exception("Insufficient funds.");
                    }
                }
                catch (Exception ex)
                {

                    lblExceptionMessage.Visible = true;
                    lblExceptionMessage.Text = ex.Message;
                }

            }

        }

        /// <summary>
        /// Handles the click event of return to account listing button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbReturnToAccountListing_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/AccountListing");

        }
    }
}