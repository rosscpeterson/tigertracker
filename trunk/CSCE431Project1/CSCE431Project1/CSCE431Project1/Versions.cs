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
    public partial class Versions : Form
    {
        // Connection variable.
        MySqlConnection m_conSQL;
        MySqlCommand m_cmdSQL;
        MySqlDataAdapter m_adpSQL;
        // Project id.
        Int32 m_projID;
        // Data table of versions.
        DataTable m_dtVersions;

        public Versions(MySqlConnection _conSQL, Int32 _projID)
        {
            InitializeComponent();
            m_conSQL = _conSQL;
            m_cmdSQL = new MySqlCommand("", m_conSQL);
            m_adpSQL = new MySqlDataAdapter();
            m_adpSQL.SelectCommand = m_cmdSQL;
            m_projID = _projID;
            GetVersions();
        }
        ~Versions()
        {
            // Dispose.
            m_adpSQL.Dispose();
            m_cmdSQL.Dispose();
        }

        private void Versions_Load(object sender, EventArgs e)
        {

        }

        private void GetVersions()
        {
            try
            {
                m_dtVersions = new DataTable();
                // Get project versions.
                m_cmdSQL.CommandText = "SELECT * FROM versions WHERE projectid = " + m_projID.ToString() + " ORDER BY vid DESC;";
                m_adpSQL.Fill(m_dtVersions);
                // Display.
                this.comboBoxVersions.DataSource = m_dtVersions.DefaultView;
                this.comboBoxVersions.DisplayMember = "version";
                this.comboBoxVersions.SelectedIndex = 0;
                this.DisplayVersion();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Build Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }
        private void DisplayVersion()
        {
            this.richTextBoxProjDesc.Text = (String)m_dtVersions.Rows[this.comboBoxVersions.SelectedIndex].ItemArray[3];
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                m_cmdSQL.CommandText = "UPDATE versions SET verisonInfo = '" + this.richTextBoxProjDesc.Text + "' WHERE versions.vid = " + m_dtVersions.Rows[this.comboBoxVersions.SelectedIndex][0].ToString() + ";";
                m_cmdSQL.ExecuteNonQuery();
                m_dtVersions.Rows[this.comboBoxVersions.SelectedIndex][3] = this.richTextBoxProjDesc.Text;
                m_dtVersions.AcceptChanges();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Update Version Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (this.textBoxNew.Text == "")
            {
                MessageBox.Show("Please insert a version name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            m_cmdSQL.CommandText = "INSERT INTO versions VALUES(null, '" + m_projID + "', '" + this.textBoxNew.Text + "', '" + this.richTextBoxProjDesc.Text + "');";
            m_cmdSQL.ExecuteNonQuery();


            DataTable newTable = new DataTable();
            m_adpSQL.SelectCommand.CommandText = "SELECT * FROM versions WHERE vid = LAST_INSERT_ID();";
            m_adpSQL.Fill(newTable);
            // Update our data table of users.
            DataRow newRow = m_dtVersions.NewRow();
            for (int i = 0; i < newRow.ItemArray.GetLength(0); ++i)
                newRow[i] = newTable.Rows[0][i];
            m_dtVersions.Rows.InsertAt(newRow, 0);
            m_dtVersions.AcceptChanges();
            // Update index.
            this.comboBoxVersions.SelectedIndex = 0;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (m_dtVersions.Rows.Count == 1)
            {
                MessageBox.Show("A Project Must Have At Least One Version", "Logic Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // Delete version.
            m_cmdSQL.CommandText = "DELETE FROM versions WHERE vid = " + m_dtVersions.Rows[this.comboBoxVersions.SelectedIndex][0].ToString();
            m_cmdSQL.ExecuteNonQuery();
            m_dtVersions.Rows[this.comboBoxVersions.SelectedIndex].Delete();
            m_dtVersions.AcceptChanges();
        }

        private void comboBoxVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayVersion();
        }
    }
}
