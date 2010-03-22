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
        DataTable version_dt, owner_dt, ownerPool_dt,
                  watcher_dt, watcherPool_dt, bugs_dt;

        public NewBug(MySqlConnection _conSQL, String currUser, Int32 currUserID, Int32 currProjID, DataTable dtVersions, DataTable dtBugs)
        {
            // Save relevant parameters.
            currentUser = currUser;
            currentUserID = currUserID;
            currentProjectID = currProjID;
            version_dt = dtVersions;
            bugs_dt = dtBugs;
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

                // Potential owners.
                adap.SelectCommand = command;
                owner_dt = new DataTable();
                ownerPool_dt = new DataTable();
                adap.Fill(owner_dt);
                owner_dt.Rows.Clear();
                adap.Fill(ownerPool_dt);
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

            this.releaseComboBox.DataSource = version_dt.DefaultView;
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
        ~NewBug()
        {
            // Dispose.
            adap.Dispose();
            command.Dispose();
        }

        private void addBugButton_Click(object sender, EventArgs e)
        {
            string newTitle_st = newTitleText.Text;
            string newReqDesc_st = newDescText.Text;
            string newPriority_st = newPriorityCombo.Text;
            string newTimeOpen_st = DateTime.Now.ToString("dd/MM/yyyy HH:MM:ss"); //or DateTime.Now.ToString("dd/MM/yyyy h:MM tt")
            string newStatus_st = "Open"; //Open, In Progress, Closed
            // For each version in combo box, do an insertion?  For now, default to top.
            // Set command to add requirement
            command.CommandText = "INSERT INTO bugs VALUES(null, '" + newTitle_st + "', '" + newReqDesc_st + "', '" + newPriority_st + "', '" +
                                      newTimeOpen_st + "', null, '" + newStatus_st + "', null, " + version_dt.Rows[0][2].ToString() + ");"; 

            // Execute the command
            command.ExecuteNonQuery();
            // Get last inserted bug.
            DataTable newTable = new DataTable();
            adap.SelectCommand.CommandText = "SELECT * FROM bugs WHERE bid = LAST_INSERT_ID();";
            adap.Fill(newTable);
            // Update our data table of requirements.
            DataRow newRow = bugs_dt.NewRow();
            /*for (int i = 0; i < newRow.ItemArray.GetLength(0); ++i)
                newRow[i] = newTable.Rows[0][i];*/
            newRow.ItemArray = newTable.Rows[0].ItemArray;
            bugs_dt.Rows.Add(newRow);
            bugs_dt.AcceptChanges();

            // Next add links.
            String InsertLinks = "";
            Int32 bugID = (Int32)newRow[0];
            for (int i = 0; i < watcher_dt.Rows.Count; ++i)
                InsertLinks += "INSERT INTO userbuglinks VALUES(null, " + watcher_dt.Rows[i][1].ToString() + " " + bugID.ToString() + ", 'watcher');";
            for (int i = 0; i < owner_dt.Rows.Count; ++i)
                InsertLinks += "INSERT INTO userbuglinks VALUES(null, " + owner_dt.Rows[i][1].ToString() + " " + bugID.ToString() + ", 'owner');";
            command.CommandText = InsertLinks;
            // Execute the command
            command.ExecuteNonQuery();

            // Now create version links.
            // ? I'm confused.
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

        private void releasesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newVersion = version_dt.Rows[this.releaseComboBox.SelectedIndex][0].ToString(); //THIS WILL CHANGE TO STRING? ****
            releasesListBox.Items.Add(newVersion.ToString() + ", ");
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

    }
}
