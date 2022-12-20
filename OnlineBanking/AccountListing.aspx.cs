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
    public partial class AccountListing : System.Web.UI.Page
    {

        private BankOfBIT_DBContext db = new BankOfBIT_DBContext();

        /// <summary>
        /// Handles the page Load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
             
            if (!IsPostBack)
            {

                if (this.Page.User.Identity.IsAuthenticated)
                {

                    try
                    {
                        string clientString= Page.User.Identity.Name.Substring(0, Page.User.Identity.Name.IndexOf("@"));

                        int clientNumber = int.Parse(clientString);

                        Client client = db.Clients.Where(x => x.ClientNumber == clientNumber).SingleOrDefault();

                        Session["client"] = client;

                        lblClient.Text = client.FullName;

                        IQueryable<BankAccount> bankAccounts = db.BankAccounts.Where(x => x.ClientId == client.ClientId);

                        Session["bankAccounts"] = bankAccounts;

                        gvAccounts.DataSource = bankAccounts.ToList();

                        this.DataBind();
                    }
                    catch (Exception)
                    {
                        lblExceptionMessage.Visible = true;
                        lblExceptionMessage.Text = "an error occured";
                    }

                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }

            }          

        }

        /// <summary>
        /// Handles the Selected index change method of the grid view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {

            Session["accountNumber"] = gvAccounts.Rows[gvAccounts.SelectedIndex].Cells[1].Text;

            Response.Redirect("~/TransactionListing.aspx");
        }
    }
}