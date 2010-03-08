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
    public partial class Projects : Form
    {
        // Connection variable.
        MySqlConnection m_conSQL;
        // Current project id, and who called.
        Int32 m_projID, m_callerID;
        // Data table for actual and potential project members, and projects.
        DataTable m_In, m_Out, m_projects;

        public Projects(MySqlConnection _conSQL, Int32 _callerID)
        {
            InitializeComponent();
            m_conSQL   = _conSQL;
            m_callerID = _callerID;
            GetProjects();
            PopulateComponents();
        }

        private void Projects_Load(object sender, EventArgs e)
        {

        }

        private void GetProjects()
        {
            MySqlCommand cmdSQL = new MySqlCommand("", m_conSQL);
            MySqlDataAdapter adpSQL = new MySqlDataAdapter();
            adpSQL.SelectCommand = cmdSQL;
            // Grey out non-administrative priveleges.
            cmdSQL.CommandText = "SELECT permissionLevel FROM users WHERE uid = " + m_callerID;//.ToString();
            DataTable dt = new DataTable();
            adpSQL.Fill(dt);

            Console.WriteLine("Trouble Output: " + dt.Rows[0][0].ToString());

            Int32 pL = Convert.ToInt32(dt.Rows[0][0].ToString());
            this.buttonNewProject.Enabled = (0 == pL);
            this.buttonDelete.Enabled = (0 == pL);
            this.textBoxNew.Enabled = (0 == pL);

            // Get all projects for superuser or projects which he is a manger for.
            if (pL > 0)
                cmdSQL.CommandText = "SELECT * FROM projects, userprojectlinks " +
                                               "WHERE userprojectlinks.userid = " + m_callerID + " AND userprojectlinks.projectid = projects.pid AND userprojectlinks.permissionLevel = 0;";
            else
                cmdSQL.CommandText = "SELECT * FROM projects";

            m_projID = -1;

            DataSet ds = new DataSet();
            adpSQL.Fill(ds, "First Table");
            m_projects = ds.Tables[0];
            this.comboBoxProject.DataSource = m_projects.DefaultView;
            this.comboBoxProject.DisplayMember = "name";
            if (m_projects.Rows.Count < 1)
                return;
            m_projID = (Int32)m_projects.Rows[0].ItemArray[0];

            adpSQL.Dispose();
            cmdSQL.Dispose();

            // Set comboboxes.
            
        }

        private void PopulateComponents()
        {
            if (m_projID < 0)
            {
                //what are we setting to null here
                this.comboBoxRemoveUser.DataSource = null;
                return;
            }

            DataRow currProj = m_projects.Rows[this.comboBoxProject.SelectedIndex];
            this.textBoxProjectName.Text = (String)currProj.ItemArray[1];

            DataSet ds = new DataSet();
            MySqlDataAdapter adpSQL = new MySqlDataAdapter();
            MySqlCommand cmdSQL = new MySqlCommand("", m_conSQL);
            adpSQL.SelectCommand = cmdSQL;

            // First get the ones in the project.
            adpSQL.SelectCommand.CommandText = "SELECT users.uid, users.username, userprojectlinks.permissionLevel FROM users, userprojectlinks " +
                                                "WHERE users.uid = userprojectlinks.userid " +
                                                "AND userprojectlinks.projectid = " + m_projID.ToString();
            adpSQL.Fill(ds, "First Table");
            // Get ones not in project.
            adpSQL.SelectCommand.CommandText = "SELECT users.uid, users.username FROM users, userprojectlinks " +
                                                "WHERE users.uid = userprojectlinks.userid " +
                                                "AND userprojectlinks.projectid = " + m_projID.ToString();
            adpSQL.Fill(ds, "Second Table");
            adpSQL.SelectCommand.CommandText = "SELECT users.uid, users.username FROM users;";
            adpSQL.Fill(ds, "Third Table");

            adpSQL.Dispose();
            cmdSQL.Dispose();
            
            m_In = ds.Tables[0];
            m_Out = Difference(ds.Tables[2], ds.Tables[1]);

            // Set comboboxes.
            this.comboBoxRemoveUser.DataSource = null;
            this.comboBoxRemoveUser.DataSource = m_In.DefaultView;
            this.comboBoxRemoveUser.DisplayMember = "username";
            this.comboBoxAddUser.DataSource = null;
            this.comboBoxAddUser.DataSource = m_Out.DefaultView;
            this.comboBoxAddUser.DisplayMember = "username";

            this.labelTitle.Text = "[User Title]";
            this.comboBoxTitle.SelectedIndex = 2;
            if (m_In.Rows.Count > 0)
            {
                switch ((Int32)m_In.Rows[0][2])
                {
                    case 0: this.labelTitle.Text = "Project Manager";   break;
                    case 1: this.labelTitle.Text = "Project Developer"; break;
                    case 2: this.labelTitle.Text = "End User";          break;
                }
            }
        }

        public static DataTable Difference(DataTable First, DataTable Second)
        {
            //Create Empty Table
            DataTable table = new DataTable("Difference");

            //Must use a Dataset to make use of a DataRelation object
            using (DataSet ds = new DataSet())
            {
                //Add tables
                ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });
                //Get Columns for DataRelation
                DataColumn[] firstcolumns = new DataColumn[ds.Tables[0].Columns.Count];

                for (int i = 0; i < firstcolumns.Length; i++)
                    firstcolumns[i] = ds.Tables[0].Columns[i];

                DataColumn[] secondcolumns = new DataColumn[ds.Tables[1].Columns.Count];

                for (int i = 0; i < secondcolumns.Length; i++)
                    secondcolumns[i] = ds.Tables[1].Columns[i];

                //Create DataRelation
                DataRelation r = new DataRelation(string.Empty, firstcolumns, secondcolumns, false);
                ds.Relations.Add(r);

                //Create columns for return table
                for (int i = 0; i < First.Columns.Count; i++)
                    table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);

                //If First Row not in Second, Add to return table.
                table.BeginLoadData();

                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r);

                    if (childrows == null || childrows.Length == 0)
                        table.LoadDataRow(parentrow.ItemArray, true);
                }

                table.EndLoadData();

            }

            return table;
        }

        private void comboBoxProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxProject.SelectedIndex < 0)
                m_projID = -1;
            else
            {
                // Get new project ID and repopulate. ERROR ******
                m_projID = (Int32)m_projects.Rows[this.comboBoxProject.SelectedIndex].ItemArray[0];
            }
            
            PopulateComponents();
            
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            // Remove from project.
            Int32 ID = (Int32)m_In.Rows[this.comboBoxRemoveUser.SelectedIndex].ItemArray[0];
            MySqlCommand cmdSQL = new MySqlCommand("DELETE FROM userprojectlinks WHERE projectid = " + m_projID + " AND userid = " + ID, m_conSQL);
            cmdSQL.ExecuteNonQuery();
            DataRow newRow = m_Out.NewRow();
            newRow[0] = m_In.Rows[this.comboBoxRemoveUser.SelectedIndex].ItemArray[0];
            newRow[1] = m_In.Rows[this.comboBoxRemoveUser.SelectedIndex].ItemArray[1];
            m_Out.Rows.Add(newRow);
            m_In.Rows[this.comboBoxRemoveUser.SelectedIndex].Delete();
            m_In.AcceptChanges();

            this.comboBoxRemoveUser_SelectedIndexChanged(sender, e);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Insert into project.
            Int32 ID = (Int32)m_Out.Rows[this.comboBoxAddUser.SelectedIndex].ItemArray[0];
            MySqlCommand cmdSQL = new MySqlCommand("INSERT INTO userprojectlinks VALUES(null, " + m_projID + ", " + ID + ", " + this.comboBoxTitle.SelectedIndex .ToString() + ");", m_conSQL);
            cmdSQL.ExecuteNonQuery();
            DataRow newRow = m_In.NewRow();
            newRow[0] = m_Out.Rows[this.comboBoxAddUser.SelectedIndex].ItemArray[0];
            newRow[1] = m_Out.Rows[this.comboBoxAddUser.SelectedIndex].ItemArray[1];
            newRow[2] = this.comboBoxTitle.SelectedIndex;
            m_In.Rows.Add(newRow);
            m_Out.Rows[this.comboBoxAddUser.SelectedIndex].Delete();
            m_Out.AcceptChanges();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Delete project and links.
            MySqlCommand cmdSQL = new MySqlCommand(/*"DELETE FROM userprojectlinks WHERE projectid = " + m_projID + ";" +*/
                                                   "DELETE FROM projects WHERE pid = " + m_projID, m_conSQL);
            cmdSQL.ExecuteNonQuery();
            m_projects.Rows[this.comboBoxProject.SelectedIndex].Delete();
            m_projects.AcceptChanges();
            // Get new project ID and repopulate.
            //m_projID = (Int32)m_projects.Rows[this.comboBoxProject.SelectedIndex].ItemArray[0];
            PopulateComponents();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            MySqlCommand cmdSQL = new MySqlCommand("UPDATE projects SET name = '" + this.textBoxProjectName.Text +
                                                   "' WHERE pid = " + m_projID, m_conSQL);
            cmdSQL.ExecuteNonQuery();
            m_projects.Rows[this.comboBoxProject.SelectedIndex][1] = this.textBoxProjectName.Text;
            m_projects.AcceptChanges();
        }

        private void buttonNewProject_Click(object sender, EventArgs e)
        {
            if (this.textBoxNew.Text == "")
            {
                MessageBox.Show("Must Specify a Project Name", "Logic Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //executes if proper information is present
                MySqlCommand cmdSQL = new MySqlCommand("INSERT INTO projects VALUES(null, '" + this.textBoxNew.Text +
                                                       "');", m_conSQL);
                cmdSQL.ExecuteNonQuery();

                MySqlDataAdapter adpSQL = new MySqlDataAdapter();
                DataTable newTable = new DataTable();
                adpSQL.SelectCommand = cmdSQL;

                adpSQL.SelectCommand.CommandText = "SELECT * FROM projects WHERE pid = LAST_INSERT_ID();";
                adpSQL.Fill(newTable);

                DataRow newRow = m_projects.NewRow();
                for (int i = 0; i < newRow.ItemArray.GetLength(0); ++i)
                    newRow[i] = newTable.Rows[0][i];
                m_projects.Rows.Add(newRow);
                m_projects.AcceptChanges();

                // Get new project ID and repopulate.
                this.comboBoxProject.SelectedIndex = m_projects.Rows.Count - 1;
                m_projID = (Int32)m_projects.Rows[this.comboBoxProject.SelectedIndex].ItemArray[0];
                PopulateComponents();
            }
        }

        private void comboBoxRemoveUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 index = this.comboBoxRemoveUser.SelectedIndex;
            if (index >= 0)
            {
                switch ((Int32)m_In.Rows[index][2])
                {
                    case 0: this.labelTitle.Text = "Project Manager";   break;
                    case 1: this.labelTitle.Text = "Project Developer"; break;
                    case 2: this.labelTitle.Text = "End User";          break;
                }
            }
        }
    }
}
