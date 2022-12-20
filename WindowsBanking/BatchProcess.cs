using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankOfBIT_DB.Data;

namespace WindowsBanking
{
    public partial class BatchProcess : Form
    {
        BankOfBIT_DBContext db = new BankOfBIT_DBContext();

        public BatchProcess()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Always display the form in the top right corner of the frame.
        /// </summary>
        private void BatchProcess_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0,0);
            institutionBindingSource.DataSource = db.Institutions.ToList();
        }

        /// <summary>
        /// handles the process transaction link label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Batch batch = new Batch();
           
            if (radSelect.Checked)
            {
                batch.ProcessTransmission($"{cboInstitutions.SelectedValue}", "this is a key");
                rtxtLog.Text += batch.WriteLogData();
            }

            if (radAll.Checked)
            {
                foreach (var institution in db.Institutions)
                {
                    batch.ProcessTransmission($"{institution.InstitutionNumber}", "this is a key");
                    rtxtLog.Text += batch.WriteLogData();
                }    
            }
            //given:  Ensure key has been entered.  Note: for use with Assignment 9
            //if(txtKey.Text.Length == 0)
            //{
            //    MessageBox.Show("Please enter a key to decrypt the input file(s).", "Key Required");
            //}
        }

        /// <summary>
        /// Handles the check changed event of the radio buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radSelect.Checked)
            {
                cboInstitutions.Visible = true;
            }
            else
            {
                cboInstitutions.Visible = false;
            }
        }
    }
}
