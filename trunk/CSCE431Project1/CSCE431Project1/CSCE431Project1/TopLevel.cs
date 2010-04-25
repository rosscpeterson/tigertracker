using System;
using System.Collections.Generic;
using System.Collections;
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
        protected DataSet projUsrs_ds;
        protected int currReqOrBugID;   //this is the id of the req or bug selected in the data grid view, used for adding owners or watchers
        //protected DataTable user_dt, userreq_dt;
        protected String showing;
        protected int selectedID = 0;

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
                cmdSQL.CommandText = "SELECT * FROM users WHERE uid = " + currentUserID.ToString() + ";";
                DataTable myuser_dt = new DataTable();
                adpSQL.Fill(myuser_dt);
                this.currentUser = myuser_dt.Rows[0][1].ToString();
                this.currentUserPermLvl = Convert.ToInt32(myuser_dt.Rows[0][3]);
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
                // Bind the combo box to the table
                this.UpdateProjectDisplay();
                this.setVersionsComboBox();

                // Get users in this project with projUsrs_ds.
                projUsrs_ds = new DataSet();
                cmdSQL.CommandText = "SELECT users.* FROM users, userprojectlinks WHERE " + currentProjectID.ToString() + " = userprojectlinks.projectid" +
                                     " AND userprojectlinks.userid = " + currentUserID.ToString() + ";";
                adpSQL.SelectCommand = cmdSQL;
                adpSQL.Fill(projUsrs_ds, "All");
                //cmdSQL.CommandText = "SELECT userprojectlinks.* FROM userprojectlinks WHERE " + currentProjectID.ToString() + " = userprojectlinks.projectid;";
                //adpSQL.SelectCommand = cmdSQL;
                //adpSQL.Fill(projUsrs_ds, "Links");

                ownerComboBox.DataSource = projUsrs_ds.Tables["All"].DefaultView;
                ownerComboBox.DisplayMember = "username";

                watcherComboBox.DataSource = projUsrs_ds.Tables["All"].DefaultView;
                watcherComboBox.DisplayMember = "username";

                TabView.SelectedIndex = 0;
                TabView_SelectedIndexChanged(TabView, null);
                //reqTable_CellClick(reqTable, new DataGridViewCellEventArgs(0, 0));
            }
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
            ver_dt.PrimaryKey = new DataColumn[] { ver_dt.Columns["vid"] };
            /*
            DataTable verReqLnk = new DataTable();
            cmdSQL.CommandText = "SELECT requirementversionlinks.* FROM requirementversionlinks, versions WHERE versions.projectid = " + currentProjectID + " AND versions.vid = versionid;";
            adpSQL.Fill(verReqLnk);

            // Update requirement name with linking version.
            foreach (DataRow dr1 in this.req_dt.Rows)
            {
                DataRow[] drc = verReqLnk.Select("requirementid = " + dr1["ReqID"]);

                foreach (DataRow dr2 in drc)
                {
                    DataRow dr3 = ver_dt.Rows.Find(dr2["versionid"]);
                    if (dr3 == null)
                        continue;
                    dr1["Title"] = String.Format("{0} - {1}", dr1["Title"].ToString(), dr3["version"]);
                }
            }*/
        }

        private void updateReqTable()
        {
            reqTable.DataSource = null;

            if (currentProjectID < 0)
                return;

            //establish the requirements grid
            
            cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                 " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                 " FROM requirements, versions, requirementversionlinks WHERE requirementversionlinks.requirementid = requirements.rid AND " +
                                 "requirementversionlinks.versionid = versions.vid AND versions.projectid = " + currentProjectID.ToString() +
                                 " ORDER BY status DESC, priority DESC, rid DESC;";

            req_dt = new DataTable();
            adpSQL.Fill(req_dt);
            // Change names.
            req_dt.PrimaryKey = new DataColumn[]{req_dt.Columns["rid"]};
            req_dt.Columns["rid"].ColumnName                    = "ReqID";
            req_dt.Columns["requirementTitle"].ColumnName       = "Title";
            req_dt.Columns["requirementDescription"].ColumnName = "Description";
            req_dt.Columns["priority"].ColumnName               = "Priority";
            req_dt.Columns["timeCreated"].ColumnName            = "Time Created";
            req_dt.Columns["timeSatisfied"].ColumnName          = "Time Satisfied";
            req_dt.Columns["status"].ColumnName                 = "Status";
            req_dt.Columns["notes"].ColumnName                  = "Notes";
            req_dt.DefaultView.Sort = String.Format("Status DESC, Priority DESC, ReqID DESC");

            reqTable.DataSource = req_dt.DefaultView;
            // Blank out ones we don't want.
            reqTable.Columns["Description"].Visible = false;
            reqTable.Columns["Notes"].Visible       = false;
            // Make others better.
            // reqTable.AutoSizeColumnMode(index) for single column.
            reqTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // reqTable.AutoResizeColumn(index) for single column.
            reqTable.AutoResizeColumns();
            /*
           //update userrequirementlinks table
           cmdSQL.CommandText = "SELECT userrequirementlinks.* FROM userrequirementlinks, versions WHERE versions.projectid = " + currentProjectID.ToString() + ";";
           userreq_dt = new DataTable();
           adpSQL.Fill(userreq_dt);

           
           //updates user_dt
           cmdSQL.CommandText = "SELECT * FROM users;";
           user_dt = new DataTable();
           adpSQL.Fill(user_dt);*/
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
            cmdSQL.CommandText = "SELECT DISTINCT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, bugreqlinks, requirements, requirementversionlinks, bugversionlinks, versions WHERE bugreqlinks.bugid = bugs.bid AND " +
                                 "bugreqlinks.requirementid = requirements.rid AND requirementversionlinks.requirementid = requirements.rid AND " +
                                 "requirementversionlinks.versionid = versions.vid AND versions.projectid = " + currentProjectID.ToString() +
                                 " AND bugversionlinks.versionid = versions.vid AND bugversionlinks.bugid = bugs.bid " + 
                                 " ORDER BY status DESC, priority DESC, bid DESC;";
            bug_dt = new DataTable();
            adpSQL.Fill(bug_dt);
            // Change names.
            bug_dt.PrimaryKey = new DataColumn[] { bug_dt.Columns["bid"] };
            bug_dt.Columns["bid"].ColumnName            = "BugID";
            bug_dt.Columns["bugTitle"].ColumnName       = "Title";
            bug_dt.Columns["bugDescription"].ColumnName = "Description";
            bug_dt.Columns["priority"].ColumnName       = "Priority";
            bug_dt.Columns["timeOpen"].ColumnName       = "Time Open";
            bug_dt.Columns["timeClosed"].ColumnName     = "Time Closed";
            bug_dt.Columns["status"].ColumnName         = "Status";
            bug_dt.Columns["notes"].ColumnName          = "Notes";
            bug_dt.DefaultView.Sort = String.Format("Status DESC, Priority DESC, BugID DESC");


            bugTable.DataSource = bug_dt.DefaultView;
            // Blank out ones we don't want.
            bugTable.Columns["Description"].Visible = false;
            bugTable.Columns["Notes"].Visible       = false;
            // Make others better.
            // reqTable.AutoSizeColumnMode(index) for single column.
            bugTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // reqTable.AutoResizeColumn(index) for single column.
            bugTable.AutoResizeColumns();
        }

        private void TabView_SelectedIndexChanged(Object sender, EventArgs e)
        {
            switch (this.TabView.SelectedIndex)
            {
                case 0: 
                    this.label4.Text = "Release";
                    this.comboBoxVer.Visible = false;
                    if (req_dt.Rows.Count > 0)
                    {
                        reqTable_CellClick(reqTable, new DataGridViewCellEventArgs(0, 0));
                    }
                    break;
                case 1:
                    this.label4.Text = "Reqs";
                    this.comboBoxVer.Visible = true;
                    if (bug_dt.Rows.Count > 0)
                    {
                        bugTable_CellClick(bugTable, new DataGridViewCellEventArgs(0, 0));
                    }
                    break;
            }
        }

        private void reqTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 row = e.RowIndex;

            //check that its a valid row
            if (row >= req_dt.Rows.Count || row < 0)
            {
                return;
            }

            showing = "Req";
            //currReqOrBugID = *****
            if (row < 0)
                return;
            // Duplicate record thing... not needed now, I don't think.
            cmdSQL.CommandText = "DELETE T1 FROM userrequirementlinks AS T1, userrequirementlinks AS T2 WHERE" +
                                 " T1.userid = T2.userid AND T1.requirementid = T2.requirementid AND T1.urlid > T2.urlid AND" +
                                 " ( T1.role = T2.role OR (T1.role IN ('owner','originator') AND T2.role IN ('owner','originator')) );";
            cmdSQL.ExecuteNonQuery();

            //set values found in the requirements table
            idText.Text                     = req_dt.Rows[row][0].ToString(); //get id
            titleText.Text                  = req_dt.Rows[row][1].ToString();  //get title
            descriptionText.Text            = req_dt.Rows[row][2].ToString(); //get Description
            priorityComboBox.SelectedIndex  = Convert.ToInt32(req_dt.Rows[row][3]) - 1;  //changes the priority combo box
            timeOpenText.Text               = req_dt.Rows[row][4].ToString(); //get time created
            timeClosedText.Text             = req_dt.Rows[row][5].ToString(); //get time satisfied
            selectedID = Convert.ToInt32(idText.Text.ToString());
                notesText.Text = Notes.getNotes(conSQL, showing, selectedID);
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
            
            adpSQL.SelectCommand = cmdSQL;
            cmdSQL.CommandText = "SELECT users.uid, users.username, userrequirementlinks.role FROM users, userrequirementlinks " +
                                 "WHERE userrequirementlinks.requirementid = " + req_dt.Rows[row][0].ToString() + 
                                 " AND users.uid = userrequirementlinks.userid " + 
                                 "AND role = 'originator';";
            if (projUsrs_ds.Tables.Contains("Originator"))
                projUsrs_ds.Tables["Originator"].Clear();
            adpSQL.Fill(projUsrs_ds, "Originator");

            projUsrs_ds.Tables["Originator"].PrimaryKey = new DataColumn[] { projUsrs_ds.Tables["Originator"].Columns["uid"] };

            cmdSQL.CommandText = "SELECT users.uid, users.username, userrequirementlinks.role FROM users, userrequirementlinks " +
                                 "WHERE userrequirementlinks.requirementid = " + req_dt.Rows[row][0].ToString() +
                                 " AND users.uid = userrequirementlinks.userid " + 
                                 "AND (role = 'owner' OR role = 'originator');";
            if (projUsrs_ds.Tables.Contains("Owners"))
                projUsrs_ds.Tables["Owners"].Clear();
            adpSQL.Fill(projUsrs_ds, "Owners");

            projUsrs_ds.Tables["Owners"].PrimaryKey = new DataColumn[] { projUsrs_ds.Tables["Owners"].Columns["uid"] };

            cmdSQL.CommandText = "SELECT users.uid, users.username, userrequirementlinks.role FROM users, userrequirementlinks " +
                                 "WHERE userrequirementlinks.requirementid = " + req_dt.Rows[row][0].ToString() +
                                 " AND users.uid = userrequirementlinks.userid " + 
                                 "AND role = 'watcher';";
            if (projUsrs_ds.Tables.Contains("Watchers"))
                projUsrs_ds.Tables["Watchers"].Clear();
            adpSQL.Fill(projUsrs_ds, "Watchers");

            projUsrs_ds.Tables["Watchers"].PrimaryKey = new DataColumn[] { projUsrs_ds.Tables["Watchers"].Columns["uid"] };

            cmdSQL.CommandText = "SELECT versions.* FROM versions, requirementversionlinks " +
                                 "WHERE requirementversionlinks.requirementid = " + req_dt.Rows[row][0].ToString() +
                                 " AND requirementversionlinks.versionid = versions.vid " +
                                 " ORDER BY versions.vid DESC;";
            if (projUsrs_ds.Tables.Contains("Links"))
                projUsrs_ds.Tables["Links"].Clear();
            adpSQL.Fill(projUsrs_ds, "Links");

            projUsrs_ds.Tables["Links"].PrimaryKey = new DataColumn[] { projUsrs_ds.Tables["Links"].Columns["vid"] };

            if (projUsrs_ds.Tables["Originator"].Rows.Count > 0)
                originatorText.Text = projUsrs_ds.Tables["Originator"].Rows[0]["username"].ToString();
            //else
                //MessageBox.Show("No Originator Found", "SQL DB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            this.ownerListBox.DataSource = projUsrs_ds.Tables["Owners"].DefaultView;
            this.ownerListBox.DisplayMember = "username";
            this.ownerListBox.Refresh();

            this.watchersListBox.DataSource = projUsrs_ds.Tables["Watchers"].DefaultView;
            this.watchersListBox.DisplayMember = "username";
            this.watchersListBox.Refresh();

            this.comboBoxRR.DataSource = ver_dt.DefaultView;
            this.comboBoxRR.DisplayMember = "version";
            this.comboBoxRR.Refresh();

            this.releaseListBox.DataSource = projUsrs_ds.Tables["Links"].DefaultView;
            this.releaseListBox.DisplayMember = "version";
            this.releaseListBox.Refresh();
            /*
            //owner and watcher array's
            ArrayList ownerIDs = new ArrayList();
            ArrayList watcherIDs = new ArrayList();
            DataTable currOwners_dt = user_dt.Clone();
            DataTable currWatchers_dt = user_dt.Clone();

            //get the origionator, userreq_dt updated in update req table function
            String expression = "requirementid = " + req_dt.Rows[row][0].ToString();
            DataRow[] userreqlinks_dr;
            userreqlinks_dr = userreq_dt.Select(expression);

            int ownerID = -1;

            //here we are also going to construct the arrays of user id's for the owners and watchers list box
            for (int i = 0; i < userreqlinks_dr.Length; i++)
            {
                DataRow newRow;
                switch (userreqlinks_dr[i][3].ToString())
                {
                    case ("originator"):
                        ownerID = Convert.ToInt32(userreqlinks_dr[i][1]);
                        break;
                    case ("owner"):
                        ownerIDs.Add(Convert.ToInt32(userreqlinks_dr[i][1]));
                        //currOwners_dt.ImportRow(userreqlinks_dr[i]);
                        break;
                    case ("watcher"):
                        watcherIDs.Add(Convert.ToInt32(userreqlinks_dr[i][1]));
                        //currWatchers_dt.ImportRow(userreqlinks_dr[i]);
                        break;
                    default:
                        MessageBox.Show("No user associated with selected requirement", "SQL DB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }

            //we have owner id go get owners name from user_dt
            expression = "uid = " + ownerID.ToString();
            DataRow[] user_dr;
            user_dr = user_dt.Select(expression);

            if (user_dr.Length > 0)
            {
                originatorText.Text = user_dr[0][1].ToString();
            }
            else
            {
                MessageBox.Show("No Origionator Found", "SQL DB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //populate the owners and watchers list boxes
            //owners list box
            for (int i = 0; i < ownerIDs.Count; i++)
            {
                expression = "uid = " + ownerIDs[i].ToString();
                DataRow[] owners_dr;
                owners_dr = user_dt.Select(expression);

                currOwners_dt.ImportRow(owners_dr[0]);
            }

            this.ownerListBox.DataSource = currOwners_dt.DefaultView;
            this.ownerListBox.DisplayMember = "username";

            //Watchers List Box
            for (int i = 0; i < watcherIDs.Count; i++)
            {
                expression = "uid = " + watcherIDs[i].ToString();
                DataRow[] watchers_dr;
                watchers_dr = user_dt.Select(expression);

                currWatchers_dt.ImportRow(watchers_dr[0]);
            }

            this.watchersListBox.DataSource = currWatchers_dt.DefaultView;
            this.watchersListBox.DisplayMember = "username";*/
        }

        private void bugTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            showing = "Bug";
            Int32 row = e.RowIndex;
          
            if (row >= bug_dt.Rows.Count || row < 0)
            {
                return;
            }

            //set values found in the requirements table
            idText.Text                     = bug_dt.Rows[row]["BugID"].ToString(); //get id
            titleText.Text                  = bug_dt.Rows[row]["Title"].ToString();  //get title
            descriptionText.Text            = bug_dt.Rows[row]["Description"].ToString(); //get Description
            priorityComboBox.SelectedIndex  = Convert.ToInt32(bug_dt.Rows[row]["Priority"]) - 1;  //changes the priority combo box
            timeOpenText.Text               = bug_dt.Rows[row]["Time Open"].ToString(); //get time created
            timeClosedText.Text             = bug_dt.Rows[row]["Time Closed"].ToString(); //get time satisfied
            selectedID = Convert.ToInt32(idText.Text.ToString());
            notesText.Text = Notes.getNotes(conSQL, showing, selectedID);
            //get status
            switch (bug_dt.Rows[row]["Status"].ToString())
            {
                case ("Open"):
                    statusComboBox.SelectedIndex = 0;
                    break;
                case ("In Progress"):
                    statusComboBox.SelectedIndex = 1;
                    break;
                case ("Closed"):
                    statusComboBox.SelectedIndex = 2;
                    break;
            }

            adpSQL.SelectCommand = cmdSQL;
            cmdSQL.CommandText = "SELECT users.uid, users.username FROM users, userbuglinks " +
                                 "WHERE userbuglinks.bugid = " + bug_dt.Rows[row]["BugID"].ToString() +
                                 " AND users.uid = userbuglinks.userid " +
                                 "AND role = 'originator';";
            if (projUsrs_ds.Tables.Contains("Originator"))
                projUsrs_ds.Tables["Originator"].Clear();
            adpSQL.Fill(projUsrs_ds, "Originator");

            projUsrs_ds.Tables["Originator"].PrimaryKey = new DataColumn[] { projUsrs_ds.Tables["Originator"].Columns["uid"] };

            cmdSQL.CommandText = "SELECT users.uid, users.username FROM users, userbuglinks " +
                                 "WHERE userbuglinks.bugid = " + bug_dt.Rows[row]["BugID"].ToString() +
                                 " AND users.uid = userbuglinks.userid " +
                                 "AND (role = 'owner' OR role = 'originator');";
            if (projUsrs_ds.Tables.Contains("Owners"))
                projUsrs_ds.Tables["Owners"].Clear();
            adpSQL.Fill(projUsrs_ds, "Owners");

            projUsrs_ds.Tables["Owners"].PrimaryKey = new DataColumn[] { projUsrs_ds.Tables["Owners"].Columns["uid"] };

            cmdSQL.CommandText = "SELECT users.uid, users.username FROM users, userbuglinks " +
                                 "WHERE userbuglinks.bugid = " + bug_dt.Rows[row]["BugID"].ToString() +
                                 " AND users.uid = userbuglinks.userid " +
                                 "AND role = 'watcher';";
            if (projUsrs_ds.Tables.Contains("Watchers"))
                projUsrs_ds.Tables["Watchers"].Clear();
            adpSQL.Fill(projUsrs_ds, "Watchers");

            projUsrs_ds.Tables["Watchers"].PrimaryKey = new DataColumn[] { projUsrs_ds.Tables["Watchers"].Columns["uid"] };

            cmdSQL.CommandText = "SELECT requirements.requirementTitle, versions.version FROM versions, requirementversionlinks, requirements, bugreqlinks " +
                                 "WHERE bugreqlinks.requirementid = requirements.rid AND bugreqlinks.bugid = " + bug_dt.Rows[row]["BugID"].ToString() +
                                 " AND requirementversionlinks.requirementid = requirements.rid AND requirementversionlinks.versionid = versions.vid " +
                                 " ORDER BY requirements.rid DESC, versions.vid DESC;";
            if (projUsrs_ds.Tables.Contains("Links"))
                projUsrs_ds.Tables["Links"].Clear();
            projUsrs_ds.EnforceConstraints = false;
            adpSQL.Fill(projUsrs_ds, "Links");
            try { projUsrs_ds.EnforceConstraints = true; }
            catch (Exception err) {
                //if statement removes the err not used warning.
                if (err.Message == "" || true)
                {
                    DataRow[] drc = projUsrs_ds.Tables["Links"].GetErrors();
                }
            }
            if (projUsrs_ds.Tables["Links"].Columns.Contains("ReqVer") == false)
                projUsrs_ds.Tables["Links"].Columns.Add("ReqVer", typeof(String));

            foreach (DataRow dr in projUsrs_ds.Tables["Links"].Rows)
            {
                dr["ReqVer"] = String.Format("{0} - {1}", dr["requirementTitle"], dr["version"]);
            }

            projUsrs_ds.Tables["Links"].PrimaryKey = new DataColumn[] { projUsrs_ds.Tables["Links"].Columns["ReqVer"] };

            if (projUsrs_ds.Tables["Originator"].Rows.Count > 0)
                originatorText.Text = projUsrs_ds.Tables["Originator"].Rows[0]["username"].ToString();
            //else
                //MessageBox.Show("No Origionator Found", "SQL DB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            this.ownerListBox.DataSource = projUsrs_ds.Tables["Owners"].DefaultView;
            this.ownerListBox.DisplayMember = "username";
            this.ownerListBox.Refresh();

            this.watchersListBox.DataSource = projUsrs_ds.Tables["Watchers"].DefaultView;
            this.watchersListBox.DisplayMember  = "username";
            this.watchersListBox.Refresh();

            this.comboBoxRR.DataSource = req_dt.DefaultView;
            this.comboBoxRR.DisplayMember = "Title";
            this.comboBoxRR.Refresh();

            this.releaseListBox.DataSource = projUsrs_ds.Tables["Links"].DefaultView;
            this.releaseListBox.DisplayMember = "ReqVer";
            this.releaseListBox.Refresh();
        }

        private void newReqButton_Click(object sender, EventArgs e)
        {
            NewReq newReqWindow = new NewReq(conSQL, currentUser, currentUserID, currentProjectID, ver_dt, req_dt/*, userreq_dt*/);
            newReqWindow.ShowDialog();
            updateReqTable();
            TabView.SelectedIndex = 0;
            TabView_SelectedIndexChanged(TabView, null);
        }

        private void newBugButton_Click(object sender, EventArgs e)
        {
            NewBug newBugWindow = new NewBug(conSQL, currentUser, currentUserID, currentProjectID, ver_dt, bug_dt, req_dt);
            newBugWindow.ShowDialog();
            updateBugTable();
            TabView.SelectedIndex = 1;
            TabView_SelectedIndexChanged(TabView, null);
        }

        private void toolProjCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Got an error here when I logged out and tried to log in with different user****
            //got error here from adding ross to a project as a developer.  When I closed user boxes it gave problem ****
            if (this.toolProjCombo.ComboBox.SelectedIndex < 0)
                return;
            currentProjectID = (Int32)proj_dt.Rows[this.toolProjCombo.ComboBox.SelectedIndex][0];   //gives the id selected
            setVersionsComboBox();
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
            //trap to see if user is in pool
            if (this.comboBoxRR.SelectedIndex < 0 || (TabView.SelectedIndex == 1 && comboBoxVer.SelectedIndex < 0))
                return;
            
            DataRow newRow  = projUsrs_ds.Tables["Links"].NewRow();

            //are we on a req or a bug?
            if (TabView.SelectedIndex == 1)
            {
                String szReqVer = String.Format("{0} - {1}", this.comboBoxRR.Text, this.comboBoxVer.Text);
                if (projUsrs_ds.Tables["Links"].Rows.Find(szReqVer) != null)
                    return;
                newRow["requirementTitle"] = this.comboBoxRR.Text;
                newRow["version"] = this.comboBoxVer.Text;
                newRow["ReqVer"] = szReqVer;
            }
            else
            {
                if (projUsrs_ds.Tables["Links"].Rows.Find(ver_dt.Rows[this.comboBoxRR.SelectedIndex]["vid"]) != null)
                    return;
                newRow["vid"] = ver_dt.Rows[this.comboBoxRR.SelectedIndex]["vid"];
                newRow["projectid"] = ver_dt.Rows[this.comboBoxRR.SelectedIndex]["projectid"];
                newRow["version"] = ver_dt.Rows[this.comboBoxRR.SelectedIndex]["version"];
                newRow["verisonInfo"] = ver_dt.Rows[this.comboBoxRR.SelectedIndex]["verisonInfo"];
            }

            projUsrs_ds.Tables["Links"].Rows.Add(newRow);
            this.releaseListBox.Refresh();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            cmdSQL.CommandText = "";
            String title    = this.titleText.Text; 
            String status   = this.statusComboBox.SelectedItem.ToString();
            String priority = this.priorityComboBox.SelectedItem.ToString();
            String notes    = this.Text;
            string nowTime  = DateTime.Now.ToString();
            DataRow selectedRow = null;
            if (TabView.SelectedIndex == 0)
            {
                selectedRow = req_dt.Rows.Find(Convert.ToInt32(this.idText.Text));
            }
            else
            {
                selectedRow = bug_dt.Rows.Find(Convert.ToInt32(this.idText.Text));
            }
            Int32 closed = String.Compare("0/0/0000 12:00:00 AM", this.timeClosedText.Text);
            
            selectedRow["Title"]    = title;
            selectedRow["Status"]   = status;
            selectedRow["Priority"] = priority;
            selectedRow["Notes"]    = notes;
            MySql.Data.Types.MySqlDateTime mySqlDt;
            if (closed == 0 && status == "Closed")
            {
                this.timeClosedText.Text = nowTime;
                mySqlDt = new MySql.Data.Types.MySqlDateTime(DateTime.Now);
            }
            else if (status != "Closed")
            {
                this.timeClosedText.Text = "0/0/0000 12:00:00 AM";
                mySqlDt = new MySql.Data.Types.MySqlDateTime("0/0/0000 12:00:00 AM");
            }
            else
            {
                mySqlDt = new MySql.Data.Types.MySqlDateTime(DateTime.Parse(this.timeClosedText.Text.ToString()));
            }

            if (TabView.SelectedIndex == 0)
            {
                selectedRow["Time Satisfied"] = mySqlDt;
                cmdSQL.CommandText += "UPDATE requirements SET requirementTitle = '" + title + "', priority = '" +
                                      priority + "', status = '" + status + "', timeSatisfied = '" + mySqlDt.ToString() + "' WHERE rid = " + selectedRow["ReqID"] + ";";
            }
            else
            {
                selectedRow["Time Closed"] = mySqlDt;
                cmdSQL.CommandText += "UPDATE bugs SET bugTitle = '" + title + "', priority = '" +
                                      priority + "', status = '" + status + "', timeClosed = '" + mySqlDt.ToString() + "' WHERE bid = " + selectedRow["BugID"] + ";";
            }
            foreach (DataRow dr in projUsrs_ds.Tables["Owners"].Rows)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    if (TabView.SelectedIndex == 0)
                    {
                        cmdSQL.CommandText += "INSERT INTO userrequirementlinks VALUES (null, " + dr["uid"] + ", " + idText.Text + ", 'owner');";
                    }
                    else
                    {
                        cmdSQL.CommandText += "INSERT INTO userbuglinks VALUES (null, " + dr["uid"] + ", " + idText.Text + ", 'owner');";
                    }
                }
            }
            foreach (DataRow dr in projUsrs_ds.Tables["Watchers"].Rows)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    if (TabView.SelectedIndex == 0)
                    {
                        cmdSQL.CommandText += "INSERT INTO userrequirementlinks VALUES (null, " + dr["uid"] + ", " + idText.Text + ", 'watcher');";
                    }
                    else
                    {
                        cmdSQL.CommandText += "INSERT INTO userbuglinks VALUES (null, " + dr["uid"] + ", " + idText.Text + ", 'watcher');";
                    }
                }
            }
            foreach (DataRow dr in projUsrs_ds.Tables["Links"].Rows)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    if (TabView.SelectedIndex == 0)
                    {
                        cmdSQL.CommandText += "INSERT INTO requirementversionlinks VALUES (null, " + dr["vid"] + ", 'Not Satisfied'," + idText.Text + ", '');";
                    }
                    else
                    {
                        DataRowView ver = (DataRowView)this.comboBoxVer.SelectedItem;
                        DataRowView req = (DataRowView)this.comboBoxRR.SelectedItem;
                        cmdSQL.CommandText += "INSERT INTO bugversionlinks VALUES (null, " + idText.Text + ", " + ver.Row.ItemArray[0].ToString() + ");";
                        cmdSQL.CommandText += "INSERT INTO bugreqlinks VALUES (null, " + idText.Text + ", " + req.Row.ItemArray[0].ToString() + ");";
                    }
                }
            }
            if (cmdSQL.CommandText.Length > 0)
                cmdSQL.ExecuteNonQuery();
            selectedRow.AcceptChanges();
            projUsrs_ds.AcceptChanges();
        }

        private void ownerAddButton_Click(object sender, EventArgs e)
        {
            //trap to see if user is in pool
            if (this.ownerComboBox.SelectedIndex < 0)
                return;
            
            DataRow newRow = projUsrs_ds.Tables["Owners"].NewRow();
            newRow["uid"] = projUsrs_ds.Tables["All"].Rows[ownerComboBox.SelectedIndex]["uid"];
            newRow["username"] = projUsrs_ds.Tables["All"].Rows[ownerComboBox.SelectedIndex]["username"];
            newRow["role"] = "owner";
            // If it exists, skip it.
            if (projUsrs_ds.Tables["Owners"].Rows.Find(newRow["uid"]) != null)
                return;
            projUsrs_ds.Tables["Owners"].Rows.Add(newRow);
            //projUsrs_ds.Tables["Owners"].AcceptChanges();

            //make add the user to owners in Database

            //are we on a req or a bug?
            /*
            if (TabView.SelectedIndex == 0)
            {
                //requirements are selected
                cmdSQL.CommandText = "INSERT INTO userrequirementlinks VALUES (null, " + newRow["uid"] + ", " + idText.Text + ", 'owner')";
            }
            else
            {
                //bugs are selected
                cmdSQL.CommandText = "INSERT INTO userbuglinks VALUES (null, " + newRow["uid"] + ", " + idText.Text + ", 'owner')";
            }
            cmdSQL.ExecuteNonQuery();*/
        }

        private void watchersAddButton_Click(object sender, EventArgs e)
        {
            //trap to see if user is in pool
            if (this.ownerComboBox.SelectedIndex < 0)
                return;
            DataRow newRow = projUsrs_ds.Tables["Watchers"].NewRow();
            newRow["uid"] = projUsrs_ds.Tables["All"].Rows[watcherComboBox.SelectedIndex]["uid"];
            newRow["username"] = projUsrs_ds.Tables["All"].Rows[watcherComboBox.SelectedIndex]["username"];
            // If it exists, skip it.
            if (projUsrs_ds.Tables["Watchers"].Rows.Find(newRow["uid"]) != null)
                return;
            projUsrs_ds.Tables["Watchers"].Rows.Add(newRow);
            /*projUsrs_ds.Tables["Watchers"].AcceptChanges();

            //are we on a req or a bug?
            if (TabView.SelectedIndex == 0)
            {
                //requirements are selected
                cmdSQL.CommandText = "INSERT INTO userrequirementlinks VALUES (null, " + newRow["uid"] + ", " + idText.Text + ", 'watcher')";
            }
            else
            {
                //bugs are selected
                cmdSQL.CommandText = "INSERT INTO userbuglinks VALUES (null, " + newRow["uid"] + ", " + idText.Text + ", 'watcher')";
            }
            cmdSQL.ExecuteNonQuery();*/
        }

        private void comboBoxRR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabView.SelectedIndex == 1)
            {
                DataTable reqVer = new DataTable();
                cmdSQL.CommandText = "SELECT versions.* FROM versions, requirementversionlinks" +
                                     " WHERE requirementversionlinks.requirementid = " + req_dt.Rows[comboBoxRR.SelectedIndex]["ReqID"] + 
                                     " AND versions.vid = requirementversionlinks.versionid;";
                adpSQL.SelectCommand = cmdSQL;
                adpSQL.Fill(reqVer);
                this.comboBoxVer.DataSource    = reqVer.DefaultView;
                this.comboBoxVer.DisplayMember = "version";
            }
        }

        private void reqTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void reportsButton_Click(object sender, EventArgs e)
        {
            Reports reportsWindow = new Reports(conSQL, currentProjectID);
            reportsWindow.ShowDialog();
        }

        private void detailedNotesButton_Click(object sender, EventArgs e)
        {
            Notes nf = new Notes(conSQL, showing, selectedID,currentUserID);
            nf.ShowDialog();
        }
    }
}

