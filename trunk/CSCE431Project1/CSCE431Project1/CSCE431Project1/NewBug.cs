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
    public partial class NewBug : Form
    {
        String currentUser;
        Int32 currentUserID;
        Int32 currentProjectID;
        MySqlConnection conSQL;
        MySqlDataAdapter adap;
        MySqlCommand command;
        DataTable versionPool_dt, owner_dt, ownerPool_dt,
                  watcher_dt, watcherPool_dt, bugs_dt, reqs_dt, 
                  reqsPool_dt, reqsVersion_dt, reqsVersionLink_dt;

        public NewBug(MySqlConnection _conSQL, String currUser, Int32 currUserID, Int32 currProjID, DataTable dtVersions, DataTable dtBugs, DataTable dtRequirements)
        {
            // Save relevant parameters.
            currentUser = currUser;
            currentUserID = currUserID;
            currentProjectID = currProjID;
            versionPool_dt = dtVersions;
            bugs_dt = dtBugs;
            reqsPool_dt = dtRequirements;
            reqsVersion_dt = reqsPool_dt.Clone();
            reqs_dt = reqsPool_dt.Clone();
            // Save connection parameters.
            conSQL = _conSQL;
            command = new MySqlCommand("", conSQL);
            adap = new MySqlDataAdapter();
            adap.SelectCommand = command;
            // Initialize window componenents.
            InitializeComponent();
            try
            {
                // Populate with possible people.
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

                reqsVersionLink_dt = new DataTable();
                command.CommandText = "SELECT requirementversionlinks.* FROM requirementversionlinks, versions WHERE versionid = vid AND projectid = '" + currentProjectID + "';";
                adap.Fill(reqsVersionLink_dt);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.comboBoxReqs.DataSource = reqsVersion_dt.DefaultView;
            this.comboBoxReqs.DisplayMember = "Title";

            this.releaseComboBox.DataSource = versionPool_dt.DefaultView;
            this.releaseComboBox.DisplayMember = "version";

            this.ownersComboBox.DataSource = ownerPool_dt.DefaultView;
            this.ownersComboBox.DisplayMember = "username";

            this.watchersComboBox.DataSource = watcherPool_dt.DefaultView;
            this.watchersComboBox.DisplayMember = "username";

            this.requirementsListBox.DataSource = reqs_dt.DefaultView;
            this.requirementsListBox.DisplayMember = "Title";

            this.ownersListBox.DataSource = owner_dt.DefaultView;
            this.ownersListBox.DisplayMember = "username";

            this.watchersListBox.DataSource = watcher_dt.DefaultView;
            this.watchersListBox.DisplayMember = "username";
        }
        ~NewBug()
        {
            // Dispose.
            adap.Dispose();
            command.Dispose();
        }

        private void addBugButton_Click(object sender, EventArgs e)
        {
            try
            {
                string newTitle_st = newTitleText.Text;
                string newReqDesc_st = newDescText.Text;
                string newPriority_st = newPriorityCombo.Text;
                string newTimeOpen_st = DateTime.Now.ToString(); 
                string newStatus_st = "Open"; //Open, In Progress, Closed

                // Set command to add bug
                command.CommandText = "INSERT INTO bugs VALUES(null, '" + newTitle_st + "', '" + newReqDesc_st + "', '" + newStatus_st +
                     "', NOW(), '', 'notes', '" + newPriority_st + "');";

                // Execute the command
                command.ExecuteNonQuery();
                // Get last inserted requirement.
                DataTable newTable = new DataTable();
                adap.SelectCommand.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority " +
                                                 "FROM bugs WHERE bid = LAST_INSERT_ID();";
                adap.Fill(newTable);
                // Update our data table of bugs.
                DataRow newRow = bugs_dt.NewRow();

                newRow.ItemArray = newTable.Rows[0].ItemArray;
                bugs_dt.Rows.Add(newRow);
                bugs_dt.AcceptChanges();

                // Next add links.
                String InsertLinks = "";
                Int32 bugID = Convert.ToInt32(newRow[0]);
                for (int i = 0; i < watcher_dt.Rows.Count; ++i)
                    InsertLinks += "INSERT INTO userbuglinks VALUES(null, " + watcher_dt.Rows[i][1].ToString() + ", " + bugID.ToString() + ", 'watcher');";
                for (int i = 0; i < owner_dt.Rows.Count; ++i)
                    InsertLinks += "INSERT INTO userbuglinks VALUES(null, " + owner_dt.Rows[i][1].ToString() + ", " + bugID.ToString() + ", 'owner');";
                //insert the originator
                InsertLinks += "INSERT INTO userbuglinks VALUES(null, " + Convert.ToString(currentUserID) + ", " + bugID.ToString() + ", 'originator');";

                String SelectLinks = "requirementid IN (";
                // Now create requirement links and save id to get version from requirementversionlinks.
                foreach (DataRow dr in this.reqs_dt.Rows)
                {
                    InsertLinks += "INSERT INTO bugreqlinks VALUES(null, " + bugID.ToString() + ", " + dr["ReqID"] + ");";
                    SelectLinks += String.Format("'{0}',", dr["ReqID"].ToString());
                }
                SelectLinks += ")";
                DataRow[] drc = reqsVersionLink_dt.Select(SelectLinks);
                // Now get versions.
                foreach (DataRow dr in drc)
                {
                    InsertLinks += "INSERT INTO bugversionlinks VALUES(null, " + bugID.ToString() + ", " + dr["versionid"] + ");";
                }
                command.CommandText = InsertLinks;
                // Execute the command
                command.ExecuteNonQuery();

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

        private void ownersAddButton_Click_1(object sender, EventArgs e)
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

        private void watchersAddButton_Click_1(object sender, EventArgs e)
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

        private void ownersListBox_DoubleClick(object sender, EventArgs e)
        {
            if (ownersListBox.SelectedIndex < 0)
                return;
            DataRow newRow = ownerPool_dt.NewRow();
            newRow.ItemArray = owner_dt.Rows[ownersListBox.SelectedIndex].ItemArray;
            ownerPool_dt.Rows.Add(newRow);
            owner_dt.Rows[ownersListBox.SelectedIndex].Delete();
            ownerPool_dt.AcceptChanges();
            owner_dt.AcceptChanges();
        }

        private void watchersListBox_DoubleClick(object sender, EventArgs e)
        {
            if (watchersListBox.SelectedIndex < 0)
                return;
            DataRow newRow = watcherPool_dt.NewRow();
            newRow.ItemArray = watcher_dt.Rows[watchersListBox.SelectedIndex].ItemArray;
            watcherPool_dt.Rows.Add(newRow);
            watcher_dt.Rows[watchersListBox.SelectedIndex].Delete();
            watcherPool_dt.AcceptChanges();
            watcher_dt.AcceptChanges();
        }

        private void NewBug_Load(object sender, EventArgs e)
        {

        }

        private void ownersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void releaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 vid = Convert.ToInt32(versionPool_dt.Rows[releaseComboBox.SelectedIndex]["vid"]);
            DataRow[] drc = reqsVersionLink_dt.Select("versionid = '" + vid.ToString() + "'");
            String reqsVersionTxt = "ReqID IN (";
            foreach (DataRow dr in drc)
                reqsVersionTxt += String.Format("'{0}',", dr["requirementid"].ToString());
            reqsVersionTxt += ")";
            reqsVersion_dt.Clear();
            drc = reqsPool_dt.Select(reqsVersionTxt);
            foreach (DataRow dr in drc)
            {
                DataRow newRow = reqsVersion_dt.NewRow();
                newRow.ItemArray = dr.ItemArray;
                reqsVersion_dt.Rows.Add(newRow);
            }
            reqsVersion_dt.AcceptChanges();
        }

        private void buttonReqs_Click(object sender, EventArgs e)
        {
            if (this.comboBoxReqs.SelectedIndex < 0)
                return;
            DataRow newRow = reqs_dt.NewRow();
            newRow.ItemArray = reqsVersion_dt.Rows[comboBoxReqs.SelectedIndex].ItemArray;
            // If it exists, skip it.
            if (reqs_dt.Rows.Find(newRow["ReqID"]) != null)
                return;
            reqs_dt.Rows.Add(newRow);
            reqs_dt.AcceptChanges();
            watcherPool_dt.AcceptChanges();
        }
    }
}
