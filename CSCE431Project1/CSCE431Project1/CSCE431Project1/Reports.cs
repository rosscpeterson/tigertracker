using System;
using System.IO; //for report printing
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
    public partial class Reports : Form
    {
        protected MySqlConnection conSQL;
        protected MySqlCommand cmdSQL;
        protected MySqlDataAdapter adpSQL;

        protected int currProjID;
        protected DataTable req_dt, bug_dt; //requirements/bugs for the combo box
        protected DataTable req_pool_dt, bug_pool_dt;   //requirements/bugs that the user has selected to enter
        protected DataTable requirements_dt, bugs_dt;    //requirements/bugs that are to be entered into the final report

        public Reports(MySqlConnection _conSQL, int projID)
        {
            InitializeComponent();

            // Default value for variables.
            conSQL = new MySqlConnection();
            conSQL = _conSQL;
            cmdSQL = new MySqlCommand("", conSQL);
            adpSQL = new MySqlDataAdapter();
            adpSQL.SelectCommand = cmdSQL;
            currProjID = projID;
            req_dt = new DataTable();
            bug_dt = new DataTable();
            req_pool_dt = new DataTable();
            bug_pool_dt = new DataTable();
            requirements_dt = new DataTable();
            bugs_dt = new DataTable();

            reqListBox.DataSource = req_pool_dt;
            reqListBox.DisplayMember = "requirementTitle";

            bugListBox.DataSource = bug_pool_dt;
            bugListBox.DisplayMember = "bugTitle";

            //req_pool_dt.PrimaryKey = new DataColumn[] { projUsrs_ds.Tables["Watchers"].Columns["uid"] };

            //set the requirements combo box
            //cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.priority, requirements.timeCreated, "
            //    + "requirements.timeSatisfied, requirements.status, users.username, userrequirementlinks.role FROM requirements, users, versions, userrequirementlinks "
            //    + "WHERE users.uid = userrequirementlinks.userid AND userrequirementlinks.requirementid = requirements.rid AND versions.projectid = " + currProjID.ToString() + ";";
            cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() + ";";
            adpSQL.SelectCommand = cmdSQL;
            adpSQL.Fill(req_dt);

            req_pool_dt = req_dt.Clone();

            this.reqComboBox.DataSource = req_dt.DefaultView;
            this.reqComboBox.DisplayMember = "requirementTitle";
            this.reqComboBox.Refresh();

            this.reqListBox.DataSource = req_pool_dt.DefaultView;
            this.reqListBox.DisplayMember = "requirementTitle";
            this.reqListBox.Refresh();


            cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() + ";";
            adpSQL.SelectCommand = cmdSQL;
            adpSQL.Fill(bug_dt);

            bug_pool_dt = bug_dt.Clone();

            this.bugComboBox.DataSource = bug_dt.DefaultView;
            this.bugComboBox.DisplayMember = "bugTitle";
            this.bugComboBox.Refresh();

            this.bugListBox.DataSource = bug_pool_dt.DefaultView;
            this.bugListBox.DisplayMember = "bugTitle";
            this.bugListBox.Refresh();

            
            
            //cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
            //                     " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
            //                     " FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() +
            //                     " ORDER BY status DESC, priority DESC, rid DESC;";
        }

        private void reqAddButton_Click(object sender, EventArgs e)
        {
            if (reqNoneCheckBox.Checked)
            {
                reqNoneCheckBox.Checked = false;
            }
            //add to the req_pool_dt
            if (reqComboBox.SelectedIndex < 0)
                return;
            DataRow newRow = req_pool_dt.NewRow();
            newRow.ItemArray = req_dt.Rows[reqComboBox.SelectedIndex].ItemArray;
            req_pool_dt.Rows.Add(newRow);
            req_dt.Rows[reqComboBox.SelectedIndex].Delete();
            req_pool_dt.AcceptChanges();
            req_dt.AcceptChanges();

        }

        private void reqAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (reqAllCheckBox.Checked)
            {
                reqNoneCheckBox.Checked = false;
                reqOpenCheckBox.Enabled = false;
                reqInProgressCheckBox.Enabled = false;
                reqClosedCheckBox.Enabled = false;
                reqComboBox.Enabled = false;
                reqAddButton.Enabled = false;
            }
            else
            {
                reqOpenCheckBox.Enabled = true;
                reqInProgressCheckBox.Enabled = true;
                reqClosedCheckBox.Enabled = true;
                reqComboBox.Enabled = true;
                reqAddButton.Enabled = true;
            }
        }

        private void bugAddButton_Click(object sender, EventArgs e)
        {
            if (bugNoneCheckBox.Checked)
            {
                bugNoneCheckBox.Checked = false;
            }
            //add to the bug_pool_dt
            if (bugComboBox.SelectedIndex < 0)
                return;
            DataRow newRow = bug_pool_dt.NewRow();
            newRow.ItemArray = bug_dt.Rows[bugComboBox.SelectedIndex].ItemArray;
            bug_pool_dt.Rows.Add(newRow);
            bug_dt.Rows[bugComboBox.SelectedIndex].Delete();
            bug_pool_dt.AcceptChanges();
            bug_dt.AcceptChanges();

        }

        private void bugAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bugAllCheckBox.Checked)
            {
                bugNoneCheckBox.Checked = false;
                bugOpenCheckBox.Enabled = false;
                bugInProgressCheckBox.Enabled = false;
                bugClosedCheckBox.Enabled = false;
                bugComboBox.Enabled = false;
                bugAddButton.Enabled = false;
            }
            else
            {
                bugOpenCheckBox.Enabled = true;
                bugInProgressCheckBox.Enabled = true;
                bugClosedCheckBox.Enabled = true;
                bugComboBox.Enabled = true;
                bugAddButton.Enabled = true;
            }
        }

        private void reqNoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (reqNoneCheckBox.Checked)
            {
                reqAllCheckBox.Checked = false;
                reqOpenCheckBox.Enabled = false;
                reqInProgressCheckBox.Enabled = false;
                reqClosedCheckBox.Enabled = false;
                reqComboBox.Enabled = false;
                reqAddButton.Enabled = false;
                reqListBox.Enabled = false;
            }
            else
            {
                reqOpenCheckBox.Enabled = true;
                reqInProgressCheckBox.Enabled = true;
                reqClosedCheckBox.Enabled = true;
                reqComboBox.Enabled = true;
                reqAddButton.Enabled = true;
                reqListBox.Enabled = true;
            }
        }

        private void bugNoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bugNoneCheckBox.Checked)
            {
                bugAllCheckBox.Checked = false;
                bugOpenCheckBox.Enabled = false;
                bugInProgressCheckBox.Enabled = false;
                bugClosedCheckBox.Enabled = false;
                bugComboBox.Enabled = false;
                bugListBox.Enabled = false;
                bugAddButton.Enabled = false;
            }
            else
            {
                bugOpenCheckBox.Enabled = true;
                bugInProgressCheckBox.Enabled = true;
                bugClosedCheckBox.Enabled = true;
                bugComboBox.Enabled = true;
                bugListBox.Enabled = true;
                bugAddButton.Enabled = true;
            }
        }

        //print the needed reports
        private void genReportButton_Click(object sender, EventArgs e)
        {
            //make a requirement report
            req_Report();
            //make a bug report
            bug_Report();
            //close the report window to prevent SQL duplication
            this.Close();
        }

        //generates the requirements report
        private void req_Report()
        {
            //-------------------------HANDLE REQUIREMENTS-------------------------------------
            //Create the FileName
            string fileName = get_File_Name("Report", "Requirement");
            //SQL statement for every requirement in the project
            string select = "SELECT requirements.rid," +
                                " requirements.requirementTitle," +
                                " requirements.requirementDescription," +
                                " requirements.priority," +
                                " requirements.timeCreated," +
                                " requirements.timeSatisfied," +
                                " requirements.status," +
                                " requirements.notes ";
            string from = "FROM requirements, versions, requirementversionlinks ";
            string where = "WHERE requirementversionlinks.requirementid = requirements.rid" +
                            " AND requirementversionlinks.versionid = versions.vid" +
                            " AND versions.projectid = " + currProjID.ToString() + " ";
            string order = "ORDER BY status DESC, priority DESC, rid DESC;";

            //---All Requirements
            if (reqAllCheckBox.Checked)
            {
                cmdSQL.CommandText = select + from + where + order;
            }

            //---Some Requirements
            if (!reqAllCheckBox.Checked && !reqNoneCheckBox.Checked)
            {
                //all possible combinations of Open/Closed/In Progress
                if (reqOpenCheckBox.Checked && reqClosedCheckBox.Checked && reqInProgressCheckBox.Checked)
                {
                    where += "And requirements.status = 'Open' Or 'Closed' Or 'In Progress' ";
                }
                else if (reqOpenCheckBox.Checked && reqClosedCheckBox.Checked && !reqInProgressCheckBox.Checked)
                {
                    where += "And requirements.status = 'Open' Or 'Closed' ";
                }
                else if (reqOpenCheckBox.Checked && !reqClosedCheckBox.Checked && reqInProgressCheckBox.Checked)
                {
                    where += "And requirements.status = 'Open' Or 'In Progress' ";
                }
                else if (reqOpenCheckBox.Checked && !reqClosedCheckBox.Checked && !reqInProgressCheckBox.Checked)
                {
                    where += "And requirements.status = 'Open' ";
                }
                else if (!reqOpenCheckBox.Checked && reqClosedCheckBox.Checked && reqInProgressCheckBox.Checked)
                {
                    where += "And requirements.status = 'Closed' Or 'In Progress' ";
                }
                else if (!reqOpenCheckBox.Checked && reqClosedCheckBox.Checked && !reqInProgressCheckBox.Checked)
                {
                    where += "And requirements.status = 'Closed' ";
                }
                else if (!reqOpenCheckBox.Checked && !reqClosedCheckBox.Checked && reqInProgressCheckBox.Checked)
                {
                    where += "And requirements.status = 'In Progress' ";
                }
                else if (!reqOpenCheckBox.Checked && !reqClosedCheckBox.Checked && !reqInProgressCheckBox.Checked) {
                    //will probably result in no results - yet for completeness it is included.
                    where += "And requirements.status = '' ";
                }
                //gets all the possible requirement Titles to Look for
                //FIX ME!!!!
                cmdSQL.CommandText = select + from + where + order;
            }

            //---No Requirements
            if (reqNoneCheckBox.Checked)
            {
                //do nothing.
            }

            //Set the SQL Statement
            adpSQL.SelectCommand = cmdSQL;
            adpSQL.Fill(requirements_dt);
            //Print Report
            report(fileName, requirements_dt);
        }

        //generates the bugs report
        private void bug_Report()
        {
            //Create the FileName
            string fileName = get_File_Name("Report", "Bug");
            //SQL statement for every bug in the project
            string select = "SELECT DISTINCT bugs.bid," +
                            " bugs.bugTitle," +
                            " bugs.bugDescription," +
                            " bugs.status," +
                            " bugs.timeOpen," +
                            " bugs.timeClosed," +
                            " bugs.notes," +
                            " bugs.priority ";
            string from = "FROM bugs, bugreqlinks, requirements, requirementversionlinks, bugversionlinks, versions ";
            string where = "WHERE bugreqlinks.bugid = bugs.bid" +
                            " AND bugreqlinks.requirementid = requirements.rid" +
                            " AND requirementversionlinks.requirementid = requirements.rid" +
                            " AND requirementversionlinks.versionid = versions.vid" +
                            " AND versions.projectid = " + currProjID.ToString() +
                            " AND bugversionlinks.versionid = versions.vid" +
                            " AND bugversionlinks.bugid = bugs.bid ";
            string order = "ORDER BY status DESC, priority DESC, bid DESC;";

            //---All Bugs
            if (bugAllCheckBox.Checked)
            {
                cmdSQL.CommandText = select + from + where + order;
            }
            //---Some Bugs
            if (!bugNoneCheckBox.Checked && !bugAllCheckBox.Checked)
            {
                //all possible combinations of Open/Closed/In Progress
                if (bugOpenCheckBox.Checked && bugClosedCheckBox.Checked && bugInProgressCheckBox.Checked)
                {
                    where += "And bugs.status = 'Open' Or 'Closed' Or 'In Progress' ";
                }
                else if (bugOpenCheckBox.Checked && bugClosedCheckBox.Checked && !bugInProgressCheckBox.Checked)
                {
                    where += "And bugs.status = 'Open' Or 'Closed' ";
                }
                else if (bugOpenCheckBox.Checked && !bugClosedCheckBox.Checked && bugInProgressCheckBox.Checked)
                {
                    where += "And bugs.status = 'Open' Or 'In Progress' ";
                }
                else if (bugOpenCheckBox.Checked && !bugClosedCheckBox.Checked && !bugInProgressCheckBox.Checked)
                {
                    where += "And bugs.status = 'Open' ";
                }
                else if (!bugOpenCheckBox.Checked && bugClosedCheckBox.Checked && bugInProgressCheckBox.Checked)
                {
                    where += "And bugs.status = 'Closed' Or 'In Progress' ";
                }
                else if (!bugOpenCheckBox.Checked && bugClosedCheckBox.Checked && !bugInProgressCheckBox.Checked)
                {
                    where += "And bugs.status = 'Closed' ";
                }
                else if (!bugOpenCheckBox.Checked && !bugClosedCheckBox.Checked && bugInProgressCheckBox.Checked)
                {
                    where += "And bugs.status = 'In Progress' ";
                }
                else if (!bugOpenCheckBox.Checked && !bugClosedCheckBox.Checked && !bugInProgressCheckBox.Checked)
                {
                    //will probably result in no results - yet for completeness it is included.
                    where += "And bugs.status = '' ";
                }
                //gets all the possible requirement Titles to Look for
                //FIX ME!!!!
                cmdSQL.CommandText = select + from + where + order;
            }
            //---No Bugs
            if (bugNoneCheckBox.Checked)
            {
                //do nothing.
            }
            adpSQL.SelectCommand = cmdSQL;
            adpSQL.Fill(bugs_dt);
            report(fileName, bugs_dt);
        }

        //Returns the string with the first available fileName
        private string get_File_Name(string name, string type)
        {
            string temp = name;
            int count = 0;
            while (File.Exists(name + " - " + type + ".csv"))
            {
                name = temp + count;
                count++;
            }
            return name + " - " + type;
        }

        //prints the information in the table to a file
        private void report(string fileName, DataTable table){
            // create a writer and open the file
            TextWriter tw = new StreamWriter(fileName + ".csv");

            //create a reader for the table
            DataTableReader reader = table.CreateDataReader();

            //write the field names to the file.
            for (int i = 0; i < reader.FieldCount; i++){
                tw.Write(reader.GetName(i).Trim());
                tw.Write(",");
            }
            tw.WriteLine();

            //write the field values to the file.
            while (reader.Read()){
                for (int i = 0; i < reader.FieldCount; i++){
                    tw.Write(reader.GetValue(i).ToString().Trim());
                    tw.Write(",");
                }
                tw.WriteLine();
            }

            //close open files/streams
            reader.Close();
            tw.Close();
        }
    }
}
