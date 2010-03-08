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

        public NewReq(string connString, string currUser, int currUserID, int currProjID)
        {
            connectionString = connString;
            currentUser = currUser;
            int currentUserID = currUserID;
            int currentProjectID = currProjID;

            // Start connection.
            conSQL = new MySqlConnection(connectionString);
            conSQL.Open();

            InitializeComponent();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            MySqlCommand command = conSQL.CreateCommand();

            //establish the releases grid view
            command.CommandText = "SELECT versions.version FROM versions WHERE projectid = '" + currentProjectID + "';";

            adap.SelectCommand = command;
            DataSet release_dataset = new DataSet();
            adap.Fill(release_dataset, "release_data");

            releaseTable.DataSource = release_dataset;
            releaseTable.DataMember = "release_data";
            
        }

        private int getNewID(string tableName, string idField) {
            //returns a valid ID to be used for the table

            MySqlDataAdapter adap = new MySqlDataAdapter();
            MySqlCommand command = conSQL.CreateCommand();

            //formulate the text
            string newCommandText = "SELECT " + idField + " FROM " + tableName;
            command.CommandText = newCommandText;
            adap.SelectCommand = command;

            DataSet ids_ds = new DataSet();
            adap.Fill(ids_ds, "ids");

            DataTable ids_dt = ids_ds.Tables["ids"];

            //check to see if we are dealing with an empty table or not
            if (ids_dt.Rows.Count == 0)
            {
                return 0;
            }

            //cycle through the table to find a new id that can be used
            bool notFound = true;
            for (int i = 0; i < ids_dt.Rows.Count + 1; i++)
            {
                for (int j = 0; j < ids_dt.Rows.Count; j++)
                {
                    if (ids_dt.Rows[0][j].Equals(i))
                    {
                        notFound = false;
                        break;
                    }
                }
                if (notFound)
                    return i;
                else
                    notFound = true;
            }
            return -1;
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
            string newReqDesc_st = newReqDescText.Text;
            string newPriority_st = newReqPriorityCombo.Text;
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
            this.Close();
        }

        private void ownersCheckedList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void releaseTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        
    }
}
