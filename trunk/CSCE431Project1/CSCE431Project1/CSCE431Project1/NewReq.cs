using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace CSCE431Project1
{
    public partial class NewReq : Form
    {
        String currentUser;
        Int32 currentUserID;
        Int32 currentProjectID;
        MySqlConnection conSQL;
        MySqlDataAdapter adap;
        MySqlCommand command;
        DataTable version_dt, owner_dt, ownerPool_dt, 
                  watcher_dt, versionPool_dt, watcherPool_dt, requirement_dt;

        public NewReq(MySqlConnection _conSQL, String currUser, Int32 currUserID, Int32 currProjID, DataTable dtVersions, DataTable dtRequirements)
        {
            // Save relevant parameters.
            currentUser = currUser;
            currentUserID = currUserID;
            currentProjectID = currProjID;
            versionPool_dt = dtVersions;
            version_dt = versionPool_dt.Clone();
            requirement_dt = dtRequirements;
            // Save connection parameters.
            conSQL = _conSQL;
            command = new MySqlCommand("", conSQL);
            adap = new MySqlDataAdapter();
            adap.SelectCommand = command;
            // Initialize window componenents.
            InitializeComponent();
            try
            {
                // Populate with possible people (people that belong to project).
                command.CommandText = "SELECT users.username, userprojectlinks.userid, userprojectlinks.projectid FROM users, userprojectlinks"
                                    + " WHERE users.uid = userprojectlinks.userid AND userprojectlinks.projectid = '" + currentProjectID + "';";
                // Potential owners.
                adap.SelectCommand = command;
                //owner_dt = new DataTable();
                ownerPool_dt = new DataTable();
                //adap.Fill(owner_dt);
                //owner_dt.Rows.Clear();
                adap.Fill(ownerPool_dt);
                owner_dt = ownerPool_dt.Clone();
                // Potential watchers.
                watcher_dt = new DataTable();
                watcherPool_dt = new DataTable();
                adap.Fill(watcher_dt);
                watcher_dt.Rows.Clear();
                adap.Fill(watcherPool_dt);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.releaseComboBox.DataSource = versionPool_dt.DefaultView;
            this.releaseComboBox.DisplayMember = "version";

            this.ownersComboBox.DataSource = ownerPool_dt.DefaultView;
            this.ownersComboBox.DisplayMember = "username";

            this.watchersComboBox.DataSource = watcherPool_dt.DefaultView;
            this.watchersComboBox.DisplayMember = "username";

            this.ownersListBox.DataSource = owner_dt.DefaultView;
            this.ownersListBox.DisplayMember = "username";

            this.watchersListBox.DataSource = watcher_dt.DefaultView;
            this.watchersListBox.DisplayMember = "username";
        }
        ~NewReq()
        {
            // Dispose.
            adap.Dispose();
            command.Dispose();
        }

        private void NewReq_Load(object sender, EventArgs e)
        {

        }

        private void addReqButton_Click(object sender, EventArgs e)
        {
            try
            {
                string newTitle_st = newTitleText.Text;
                string newReqDesc_st = newDescText.Text;
                string newPriority_st = newPriorityCombo.Text;
                string newTimeOpen_st = DateTime.Now.ToString("dd/MM/yyyy HH:MM:ss"); //or DateTime.Now.ToString("dd/MM/yyyy h:MM tt")
                string newStatus_st = "Open"; //Open, In Progress, Closed

                // Set command to add requirement
                command.CommandText = "INSERT INTO requirements VALUES(null, '" + newTitle_st + "', '" + newReqDesc_st + "', '" + newPriority_st + "', '" +
                                      newTimeOpen_st + "', null, '" + newStatus_st + "', null);";

                // Execute the command
                command.ExecuteNonQuery();
                // Get last inserted requirement.
                DataTable newTable = new DataTable();
                adap.SelectCommand.CommandText = "SELECT * FROM requirements WHERE rid = LAST_INSERT_ID();";
                adap.Fill(newTable);
                // Update our data table of requirements.
                DataRow newRow = requirement_dt.NewRow();
                /*for (int i = 0; i < newRow.ItemArray.GetLength(0); ++i)
                    newRow[i] = newTable.Rows[0][i];*/
                newRow.ItemArray = newTable.Rows[0].ItemArray;
                requirement_dt.Rows.Add(newRow);
                requirement_dt.AcceptChanges();
                
                // Next add links.
                String InsertLinks = "";
                Int32 reqID = (Int32)newRow[0];
                for (int i = 0; i < watcher_dt.Rows.Count; ++i)
                    InsertLinks += "INSERT INTO userrequirementlinks VALUES(null, " + watcher_dt.Rows[i][1].ToString() + " " + reqID.ToString() + ", 'watcher');";
                for (int i = 0; i < owner_dt.Rows.Count; ++i)
                    InsertLinks += "INSERT INTO userrequirementlinks VALUES(null, " + owner_dt.Rows[i][1].ToString() + " " + reqID.ToString() + ", 'owner');";
                
                // Now create version links.
                foreach (DataRow dr in this.version_dt.Rows)
                    InsertLinks += "INSERT INTO requirementversionlinks VALUES(null, " + dr[0].ToString() + ", null, " + reqID.ToString() + ", null);";

                command.CommandText = InsertLinks;
                // Execute the command
                command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void releaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newVersion = version_dt.Rows[this.releaseComboBox.SelectedIndex][0].ToString(); //THIS WILL CHANGE TO STRING? ****
            releasesListBox.Items.Add(newVersion.ToString() + ", ");

        }

        private void ownersAddButton_Click(object sender, EventArgs e)
        {
            if (ownersComboBox.SelectedIndex < 0)
                return;
            DataRow newRow = owner_dt.NewRow();
            newRow.ItemArray = ownerPool_dt.Rows[ownersComboBox.SelectedIndex].ItemArray;
            owner_dt.Rows.Add(newRow);
            ownerPool_dt.Rows[ownersComboBox.SelectedIndex].Delete();
            owner_dt.AcceptChanges();
            ownerPool_dt.AcceptChanges();
        }

        private void watchersAddButton_Click(object sender, EventArgs e)
        {
            if (watchersComboBox.SelectedIndex < 0)
                return;
            DataRow newRow = watcher_dt.NewRow();
            newRow.ItemArray = watcherPool_dt.Rows[watchersComboBox.SelectedIndex].ItemArray;
            watcher_dt.Rows.Add(newRow);
            watcherPool_dt.Rows[watchersComboBox.SelectedIndex].Delete();
            watcher_dt.AcceptChanges();
            watcherPool_dt.AcceptChanges();
        }

        

    }
}
