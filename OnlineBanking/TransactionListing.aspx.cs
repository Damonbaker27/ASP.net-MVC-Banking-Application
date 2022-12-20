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
    public partial class TransactionListing : System.Web.UI.Page
    {

        private BankOfBIT_DBContext db = new BankOfBIT_DBContext();


        /// <summary>
        /// Handles the Page load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.User.Identity.IsAuthenticated)
                {

                    try
                    {
                        Client client = (Client)Session["client"];
                        
                        lblClientName.Text = client.FullName;

                        int accountNumber = Convert.ToInt32(Session["accountNumber"]);

                        BankAccount bankAccount = db.BankAccounts.Where(x => x.AccountNumber == accountNumber).SingleOrDefault();

                        Session["bankAccount"] = bankAccount;

                        lblAccountNumber.Text = "Account Number: " + accountNumber.ToString();
                      
                        lblBalance.Text = "Balance: " + String.Format("{0:c}", bankAccount.Balance);

                        IEnumerable<Transaction> transactions = db.Transactions.Where(x => x.BankAccountId == bankAccount.BankAccountId);
                       

                        gvTransactions.DataSource = transactions.ToList();

                        this.DataBind();
                    }
                    catch (Exception)
                    {
                        lblExceptionMessage.Visible = true;
                        lblExceptionMessage.Text = "an error occured while processing request.";
                        
                    }

                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }

            }


        }

        /// <summary>
        /// Handles the click event of pay bills button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbPayBills_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateTransaction.aspx");

        }

        /// <summary>
        /// Handles the click event of the Return to account listing button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccountListing.aspx");
        }
    }
}