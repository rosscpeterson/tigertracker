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
    //To pass data from window to window pass the form as a parameter to the construtor and then
    //make a changeText function or something in the window thats reading.
    public partial class TopLevel : Form
    {
        // Connection variable.
        protected MySqlConnection conSQL;
        protected MySqlCommand cmdSQL;
        protected MySqlDataAdapter adpSQL;
        // User/Project variables.
        protected string connectionString;
        protected string currentUser;
        protected int currentUserID;
        protected int currentProjectID;
        protected int currentUserPermLvl;
        // Keep data tables.
        protected DataTable proj_dt, ver_dt, req_dt, bug_dt;
        protected DataView req_vw;

        public TopLevel()
        {
            InitializeComponent();
            // Default value for variables.
            conSQL = new MySqlConnection();
            cmdSQL = new MySqlCommand("", conSQL);
            adpSQL = new MySqlDataAdapter();
            adpSQL.SelectCommand = cmdSQL;
            // Log user, get id and level.
            LogUser();
            UpdateUserDisplay();
            // Sets the project combo box and updates the req and bug table.
            setProjComboBox();  
        }
        // Always close the connection.
        ~TopLevel()
        {
            adpSQL.Dispose();
            cmdSQL.Dispose();
            conSQL.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {}

        private void LogUser()
        {
            // Hide this window.
            this.Hide();
            // Get log in.
            LogIn logIn = new LogIn(conSQL);
            // Show as dialog box.
            logIn.ShowDialog();
            // Stop if not a valid user.
            if ((currentUserID = logIn.UserID()) < 0)
            {
                System.Environment.Exit(0);
            }
            // Get rest of information.
            this.currentUser = logIn.UserName();
            this.currentUserPermLvl = logIn.UserLevel();
            // Log in sucessful.
            logIn.Close();
            this.Show();
        }
        // Updates user information changed by other forms (note that id does not change).
        private void UpdateUserInfo()
        {
            try
            {
                cmdSQL.CommandText = "SELECT username, permissionLevel FROM users WHERE uid = " + currentUserID.ToString() + ";";
                DataTable dtUserInfo = new DataTable();
                adpSQL.Fill(dtUserInfo);
                this.currentUser = (String)dtUserInfo.Rows[0][0];
                this.currentUserPermLvl = Convert.ToInt32(dtUserInfo.Rows[0][1]);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        // Grey out certain components according to user level.
        private void UpdateUserDisplay()
        {
            this.toolUsersButton.Visible = this.toolStripSeparator2.Visible = (this.currentUserPermLvl == 0);
            this.labelUserName.ForeColor = (this.currentUserPermLvl == 0) ? Color.Red : Color.Black;
            this.labelUserName.Text = this.currentUser;
        }

        private void setProjComboBox()
        {
            currentProjectID = -1;
            this.toolProjCombo.ComboBox.DataSource = null;
            //Set Curr Projects combo box
            proj_dt = new DataTable();
            cmdSQL.CommandText = "SELECT projects.pid, projects.name, userprojectlinks.permissionLevel FROM projects, userprojectlinks " +
                                 "WHERE projects.pid = userprojectlinks.projectid AND userprojectlinks.userid = " + currentUserID.ToString() + ";";
            adpSQL.Fill(proj_dt);

            if (proj_dt.Rows.Count != 0)
            {
                //if there are valid projects go ahead and update everything
                currentProjectID = (int)proj_dt.Rows[0][0];
                this.toolProjCombo.ComboBox.DataSource = proj_dt.DefaultView;   
                this.toolProjCombo.ComboBox.DisplayMember = "name";
                updateReqTable();
                updateBugTable();
            }

            // Bind the combo box to the table
            this.UpdateProjectDisplay();
            this.setVersionsComboBox();   
        }
        // Display project related things.
        private void UpdateProjectDisplay()
        {
            if (currentProjectID < 0)
            {
                this.labelUserTitle.Text = "[User Title]";
                return;
            }
            switch (Convert.ToInt32(proj_dt.Rows[0][2]))
            {
                case 0:
                    this.labelUserTitle.Text = "Project Manager";
                    break;
                case 10:
                    this.labelUserTitle.Text = "Project Developer";
                    break;
                default:
                    this.labelUserTitle.Text = "End User";
                    break;
            }
        }

        private void setVersionsComboBox()
        {
            ver_dt = new DataTable();

            //establish the requirements grid
            cmdSQL.CommandText = "SELECT * FROM versions WHERE projectid = " + currentProjectID + " ORDER BY vid DESC;";
            adpSQL.Fill(ver_dt);
        }

        private void updateReqTable()
        {
            reqTable.DataSource = null;

            if (currentProjectID < 0)
                return;

            //establish the requirements grid
            
            cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                 " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                 " FROM requirements, versions WHERE versions.projectid = " + currentProjectID.ToString() +
                                 " ORDER BY rid DESC;";
            req_dt = new DataTable();
            adpSQL.Fill(req_dt);

            // Create data view.
            req_vw = new DataView(req_dt);
            
            reqTable.DataSource = req_dt.DefaultView;
        }

        private void updateBugTable()
        {
            bugTable.DataSource = null;

            if (currentProjectID < 0)
                return;

            //establish the bugs grid view
            // Get every bug current user is linked to.
            /*cmdSQL.CommandText = "SELECT * FROM bugs, userprojectlinks, userbuglinks WHERE userbuglinks.userid = " + currentProjectID.ToString() + " AND bugs.bid = userbuglinks.bugid AND" +
                                    " userprojectlinks.userid = userbuglinks.userid AND userprojectlinks.projectid = " + currentProjectID.ToString() + ";";*/
            // Get every bug in project.
            cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currentProjectID.ToString() +
                                 " ORDER BY bid DESC;";
            bug_dt = new DataTable();
            adpSQL.Fill(bug_dt);

            bugTable.DataSource = bug_dt;
        }

        private void reqTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("Row #{0}, Column #{0} Clicked  " + e.RowIndex + "  " + e.ColumnIndex);

            int row = e.RowIndex;

            //set values found in the requirements table
            idText.Text = req_dt.Rows[row][0].ToString(); //get id
            titleText.Text = req_dt.Rows[row][1].ToString();  //get title
            descriptionText.Text = req_dt.Rows[row][2].ToString(); //get Description
            priorityComboBox.SelectedIndex = Convert.ToInt32(req_dt.Rows[row][3]) - 1;  //changes the priority combo box
            timeOpenText.Text = req_dt.Rows[row][4].ToString(); //get time created
            timeClosedText.Text = req_dt.Rows[row][5].ToString(); //get time satisfied
            
            //get status
            switch (req_dt.Rows[row][6].ToString())
            {
                case("Open"):
                    statusComboBox.SelectedIndex = 0;
                    break;
                case("In Progress"):
                    statusComboBox.SelectedIndex = 1;
                    break;
                case("Closed"):
                    statusComboBox.SelectedIndex = 2;
                    break;
            }

            //get the origionator

        }

        private void newReqButton_Click(object sender, EventArgs e)
        {
            NewReq newReqWindow = new NewReq(conSQL, currentUser, currentUserID, currentProjectID, ver_dt, req_dt);
            newReqWindow.ShowDialog();
        }

        private void newBugButton_Click(object sender, EventArgs e)
        {
            NewBug newBugWindow = new NewBug(conSQL, currentUser, currentUserID, currentProjectID, ver_dt, bug_dt);
            newBugWindow.ShowDialog();
        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolProjCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentProjectID = (Int32)proj_dt.Rows[this.toolProjCombo.ComboBox.SelectedIndex][0];   //gives the id selected
            updateReqTable();
            updateBugTable();
        }

        private void projects_toolstrip_Click(object sender, EventArgs e)
        {
            Projects projWindow = new Projects(conSQL, currentUserID, currentUserPermLvl);
            projWindow.ShowDialog();
            setProjComboBox();
        }

        private void toolUsersButton_Click(object sender, EventArgs e)
        {
            Users usersWindow = new Users(conSQL, currentUserID);
            usersWindow.ShowDialog();
            UpdateUserInfo();
            UpdateUserDisplay();
        }

        private void toolStripButtonLogOff_Click(object sender, EventArgs e)
        {
            conSQL.Close();
            LogUser();
            UpdateUserDisplay();
            setProjComboBox();
        }

        private void addVerButton_Click(object sender, EventArgs e)
        {
            this.releaseListBox.Items.Add(ver_dt.Rows[releaseListBox.SelectedIndex][0].ToString());    //Sets up the watchers combo box
            //remove users from the table so they are no longer shown in the combo box
            ver_dt.Rows[releaseListBox.SelectedIndex].Delete();
            ver_dt.AcceptChanges();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            //sample query from users line 169
            //m_cmdSQL.CommandText = "UPDATE users SET username = '" + name + "', password = '" + pass + "', permissionLevel = '" +
              //                         newLevel + "', active = " + activity + ", email = '" + email + "' WHERE uid = " + m_userID;
        }
    }
}

