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
        MySqlCommand m_cmdSQL;
        MySqlDataAdapter m_adpSQL;
        // Current user id, as well as user who called.
        Int32 m_userID, m_callerID;
        // Data table for users, combo box.
        DataTable m_users;

        public Users(MySqlConnection _conSQL, Int32 _callerID) //callerID is the current user using the program
        {
            InitializeComponent();
            m_conSQL = _conSQL;
            m_cmdSQL = new MySqlCommand("", m_conSQL);
            m_adpSQL = new MySqlDataAdapter();
            m_adpSQL.SelectCommand = m_cmdSQL;
            m_callerID = _callerID;
            // Get users in the system.
            GetUsers();
            // Populate rest of componenets.
            PopulateComponents();
        }
        ~Users()
        {
            // Dispose.
            m_adpSQL.Dispose();
            m_cmdSQL.Dispose();
        }

        private void Users_Load(object sender, EventArgs e)
        {}

        // Get all users in system.
        private void GetUsers()
        {
            try
            {
                // Default to invalid.
                m_userID = -1;
                // Build empty data table.
                DataTable dt = new DataTable();
                // Get all projects.
                m_adpSQL.SelectCommand.CommandText = "SELECT * FROM users;";
                // Tell adapter what to fill.
                m_adpSQL.Fill(dt);
                // Keep table.
                m_users = dt;
                if (m_users.Rows.Count < 1)
                    return;
                // First entry in table is ID.
                m_userID = (Int32)m_users.Rows[0].ItemArray[0];
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
            // There is no user selected.
            if (m_userID < 0)
                return;
            // Simply populate with correct row of table.
            DataRow currUser = m_users.Rows[this.comboBoxUsers.SelectedIndex];
            this.textBoxName.Text            = (String)currUser.ItemArray[1];
            this.textBoxPass.Text            = (String)currUser.ItemArray[2];
            this.comboBoxLevel.SelectedIndex = Convert.ToInt32(currUser.ItemArray[3]);    
            this.checkBoxActive.Checked      = (Int32)currUser.ItemArray[4] != 0;   
            this.textBoxEmail.Text           = (String)currUser.ItemArray[5];
        }

        private void comboBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get new user ID and repopulate.
            m_userID = (Int32)m_users.Rows[this.comboBoxUsers.SelectedIndex].ItemArray[0];
            PopulateComponents();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            // Take care of self-deletion first.
            if (m_userID == m_callerID)
            {
                MessageBox.Show("Superuser cannot delete himself", "Logic Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // Delete in remote database and local variable.
            try
            {
                // Delete user and links.
                m_cmdSQL.CommandText = /*"DELETE FROM userprojectlinks WHERE userid = " + m_userID + ";" +*/
                                       "DELETE FROM users WHERE uid = " + m_userID;
                m_cmdSQL.ExecuteNonQuery();
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
            // Do not allow the creation of no user.
            if (this.textBoxNewName.Text == "")
            {
                MessageBox.Show("Must Specify a User Name", "Logic Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                // Create new user.
                m_cmdSQL.CommandText = "INSERT INTO users VALUES(null, '" + this.textBoxNewName.Text + "', '', 1, 1, '');";
                m_cmdSQL.ExecuteNonQuery();
                // Get last inserted user.
                DataTable newTable = new DataTable();
                m_adpSQL.SelectCommand.CommandText = "SELECT * FROM users WHERE uid = LAST_INSERT_ID();";
                m_adpSQL.Fill(newTable);
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
                String name     = this.textBoxName.Text;
                String pass     = this.textBoxPass.Text;
                String email    = this.textBoxEmail.Text;
                String newLevel = this.comboBoxLevel.SelectedIndex.ToString();
                Int32  activity = (this.checkBoxActive.Checked) ? 1 : 0;
                m_cmdSQL.CommandText = "UPDATE users SET username = '" + name + "', password = '" + pass + "', permissionLevel = '" +
                                       newLevel + "', active = " + activity + ", email = '" + email + "' WHERE uid = " + m_userID;
                m_cmdSQL.ExecuteNonQuery();
                m_users.Rows[this.comboBoxUsers.SelectedIndex][1] = name;
                m_users.Rows[this.comboBoxUsers.SelectedIndex][2] = pass;
                m_users.Rows[this.comboBoxUsers.SelectedIndex][3] = newLevel;
                m_users.Rows[this.comboBoxUsers.SelectedIndex][4] = activity;
                m_users.Rows[this.comboBoxUsers.SelectedIndex][5] = email;
                m_users.AcceptChanges();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
