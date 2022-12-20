namespace WindowsBanking
{
    partial class ProcessTransaction
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label accountNumberLabel;
            System.Windows.Forms.Label balanceLabel;
            System.Windows.Forms.Label fullNameLabel;
            System.Windows.Forms.Label clientNumberLabel;
            this.grpClient = new System.Windows.Forms.GroupBox();
            this.clientNumberMaskedLabel = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.clientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fullNameLabel1 = new System.Windows.Forms.Label();
            this.balanceLabel1 = new System.Windows.Forms.Label();
            this.bankAccountBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.accountNumberMaskedLabel = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.grpTransaction = new System.Windows.Forms.GroupBox();
            this.lblTransactionType = new System.Windows.Forms.Label();
            this.cboTransactionType = new System.Windows.Forms.ComboBox();
            this.transactionTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lnkReturn = new System.Windows.Forms.LinkLabel();
            this.lnkUpdate = new System.Windows.Forms.LinkLabel();
            this.cboPayeeAccount = new System.Windows.Forms.ComboBox();
            this.payeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblPayeeAccount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNoAdditionalAccounts = new System.Windows.Forms.Label();
            accountNumberLabel = new System.Windows.Forms.Label();
            balanceLabel = new System.Windows.Forms.Label();
            fullNameLabel = new System.Windows.Forms.Label();
            clientNumberLabel = new System.Windows.Forms.Label();
            this.grpClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankAccountBindingSource1)).BeginInit();
            this.grpTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transactionTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.payeeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // accountNumberLabel
            // 
            accountNumberLabel.AutoSize = true;
            accountNumberLabel.Location = new System.Drawing.Point(108, 73);
            accountNumberLabel.Name = "accountNumberLabel";
            accountNumberLabel.Size = new System.Drawing.Size(90, 13);
            accountNumberLabel.TabIndex = 0;
            accountNumberLabel.Text = "Account Number:";
            // 
            // balanceLabel
            // 
            balanceLabel.AutoSize = true;
            balanceLabel.Location = new System.Drawing.Point(404, 73);
            balanceLabel.Name = "balanceLabel";
            balanceLabel.Size = new System.Drawing.Size(49, 13);
            balanceLabel.TabIndex = 2;
            balanceLabel.Text = "Balance:";
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.Location = new System.Drawing.Point(404, 36);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new System.Drawing.Size(57, 13);
            fullNameLabel.TabIndex = 4;
            fullNameLabel.Text = "Full Name:";
            // 
            // clientNumberLabel
            // 
            clientNumberLabel.AutoSize = true;
            clientNumberLabel.Location = new System.Drawing.Point(108, 36);
            clientNumberLabel.Name = "clientNumberLabel";
            clientNumberLabel.Size = new System.Drawing.Size(76, 13);
            clientNumberLabel.TabIndex = 6;
            clientNumberLabel.Text = "Client Number:";
            // 
            // grpClient
            // 
            this.grpClient.Controls.Add(clientNumberLabel);
            this.grpClient.Controls.Add(this.clientNumberMaskedLabel);
            this.grpClient.Controls.Add(fullNameLabel);
            this.grpClient.Controls.Add(this.fullNameLabel1);
            this.grpClient.Controls.Add(balanceLabel);
            this.grpClient.Controls.Add(this.balanceLabel1);
            this.grpClient.Controls.Add(accountNumberLabel);
            this.grpClient.Controls.Add(this.accountNumberMaskedLabel);
            this.grpClient.Location = new System.Drawing.Point(47, 48);
            this.grpClient.Name = "grpClient";
            this.grpClient.Size = new System.Drawing.Size(694, 133);
            this.grpClient.TabIndex = 0;
            this.grpClient.TabStop = false;
            this.grpClient.Text = "Client Data";
            this.grpClient.Enter += new System.EventHandler(this.grpClient_Enter);
            // 
            // clientNumberMaskedLabel
            // 
            this.clientNumberMaskedLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "ClientNumber", true));
            this.clientNumberMaskedLabel.Location = new System.Drawing.Point(190, 36);
            this.clientNumberMaskedLabel.Name = "clientNumberMaskedLabel";
            this.clientNumberMaskedLabel.Size = new System.Drawing.Size(100, 23);
            this.clientNumberMaskedLabel.TabIndex = 7;
            // 
            // clientBindingSource
            // 
            this.clientBindingSource.DataSource = typeof(BankOfBIT_DB.Models.Client);
            // 
            // fullNameLabel1
            // 
            this.fullNameLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "FullName", true));
            this.fullNameLabel1.Location = new System.Drawing.Point(467, 36);
            this.fullNameLabel1.Name = "fullNameLabel1";
            this.fullNameLabel1.Size = new System.Drawing.Size(100, 23);
            this.fullNameLabel1.TabIndex = 5;
            // 
            // balanceLabel1
            // 
            this.balanceLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource1, "Balance", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.balanceLabel1.Location = new System.Drawing.Point(467, 68);
            this.balanceLabel1.Name = "balanceLabel1";
            this.balanceLabel1.Size = new System.Drawing.Size(100, 23);
            this.balanceLabel1.TabIndex = 3;
            this.balanceLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bankAccountBindingSource1
            // 
            this.bankAccountBindingSource1.DataSource = typeof(BankOfBIT_DB.Models.BankAccount);
            // 
            // accountNumberMaskedLabel
            // 
            this.accountNumberMaskedLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource1, "AccountNumber", true));
            this.accountNumberMaskedLabel.Location = new System.Drawing.Point(204, 73);
            this.accountNumberMaskedLabel.Name = "accountNumberMaskedLabel";
            this.accountNumberMaskedLabel.Size = new System.Drawing.Size(100, 23);
            this.accountNumberMaskedLabel.TabIndex = 1;
            // 
            // grpTransaction
            // 
            this.grpTransaction.Controls.Add(this.lblTransactionType);
            this.grpTransaction.Controls.Add(this.cboTransactionType);
            this.grpTransaction.Controls.Add(this.lnkReturn);
            this.grpTransaction.Controls.Add(this.lnkUpdate);
            this.grpTransaction.Controls.Add(this.cboPayeeAccount);
            this.grpTransaction.Controls.Add(this.txtAmount);
            this.grpTransaction.Controls.Add(this.lblPayeeAccount);
            this.grpTransaction.Controls.Add(this.label1);
            this.grpTransaction.Controls.Add(this.lblNoAdditionalAccounts);
            this.grpTransaction.Location = new System.Drawing.Point(47, 187);
            this.grpTransaction.Name = "grpTransaction";
            this.grpTransaction.Size = new System.Drawing.Size(694, 208);
            this.grpTransaction.TabIndex = 1;
            this.grpTransaction.TabStop = false;
            this.grpTransaction.Text = "Perform Transaction";
            // 
            // lblTransactionType
            // 
            this.lblTransactionType.AutoSize = true;
            this.lblTransactionType.Location = new System.Drawing.Point(170, 61);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Size = new System.Drawing.Size(93, 13);
            this.lblTransactionType.TabIndex = 8;
            this.lblTransactionType.Text = "Transaction Type:";
            // 
            // cboTransactionType
            // 
            this.cboTransactionType.DataSource = this.transactionTypeBindingSource;
            this.cboTransactionType.DisplayMember = "Description";
            this.cboTransactionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTransactionType.FormattingEnabled = true;
            this.cboTransactionType.Location = new System.Drawing.Point(303, 58);
            this.cboTransactionType.Name = "cboTransactionType";
            this.cboTransactionType.Size = new System.Drawing.Size(201, 21);
            this.cboTransactionType.TabIndex = 7;
            this.cboTransactionType.ValueMember = "TransactionTypeId";
            this.cboTransactionType.SelectedIndexChanged += new System.EventHandler(this.cboTransactionType_SelectedIndexChanged);
            // 
            // transactionTypeBindingSource
            // 
            this.transactionTypeBindingSource.DataSource = typeof(BankOfBIT_DB.Models.TransactionType);
            // 
            // lnkReturn
            // 
            this.lnkReturn.AutoSize = true;
            this.lnkReturn.Location = new System.Drawing.Point(319, 174);
            this.lnkReturn.Name = "lnkReturn";
            this.lnkReturn.Size = new System.Drawing.Size(80, 13);
            this.lnkReturn.TabIndex = 5;
            this.lnkReturn.TabStop = true;
            this.lnkReturn.Text = "Return to Client";
            this.lnkReturn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReturn_LinkClicked);
            // 
            // lnkUpdate
            // 
            this.lnkUpdate.AutoSize = true;
            this.lnkUpdate.Location = new System.Drawing.Point(221, 174);
            this.lnkUpdate.Name = "lnkUpdate";
            this.lnkUpdate.Size = new System.Drawing.Size(42, 13);
            this.lnkUpdate.TabIndex = 4;
            this.lnkUpdate.TabStop = true;
            this.lnkUpdate.Text = "Update";
            this.lnkUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdate_LinkClicked);
            // 
            // cboPayeeAccount
            // 
            this.cboPayeeAccount.DataSource = this.bankAccountBindingSource1;
            this.cboPayeeAccount.DisplayMember = "AccountNumber";
            this.cboPayeeAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayeeAccount.FormattingEnabled = true;
            this.cboPayeeAccount.Location = new System.Drawing.Point(303, 129);
            this.cboPayeeAccount.Name = "cboPayeeAccount";
            this.cboPayeeAccount.Size = new System.Drawing.Size(201, 21);
            this.cboPayeeAccount.TabIndex = 3;
            this.cboPayeeAccount.ValueMember = "BankAccountId";
            this.cboPayeeAccount.Visible = false;
            // 
            // payeeBindingSource
            // 
            this.payeeBindingSource.DataSource = typeof(BankOfBIT_DB.Models.Payee);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(303, 98);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(201, 20);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPayeeAccount
            // 
            this.lblPayeeAccount.AutoSize = true;
            this.lblPayeeAccount.Location = new System.Drawing.Point(218, 129);
            this.lblPayeeAccount.Name = "lblPayeeAccount";
            this.lblPayeeAccount.Size = new System.Drawing.Size(40, 13);
            this.lblPayeeAccount.TabIndex = 1;
            this.lblPayeeAccount.Text = "Payee:";
            this.lblPayeeAccount.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Amount:";
            // 
            // lblNoAdditionalAccounts
            // 
            this.lblNoAdditionalAccounts.Location = new System.Drawing.Point(300, 153);
            this.lblNoAdditionalAccounts.Name = "lblNoAdditionalAccounts";
            this.lblNoAdditionalAccounts.Size = new System.Drawing.Size(207, 33);
            this.lblNoAdditionalAccounts.TabIndex = 6;
            this.lblNoAdditionalAccounts.Text = "No accounts available to receive transfer.";
            this.lblNoAdditionalAccounts.Visible = false;
            // 
            // ProcessTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grpTransaction);
            this.Controls.Add(this.grpClient);
            this.Name = "ProcessTransaction";
            this.Text = "ProcessTransaction";
            this.Load += new System.EventHandler(this.ProcessTransaction_Load);
            this.grpClient.ResumeLayout(false);
            this.grpClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankAccountBindingSource1)).EndInit();
            this.grpTransaction.ResumeLayout(false);
            this.grpTransaction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transactionTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.payeeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpClient;
        private System.Windows.Forms.GroupBox grpTransaction;
        private System.Windows.Forms.Label lblNoAdditionalAccounts;
        private System.Windows.Forms.LinkLabel lnkReturn;
        private System.Windows.Forms.LinkLabel lnkUpdate;
        private System.Windows.Forms.ComboBox cboPayeeAccount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblPayeeAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource payeeBindingSource;
        private System.Windows.Forms.ComboBox cboTransactionType;
        private System.Windows.Forms.BindingSource transactionTypeBindingSource;
        private System.Windows.Forms.Label lblTransactionType;
        private System.Windows.Forms.BindingSource clientBindingSource;
        private System.Windows.Forms.BindingSource bankAccountBindingSource1;
        private EWSoftware.MaskedLabelControl.MaskedLabel clientNumberMaskedLabel;
        private System.Windows.Forms.Label fullNameLabel1;
        private System.Windows.Forms.Label balanceLabel1;
        private EWSoftware.MaskedLabelControl.MaskedLabel accountNumberMaskedLabel;
    }
}