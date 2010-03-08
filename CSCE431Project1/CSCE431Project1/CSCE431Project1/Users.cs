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

namespace CSCE431Project1
{
    public partial class Users : Form
    {
        // Connection variable.
        MySqlConnection m_conSQL;
        // Current user id, as well as user who called.
        Int32 m_userID, m_callerID;
        // Data table for users, combo box.
        DataTable m_users;

        public Users(MySqlConnection _conSQL, Int32 _callerID) //callerID is the current user using the program
        {
            InitializeComponent();
            m_conSQL = _conSQL;
            m_callerID = _callerID;
            // Get users in the system.
            GetUsers();
            // Populate rest of componenets.
            PopulateComponents();
        }

        private void Users_Load(object sender, EventArgs e)
        {

        }

        // Get all users in system.
        private void GetUsers()
        {
            try
            {
                // Default to invalid.
                m_userID = -1;
                // Build empty data table.
                DataTable dt = new DataTable();
                // Build a data adapter which fills table.
                MySqlDataAdapter adpSQL = new MySqlDataAdapter();
                // Build a SQL command object.
                MySqlCommand cmdSQL = new MySqlCommand("", m_conSQL);
                // Tell adapter about which command to perform.
                adpSQL.SelectCommand = cmdSQL;

                // Get all projects.
                adpSQL.SelectCommand.CommandText = "SELECT * FROM users;";
                // Tell adapter what to fill.
                adpSQL.Fill(dt);

                // Keep table.
                m_users = dt;
                if (m_users.Rows.Count < 1)
                    return;
                // First entry in table is ID.
                m_userID = (Int32)m_users.Rows[0].ItemArray[0];

                adpSQL.Dispose();
                cmdSQL.Dispose();

                // Set user combobox.
                this.comboBoxUsers.DataSource = m_users.DefaultView;
                this.comboBoxUsers.DisplayMember = "username";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PopulateComponents()
        {
            if (m_userID < 0)
                return;

            // Simply populate with correct row of table.
            DataRow currUser = m_users.Rows[this.comboBoxUsers.SelectedIndex];
            this.textBoxName.Text  = (String)currUser.ItemArray[1];
            this.textBoxPass.Text  = (String)currUser.ItemArray[2];
            this.textBoxEmail.Text = (String)currUser.ItemArray[5];
            this.checkBoxActive.Checked = (Int32)currUser.ItemArray[4] != 0;    //active
            this.comboBoxLevel.SelectedIndex = Convert.ToInt32(currUser.ItemArray[3]);    //permission level
        }

        private void comboBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get new project ID and repopulate.
            m_userID = (Int32)m_users.Rows[this.comboBoxUsers.SelectedIndex].ItemArray[0];
            PopulateComponents();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (m_userID == m_callerID)
            {
                MessageBox.Show("Superuser cannot delete himself", "Logic Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                // Delete user and links.
                MySqlCommand cmdSQL = new MySqlCommand(/*"DELETE FROM userprojectlinks WHERE userid = " + m_userID + ";" +*/
                                                       "DELETE FROM users WHERE uid = " + m_userID, m_conSQL);
                cmdSQL.ExecuteNonQuery();
                m_users.Rows[this.comboBoxUsers.SelectedIndex].Delete();
                m_users.AcceptChanges();
                // Get new project ID and repopulate.
                m_userID = (Int32)m_users.Rows[this.comboBoxUsers.SelectedIndex].ItemArray[0];
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            PopulateComponents();
        }

        private void buttonNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                //MAKE SURE YOU CANT ADD NEW USER******************

                // Create new user.
                MySqlCommand cmdSQL = new MySqlCommand("INSERT INTO users VALUES(null, '" + this.textBoxNewName.Text +
                                                       "', '', 1, 1, '');", m_conSQL);
                cmdSQL.ExecuteNonQuery();

                MySqlDataAdapter adpSQL = new MySqlDataAdapter();
                DataTable newTable = new DataTable();
                adpSQL.SelectCommand = cmdSQL;

                adpSQL.SelectCommand.CommandText = "SELECT * FROM users WHERE uid = LAST_INSERT_ID();";
                adpSQL.Fill(newTable);

                // Update our data table of users.
                DataRow newRow = m_users.NewRow();
                for (int i = 0; i < newRow.ItemArray.GetLength(0); ++i)
                    newRow[i] = newTable.Rows[0][i];
                m_users.Rows.Add(newRow);
                m_users.AcceptChanges();

                // Get new project ID and repopulate.
                this.comboBoxUsers.SelectedIndex = m_users.Rows.Count - 1;
                m_userID = (Int32)m_users.Rows[this.comboBoxUsers.SelectedIndex].ItemArray[0];
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            PopulateComponents();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxName.Text;
                string pass = textBoxPass.Text;
                string email = textBoxEmail.Text;
                int newLevel = this.comboBoxLevel.SelectedIndex;
                int activity = (this.checkBoxActive.Checked) ? 1 : 0;
                MySqlCommand cmdSQL = new MySqlCommand("UPDATE users SET username = '" + name +
                                                       "', password = '" + pass + "', email = '" + email + "', permissionLevel = " +
                                                       newLevel + ", active = " + activity +
                                                       " WHERE uid = " + m_userID, m_conSQL);
                cmdSQL.ExecuteNonQuery();
                m_users.Rows[this.comboBoxUsers.SelectedIndex][1] = name;
                m_users.Rows[this.comboBoxUsers.SelectedIndex][2] = pass;
                m_users.Rows[this.comboBoxUsers.SelectedIndex][3] = email;
                m_users.Rows[this.comboBoxUsers.SelectedIndex][4] = newLevel;
                m_users.Rows[this.comboBoxUsers.SelectedIndex][5] = activity;
                m_users.AcceptChanges();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
