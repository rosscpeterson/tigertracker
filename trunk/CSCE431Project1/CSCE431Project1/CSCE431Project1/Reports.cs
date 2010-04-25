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

        //Returns the string with the first available fileName
        private string get_File_Name(string name)
        {
            string temp = name;
            int count = 0;
            while (File.Exists(name + ".csv"))
            {
                name = temp + count;
                count++;
            }
            return name;
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

        private void genReportButton_Click(object sender, EventArgs e){
            //-------------------------HANDLE REQUIREMENTS-------------------------------------
            //Create the FileName
            string fileName = get_File_Name("Report - Requirement");
            //setup the basic SQL statement
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
                            " AND versions.projectid = " + currProjID.ToString();
            string order = " ORDER BY status DESC, priority DESC, rid DESC;";

            //For All Requirements
            if (reqAllCheckBox.Checked)
            {
                cmdSQL.CommandText = select + from + where + order;
            }

            //For Some Requirements
            if (!reqAllCheckBox.Checked && !reqNoneCheckBox.Checked)
            {
                //Gets the possible status to look for.
                if (reqInProgressCheckBox.Checked)
                {
                    where = where + " And requirements.status = 'In Progress' ";
                }
                if (reqOpenCheckBox.Checked)
                {
                    where = where + " And requirements.status = 'Open' ";
                }
                if (reqClosedCheckBox.Checked)
                {
                    where = where + " And requirements.status = 'Closed' ";
                }
                /* ALL possible combinations. debug purpose.
                if (reqOpenCheckBox.Checked && reqClosedCheckBox.Checked && reqInProgressCheckBox.Checked){
                    where = where + " And requirements.status = 'Open' Or 'Closed' Or 'In Progress' ";
                }
                else if (reqOpenCheckBox.Checked && reqClosedCheckBox.Checked && !reqInProgressCheckBox.Checked){
                    where = where + " And requirements.status = 'Open' Or 'Closed' ";
                }
                else if (reqOpenCheckBox.Checked && !reqClosedCheckBox.Checked && reqInProgressCheckBox.Checked){
                    where = where + " And requirements.status = 'Open' Or 'In Progress' ";
                }
                else if (reqOpenCheckBox.Checked && !reqClosedCheckBox.Checked && !reqInProgressCheckBox.Checked){
                    where = where + " And requirements.status = 'Open' ";
                }
                else if (!reqOpenCheckBox.Checked && reqClosedCheckBox.Checked && reqInProgressCheckBox.Checked){
                    where = where + " And requirements.status = 'Closed' Or 'In Progress' ";
                }
                else if (!reqOpenCheckBox.Checked && reqClosedCheckBox.Checked && !reqInProgressCheckBox.Checked){
                    where = where + " And requirements.status = 'Closed' ";
                }
                else if (!reqOpenCheckBox.Checked && !reqClosedCheckBox.Checked && reqInProgressCheckBox.Checked){
                    where = where + " And requirements.status = 'In Progress' ";
                }
                 */
                //gets all the possible requirement Titles to Look for
                //FIX ME!!!!
                cmdSQL.CommandText = select + from + where + order;
            }
            
            //For No Requirements, do nothing.
            adpSQL.SelectCommand = cmdSQL;
            adpSQL.Fill(requirements_dt);
            report(fileName, requirements_dt);
            
            //-------------------------HANDLE BUGS---------------------------------------------
            //Create the FileName
            fileName = get_File_Name("Report - Bug");

            //For All Bugs
            if (bugAllCheckBox.Checked){
                cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                     " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                     " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                     " ORDER BY bid DESC;";
                adpSQL.SelectCommand = cmdSQL;
                adpSQL.Fill(bugs_dt);
            }
            //For No Bugs do nothing.
            report(fileName, bugs_dt);


            /*------------------BREAK BREAK BREAK BREAK-----------------------
            if (!reqNoneCheckBox.Checked && reqAllCheckBox.Checked)
            {
                //if all is selected and none is not (none gets precidence)
                if (!reqOpenCheckBox.Checked && !reqInProgressCheckBox.Checked && !reqClosedCheckBox.Checked)
                {
                    //none of the status boxes checked
                    cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                     " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                     " FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() +
                                     " ORDER BY status DESC, priority DESC, rid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(requirements_dt);
                }
                else if (reqOpenCheckBox.Checked && !reqInProgressCheckBox.Checked && !reqClosedCheckBox.Checked)
                {
                    //we want all open bugs
                    cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                     " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                     " FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() + " AND requirements.status = 'Open'" +
                                     " ORDER BY status DESC, priority DESC, rid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(requirements_dt);
                }
                else if (!reqOpenCheckBox.Checked && reqInProgressCheckBox.Checked && !reqClosedCheckBox.Checked)
                {
                    //we want all In Progress bugs
                    cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                     " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                     " FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() + " AND requirements.status = 'In Progress'" +
                                     " ORDER BY status DESC, priority DESC, rid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(requirements_dt);
                }
                else if (!reqOpenCheckBox.Checked && !reqInProgressCheckBox.Checked && reqClosedCheckBox.Checked)
                {
                    //we want all Closed bugs
                    cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                     " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                     " FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() + " AND requirements.status = 'Closed'" +
                                     " ORDER BY status DESC, priority DESC, rid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(requirements_dt);
                }
                else if (reqOpenCheckBox.Checked && reqInProgressCheckBox.Checked && !reqClosedCheckBox.Checked)
                {
                    //we want all open and In Progress bugs
                    cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                     " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                     " FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() + " AND requirements.status = 'Open'" +
                                     " OR requirements.status = 'In Progress' ORDER BY status DESC, priority DESC, rid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(requirements_dt);
                }
                else if (!reqOpenCheckBox.Checked && reqInProgressCheckBox.Checked && reqClosedCheckBox.Checked)
                {
                    //we want all In Progress and Closed bugs
                    cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                     " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                     " FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() + " AND requirements.status = 'Closed'" +
                                     " OR requirements.status = 'In Progress' ORDER BY status DESC, priority DESC, rid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(requirements_dt);
                }
                else if (reqOpenCheckBox.Checked && !reqInProgressCheckBox.Checked && reqClosedCheckBox.Checked)
                {
                    //we want all open and closed bugs
                    cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                     " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                     " FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() + " AND requirements.status = 'Open'" +
                                     " OR requirements.status = 'Closed' ORDER BY status DESC, priority DESC, rid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(requirements_dt);
                }
            }
            else if (!reqNoneCheckBox.Checked)
            {
                //we need to go throug them one by one.
                DataTable requirements_temp_dt = new DataTable();
                cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                 " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                 " FROM requirements, versions WHERE versions.projectid = " + currProjID.ToString() +
                                 " ORDER BY status DESC, priority DESC, rid DESC;";
                adpSQL.SelectCommand = cmdSQL;
                adpSQL.Fill(requirements_temp_dt);

                requirements_dt = requirements_temp_dt.Clone();

                for (int i = 0; i < req_pool_dt.Rows.Count; i++)
                {
                    String expression = "rid = " + req_pool_dt.Rows[i][0].ToString();
                    DataRow[] newRow;
                    newRow = requirements_temp_dt.Select(expression);
                    for (int j = 0; j < newRow.Length; j++)
                    {
                        requirements_dt.ImportRow(newRow[j]);
                    }
                }
                Console.Out.WriteLine("Requirements_dt has length of " + requirements_dt.Rows.Count);   //for debug purposes only
            }
            //HANDLE BUGS-----------------------------------------------------------------------------------------------------------------
            cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                 " ORDER BY bid DESC;";
            bug_dt = new DataTable();
            adpSQL.Fill(bug_dt);

            if (!bugNoneCheckBox.Checked && bugAllCheckBox.Checked)
            {
                //if all is selected and none is not (none gets precidence)
                if (!bugOpenCheckBox.Checked && !bugInProgressCheckBox.Checked && !bugClosedCheckBox.Checked)
                {
                    //none of the status boxes checked
                    cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                 " ORDER BY bid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(bugs_dt);
                }
                else if (bugOpenCheckBox.Checked && !bugInProgressCheckBox.Checked && !bugClosedCheckBox.Checked)
                {
                    //we want all open bugs
                    cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                 " AND bugs.status = 'Open'" +
                                 " ORDER BY bid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(bugs_dt);
                }
                else if (!bugOpenCheckBox.Checked && bugInProgressCheckBox.Checked && !bugClosedCheckBox.Checked)
                {
                    //we want all In Progress bugs
                    cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                 " AND bugs.status = 'In Progress'" +
                                 " ORDER BY bid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(bugs_dt);
                }
                else if (!bugOpenCheckBox.Checked && !bugInProgressCheckBox.Checked && bugClosedCheckBox.Checked)
                {
                    //we want all Closed bugs
                    cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                 " AND bugs.status = 'Closed'" +
                                 " ORDER BY bid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(bugs_dt);
                }
                else if (bugOpenCheckBox.Checked && bugInProgressCheckBox.Checked && !bugClosedCheckBox.Checked)
                {
                    //we want all open and In Progress bugs
                    cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                 " AND bugs.status = 'Open' OR bugs.status = 'In Progress'" +
                                 " ORDER BY bid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(bugs_dt);
                }
                else if (!bugOpenCheckBox.Checked && bugInProgressCheckBox.Checked && bugClosedCheckBox.Checked)
                {
                    //we want all In Progress and Closed bugs
                    cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                 " AND bugs.status = 'Closed' OR bugs.status = 'In Progress'" +
                                 " ORDER BY bid DESC;";
                    adpSQL.SelectCommand = cmdSQL; 
                    adpSQL.Fill(bugs_dt);
                }
                else if (bugOpenCheckBox.Checked && !bugInProgressCheckBox.Checked && bugClosedCheckBox.Checked)
                {
                    //we want all open and closed bugs
                    cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                 " AND bugs.status = 'Open' AND bugs.status = 'Closed'" +
                                 " ORDER BY bid DESC;";
                    adpSQL.SelectCommand = cmdSQL;
                    adpSQL.Fill(bugs_dt);
                }
            }
            else if (!bugNoneCheckBox.Checked)
            {
                //we need to go throug them one by one.
                DataTable bug_temp_dt = new DataTable();
                cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                  " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                  " FROM bugs, versions WHERE versions.projectid = " + currProjID.ToString() +
                                  " ORDER BY bid DESC;";
                adpSQL.SelectCommand = cmdSQL;
                adpSQL.Fill(bug_temp_dt);

                bugs_dt = bug_temp_dt.Clone();

                for (int i = 0; i < bug_pool_dt.Rows.Count; i++)
                {
                    String expression = "rid = " + bug_pool_dt.Rows[i][0].ToString();
                    DataRow[] newRow;
                    newRow = bug_temp_dt.Select(expression);
                    for (int j = 0; j < newRow.Length; j++)
                    {
                        bugs_dt.ImportRow(newRow[j]);
                    }
                }
                Console.Out.WriteLine("bugs_dt has length of " + bugs_dt.Rows.Count);   //for debug purposes only
            }

            /*cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                     " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                     " FROM requirements, versions, requirementversionlinks WHERE requirementversionlinks.requirementid = requirements.rid AND " +
                     "requirementversionlinks.versionid = versions.vid AND versions.projectid = " + currentProjectID.ToString() +
                     " ORDER BY status DESC, priority DESC, rid DESC;";

            //------------BUG REPORT START-------------------------
            DataTableReader bugReader = bugs_dt.CreateDataReader();
            
            //write the field names to the file.
            for (int i = 0; i < bugReader.FieldCount; i++){
                tw.Write(bugReader.GetName(i).Trim());
                tw.Write("; ");
            }
            tw.WriteLine();
            
            //write the field values to the file.
            while (bugReader.Read()){
                //declared variables for ease of undstanding code
                for (int i = 0; i < bugReader.FieldCount; i++){
                    tw.Write(bugReader.GetValue(i).ToString().Trim());
                    tw.Write("; ");
                }
                tw.WriteLine();
                tw.WriteLine();
            }
            //------------BUG REPORT END---------------------------

            // close open readers/streams
            bugReader.Close();
            reqReader.Close();
            tw.Close();
             */
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

        private void bugNoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bugNoneCheckBox.Checked)
            {
                bugAllCheckBox.Checked = false;
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
    }
}
