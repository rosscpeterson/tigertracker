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
        string connectionString;
        string currentUser;
        int currentUserID;
        int currentProjectID;
        MySqlConnection conSQL;
        MySqlDataAdapter adap;
        MySqlCommand command;
        DataTable release_dt, owner_dt, watcher_dt;
        DataSet owner_ds, watcher_ds;

        public NewReq(string connString, string currUser, int currUserID, int currProjID)
        {
            connectionString = connString;
            currentUser = currUser;
            int currentUserID = currUserID;
            int currentProjectID = currProjID;

            // Start connection.
            conSQL = new MySqlConnection(connectionString);
            conSQL.Open();
            adap = new MySqlDataAdapter();
            command = conSQL.CreateCommand();

            InitializeComponent();
            try
            {
                //establish the releases combo box
                command.CommandText = "SELECT versions.version FROM versions WHERE projectid = '" + currentProjectID + "';";

                adap.SelectCommand = command;
                release_dt = new DataTable();   //declared above
                adap.Fill(release_dt);

                //populate the users_dt data table
                command.CommandText = "SELECT users.username, userprojectlinks.userid, userprojectlinks.projectid FROM users, userprojectlinks"
                                        + " WHERE users.uid = userprojectlinks.userid AND userprojectlinks.projectid = '" + currentProjectID + "';";

                adap.SelectCommand = command;
                owner_ds = new DataSet();   //declared above
                adap.Fill(owner_ds, "New Table");
                owner_dt = owner_ds.Tables[0];

                watcher_ds = new DataSet();
                adap.Fill(watcher_ds, "New Table");
                watcher_dt = watcher_ds.Tables[0];

            }

            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.releaseComboBox.DataSource = release_dt.DefaultView;
            this.releaseComboBox.DisplayMember = "username";

            this.ownersComboBox.DataSource = owner_dt.DefaultView;
            this.ownersComboBox.DisplayMember = "username";

            this.watchersComboBox.DataSource = watcher_dt.DefaultView;
            this.watchersComboBox.DisplayMember = "username";
        }

        private void NewReq_Load(object sender, EventArgs e)
        {

        }

        private void addReqButton_Click(object sender, EventArgs e)
        {
            //set up preliminary data NOT NEEDED
            //int validID = getNewID("requirements", "rid");  //retrives a valid id to use

            //update the userrequrementlinks table NEEDS DOING**********************************************


            
            //Console.WriteLine(validID);

            MySqlDataAdapter adap = new MySqlDataAdapter();
            MySqlCommand command = conSQL.CreateCommand();

            string newTitle_st = newTitleText.Text;
            string newReqDesc_st = newDescText.Text;
            string newPriority_st = newPriorityCombo.Text;
            string newTimeOpen_st = DateTime.Now.ToString("dd/MM/yyyy HH:MM:ss"); //or DateTime.Now.ToString("dd/MM/yyyy h:MM tt")
            string newStatus_st = "Open"; //Open, In Progress, Closed

            //set command to add requirement
            //command.CommandText = "INSERT INTO requirements VALUES(null, '" + newTitle_st + "', '" + newReqDesc_st + "', '" + newPriority_st + "', '" +
                //newTimeOpen_st + "', null, '" + newStatus_st + "', null);"; 

            //execute the command
           // command.ExecuteNonQuery();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            conSQL.Dispose();
            adap.Dispose();
            this.Close();
        }

        private void releaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newVersion = release_dt.Rows[this.releaseComboBox.SelectedIndex][0].ToString(); //THIS WILL CHANGE TO STRING? ****
            releasesListBox.Items.Add(newVersion.ToString() + ", ");

        }

        private void ownersAddButton_Click(object sender, EventArgs e)
        {
            this.ownersListBox.Items.Add(owner_dt.Rows[ownersComboBox.SelectedIndex][0].ToString());    //Sets up the owners combo box
            //remove users from the table so they are no longer shown in the combo box
            owner_dt.Rows[ownersComboBox.SelectedIndex].Delete();
            owner_dt.AcceptChanges();
        }

        private void watchersAddButton_Click(object sender, EventArgs e)
        {
            this.watchersListBox.Items.Add(owner_dt.Rows[watchersComboBox.SelectedIndex][0].ToString());    //Sets up the watchers combo box
            //remove users from the table so they are no longer shown in the combo box
            watcher_dt.Rows[watchersComboBox.SelectedIndex].Delete();
            watcher_dt.AcceptChanges();
        }
      

        
    }
}
