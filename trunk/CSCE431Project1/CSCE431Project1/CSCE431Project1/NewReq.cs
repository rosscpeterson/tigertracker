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
                adap.SelectCommand = command;
                // Potential owners.
                ownerPool_dt = new DataTable();
                adap.Fill(ownerPool_dt);
                owner_dt = ownerPool_dt.Clone();
                // Potential watchers.
                watcherPool_dt = new DataTable();
                adap.Fill(watcherPool_dt);
                watcher_dt = watcherPool_dt.Clone();
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

            this.releaseListBox.DataSource = version_dt.DefaultView;
            this.releaseListBox.DisplayMember = "version";

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
                command.CommandText = "INSERT INTO requirements VALUES(null, '" + newTitle_st + "', '" + newReqDesc_st + "', '" + newPriority_st + "', " +
                    /*newTimeOpen_stDateTime.Now+*/ "NOW(), null, '" + newStatus_st + "', null);";

                // Execute the command
                command.ExecuteNonQuery();
                // Get last inserted requirement.
                DataTable newTable = new DataTable();
                adap.SelectCommand.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                 " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes " +
                                 "FROM requirements WHERE rid = LAST_INSERT_ID();";
                adap.Fill(newTable);
                // Update our data table of requirements.
                DataRow newRow = requirement_dt.NewRow();
                
                newRow.ItemArray = newTable.Rows[0].ItemArray;
                requirement_dt.Rows.Add(newRow);
                requirement_dt.AcceptChanges();
                
                // Next add links.
                String InsertLinks = "";
                Int32 reqID = Convert.ToInt32(newRow[0]);
                for (int i = 0; i < watcher_dt.Rows.Count; ++i)
                    InsertLinks += "INSERT INTO userrequirementlinks VALUES(null, " + watcher_dt.Rows[i][1].ToString() + ", " + reqID.ToString() + ", 'watcher');";
                for (int i = 0; i < owner_dt.Rows.Count; ++i)
                    InsertLinks += "INSERT INTO userrequirementlinks VALUES(null, " + owner_dt.Rows[i][1].ToString() + ", " + reqID.ToString() + ", 'owner');";
                
                // Now create version links.
                foreach (DataRow dr in this.version_dt.Rows)
                    InsertLinks += "INSERT INTO requirementversionlinks VALUES(null, " + dr[0].ToString() + ", 'Not Satisfied', " + reqID.ToString() + ", null);";

                command.CommandText = InsertLinks;
                // Execute the command
                command.ExecuteNonQuery();

                //close the window
                this.Close();
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

        private void releaseAddButton_Click(object sender, EventArgs e)
        {
            if (releaseComboBox.SelectedIndex < 0)
                return;
            DataRow newRow = version_dt.NewRow();
            newRow.ItemArray = versionPool_dt.Rows[releaseComboBox.SelectedIndex].ItemArray;
            version_dt.Rows.Add(newRow);
            versionPool_dt.Rows[releaseComboBox.SelectedIndex].Delete();
            version_dt.AcceptChanges();
            versionPool_dt.AcceptChanges();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        

    }
}
